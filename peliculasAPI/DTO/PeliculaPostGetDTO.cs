using System.Collections.Generic;

namespace peliculasAPI.DTO
{
	public class PeliculaPostGetDTO
	{
		public List<GenerosDTO> Generos { get; set; }
		public List<CineDTO> Cines { get; set; }
	}
}
