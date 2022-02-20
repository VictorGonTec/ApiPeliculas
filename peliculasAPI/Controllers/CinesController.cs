using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using peliculasAPI.DTO;
using peliculasAPI.Entidades;
using peliculasAPI.Utilidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peliculasAPI.Controllers
{
	[ApiController]
	[Route("api/cines")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
	public class CinesController : ControllerBase
	{
		private readonly AplicationDbContex context;
		private readonly IMapper mapper;

		public CinesController(AplicationDbContex context,
			IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<CineDTO>>> Get([FromQuery]PaginacionDTO paginacionDTO)
		{
			var queryable = context.Cine.AsQueryable();
			await HttpContext.InsertaParametrosPaginacionEnCabecera(queryable);
			var cines = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
			return mapper.Map<List<CineDTO>>(cines);
		}


		[HttpGet("{Id:int}")] //api/generos/Id este es el endpoint final
		public async Task<ActionResult<CineDTO>> Get(int Id)
		{
			var cines = await context.Cine.FirstOrDefaultAsync(x => x.Id == Id);
			if (cines == null)
			{
				return NotFound();
			}
			return mapper.Map<CineDTO>(cines);
		}

		[HttpPut("id:int")]
		public async Task<ActionResult> Put(int id, [FromBody] CineCreacionDTO cineCreacionDTO)
		{
			var cines = await context.Cine.FirstOrDefaultAsync(x => x.Id == id);

			if (cines == null)
			{
				return NotFound();
			}
			cines = mapper.Map(cineCreacionDTO, cines);
			await context.SaveChangesAsync();
			return NotFound();
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] CinesCreacionDTO cinesCreacionDTO)
		{
			var cines = mapper.Map<Cine>(cinesCreacionDTO);//convertimos los datos
			context.Add(cines);//agregamos a la tabla
			await context.SaveChangesAsync();//guardamos los cambios en la DB
			return NoContent();
		}

		[HttpDelete("id:int")]
		public async Task<ActionResult> Delete(int id)
		{
			var existe = await context.Cine.AnyAsync(x => x.Id == id);

			if (!existe)
			{
				return NotFound();
			}
			context.Remove(new Cine { Id=id});
			await context.SaveChangesAsync();
			return NoContent();


		}
	}
}
