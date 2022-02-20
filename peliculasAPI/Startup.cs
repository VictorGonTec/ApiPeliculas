using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using peliculasAPI.ApiBehavior;
using peliculasAPI.Filtros;
using peliculasAPI.Utilidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peliculasAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			//para apagar el mapeo automatico de jwt
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Startup));

			services.AddSingleton(provider =>
				new MapperConfiguration(config =>
				{
					var geometryFactory = provider.GetRequiredService<GeometryFactory>();
					config.AddProfile(new AutoMapperProfiles(geometryFactory));
				}).CreateMapper());

			services.AddTransient<IAlmacenadorArchivo, AlmacenadorArchivoLocal>();

			services.AddHttpContextAccessor();

			//para la conexio a la base de datos pasando el conexion string
			services.AddDbContext<AplicationDbContex>
			(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"),
			sqlServer => sqlServer.UseNetTopologySuite()));

			//para generar los string de medicion geolocalizacion
			services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<AplicationDbContex>()
				.AddDefaultTokenProviders();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opciones =>
					opciones.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer=false,
						ValidateAudience=false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey=true,
						IssuerSigningKey=new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
						ClockSkew=TimeSpan.Zero
					});

			services.AddResponseCaching();//nos da acceso al permiso de catching de asp.net
										  //implementar la inyeccion de dependencia

			services.AddAuthorization(opciones =>
			{
				opciones.AddPolicy("EsAdmin", policy => policy.RequireClaim("role", "admin"));
			});

			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(FiltroDeExcepcion));
				options.Filters.Add(typeof(ParsearBadRequest));
			}).ConfigureApiBehaviorOptions(BehaviorBadRequest.Parsear);

			//dar permiso al cors
			services.AddCors(options =>
			{
				var frontendURL = Configuration.GetValue<string>("frontend_url");
				options.AddDefaultPolicy(builder =>
				{
					builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
					.WithExposedHeaders(new string[] { "cantidadTotalRegistro" });
					
				});
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "peliculasAPI", Version = "v1" });
				c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "peliculasAPI v1"));
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseCors();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseDeveloperExceptionPage();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
