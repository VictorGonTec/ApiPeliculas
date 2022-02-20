using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.DTO
{
	public class RatingDTO
	{
		public int PeliculaId { get; set; }
		[Range(1,5)]
		public int Puntiacion { get; set; }
	}
}
