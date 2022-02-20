namespace peliculasAPI.Entidades
{
	public class PeliculasCines
	{
		public int PeliculaId { get; set; }
		public int CineId { get; set; }
		public Peliculas Peliculas { get; set; }
		public Cine Cine { get; set; }
	}
}
