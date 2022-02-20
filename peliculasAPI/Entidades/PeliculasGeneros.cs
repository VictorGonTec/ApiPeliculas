namespace peliculasAPI.Entidades
{
	public class PeliculasGeneros
	{
		public int PeliculaId { get; set; }
		public int GeneroId { get; set; }
		public Peliculas Peliculas { get; set; }
		public Generos Genero { get; set; }
	}
}
