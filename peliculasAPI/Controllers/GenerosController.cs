//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using peliculasAPI.Entidades;
using peliculasAPI.Filtros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using peliculasAPI.DTO;
using AutoMapper;
using peliculasAPI.Utilidades;

namespace peliculasAPI.Controllers
{
	[Route("api/generos")]
	[ApiController]//para controlar las validaciones
				   //protejemos por authenticacion las peticiones del controlador
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
	public class GenerosController:ControllerBase
	{
		private readonly ILogger<GenerosController> logger;
		private readonly AplicationDbContex context;
		private readonly IMapper mapper;

		//inyectamos el aplicationDbContext para trabajar con la base de datos
		public GenerosController(
			ILogger<GenerosController> logger,
			AplicationDbContex context,
			IMapper mapper
			)
		{
			this.logger = logger;
			this.context = context;
			this.mapper = mapper;
		}

		
		[HttpGet]
		public async Task<ActionResult<List<GenerosDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
		{
			//logger.LogInformation("Vamos a mostrar los generos");
			var queryable = context.Generos.AsQueryable();
			await HttpContext.InsertaParametrosPaginacionEnCabecera(queryable);
			var generos = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
			return mapper.Map<List<GenerosDTO>>(generos);
		}

		[HttpGet("todos")]
		[AllowAnonymous]
		public async Task<ActionResult<List<GenerosDTO>>> Todos()
		{
			var generos = await context.Generos.OrderBy(x=>x.Nombre).ToListAsync();
			return mapper.Map<List<GenerosDTO>>(generos);
		}

		[HttpGet("{Id:int}")] //api/generos/Id este es el endpoint final
		public async Task<ActionResult<GenerosDTO>> Get(int Id)
		{
			var genero = await context.Generos.FirstOrDefaultAsync(x=>x.Id==Id);
			if (genero == null)
			{
				return NotFound();
			}
			return mapper.Map<GenerosDTO>(genero);
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
		{
			var genero = mapper.Map<Generos>(generoCreacionDTO);
			context.Add(genero);
			await context.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("id:int")]
		public async Task<ActionResult> Put(int id,[FromBody] GeneroCreacionDTO generoCreacionDTO)
		{
			var genero = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);

			if(genero == null)
			{
				return NotFound();
			}
			genero = mapper.Map(generoCreacionDTO, genero);
			await context.SaveChangesAsync();
			return NotFound();
		}

		[HttpDelete("id:int")]
		public async Task<ActionResult> Delete(int id)
		{
			var existe = await context.Generos.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound();
			}
			context.Remove(new Generos() { Id = id });
			await context.SaveChangesAsync();
			return NoContent();
		}

	}
}
