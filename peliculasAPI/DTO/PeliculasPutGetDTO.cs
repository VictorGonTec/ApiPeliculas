using System.Collections.Generic;
using System;

namespace peliculasAPI.DTO
{
	public class PeliculasPutGetDTO
	{
		public PeliculaDTO Pelicula { get; set; }
		public List<GenerosDTO> GenerosSeleccionados { get; set; }
		public List<GenerosDTO> GenerosNoSeleccionados { get; set; }
		public List<CineDTO> CinesSeleccionados { get; set; }
		public List<CineDTO> CinesNoSeleccionados { get; set; }
		public List<PeliculaActorDTO> Actores { get; set; }
	}
}
