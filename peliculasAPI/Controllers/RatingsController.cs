using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using peliculasAPI.DTO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using peliculasAPI.Entidades;

namespace peliculasAPI.Controllers
{
	
	[Route("api/rating")]
	[ApiController]
	public class RatingsController:ControllerBase
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly AplicationDbContex context;

		public RatingsController(UserManager<IdentityUser> userManager,
			AplicationDbContex context)
		{
			this.userManager = userManager;
			this.context = context;
		}

		[HttpPost]
		[Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult> Post([FromBody] RatingDTO ratingDTO)
		{
			var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
			var usuario = await userManager.FindByEmailAsync(email);
			var usuarioId = usuario.Id;

			var ratingActual = await context.Ratings
				.FirstOrDefaultAsync(x => x.PeliculaId == ratingDTO.PeliculaId
				&& x.UsuarioId == usuarioId);

			if (ratingActual == null)
			{
				var rating = new Rating();
				rating.PeliculaId = ratingDTO.PeliculaId;
				rating.Puntuacion = ratingDTO.Puntiacion;
				rating.UsuarioId = usuarioId;
				context.Add(rating);
			}
			else
			{
				ratingActual.Puntuacion = ratingDTO.Puntiacion;
			}

			await context.SaveChangesAsync();
			return NoContent();
		}
	}
}
