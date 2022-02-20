using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using peliculasAPI.Entidades;

namespace peliculasAPI
{
	public class AplicationDbContex : IdentityDbContext
	{
		public AplicationDbContex(DbContextOptions options) 
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PeliculasActores>()
				.HasKey(x => new { x.ActorId, x.PeliculaId });

			modelBuilder.Entity<PeliculasGeneros>()
				.HasKey(x => new { x.PeliculaId, x.GeneroId });

			modelBuilder.Entity<PeliculasCines>()
				.HasKey(x => new { x.PeliculaId, x.CineId });



			base.OnModelCreating(modelBuilder);
		}

		//DbSet para definir las tablas que queremos generar
		public DbSet<Generos> Generos { get; set; }
		public DbSet<Actor> Actores { get; set; }
		public DbSet<Cine> Cine { get; set; }
		public DbSet<Peliculas> Peliculas { get; set; }
		public DbSet<PeliculasActores> PeliculasActores { get; set; }
		public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
		public DbSet<PeliculasCines> PeliculasCines { get; set; }
		public DbSet<Rating> Ratings { get; set; }


	}
}
