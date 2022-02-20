using System.Collections.Generic;

namespace peliculasAPI.DTO
{
	public class LandingPageDTO
	{
		public List<PeliculaDTO> EnCines { get; set; }
		public List<PeliculaDTO> ProximosEstrenos { get; set; }
	}
}
