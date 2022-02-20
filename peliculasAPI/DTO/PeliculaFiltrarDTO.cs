namespace peliculasAPI.DTO
{
	public class PeliculaFiltrarDTO
	{
		public int Pagina { get; set; }
		public int RecordPorPagina { get; set; }
		public PaginacionDTO PaginacionDTO
		{
			get { return new PaginacionDTO() { Pagina = Pagina, RecordsPorPagina = RecordPorPagina }; }
		}
		public string Titulo { get; set; }
		public int GeneroId	{ get; set; }
		public bool EnCines { get; set; }
		public bool ProximosEstrenos { get; set; }
	}
}
