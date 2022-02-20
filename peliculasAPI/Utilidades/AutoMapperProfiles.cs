using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using peliculasAPI.DTO;
using peliculasAPI.Entidades;
using System.Collections.Generic;

namespace peliculasAPI.Utilidades
{
	public class AutoMapperProfiles:Profile
	{
		public AutoMapperProfiles(GeometryFactory geometryFactory)
		{
			CreateMap<Generos, GenerosDTO>().ReverseMap();
			CreateMap<GeneroCreacionDTO,Generos>();
			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<ActorCreacionDTO, Actor>()
				.ForMember(x => x.Foto, options => options.Ignore());
			CreateMap<CineCreacionDTO, Cine>()
				.ForMember(x => x.Ubicacion, x => x.MapFrom(dto =>
					 geometryFactory.CreatePoint(new Coordinate(dto.Longitud, dto.Latitud))));

			CreateMap<Cine, CineDTO>()
				.ForMember(x => x.Latitud, dto => dto.MapFrom(campo => campo.Ubicacion.Y))
				.ForMember(x => x.Longitud, dto => dto.MapFrom(campo => campo.Ubicacion.X));

			CreateMap<PeliculaCreacionDTO, Peliculas>()
				.ForMember(x => x.Poster, opciones => opciones.Ignore())
				.ForMember(x => x.PeliculasGeneros, opciones => opciones.MapFrom(MapearPeliculasGeneros))
				.ForMember(x => x.PeliculasCines, opciones => opciones.MapFrom(MapearPeliculasCines))
				.ForMember(x => x.PeliculasActores, opciones => opciones.MapFrom(MapearPeliculasActores));

			CreateMap<Peliculas, PeliculaDTO>()
				.ForMember(x => x.Generos, options => options.MapFrom(MappearPeliculaGeneros))
				.ForMember(x => x.Actores, options => options.MapFrom(MappearPeliculaActores))
				.ForMember(x => x.Cines, options => options.MapFrom(MappearPeliculaCines));

			CreateMap<IdentityUser, UsuarioDTO>();
		}
		private List<CineDTO> MappearPeliculaCines(Peliculas pelicula, PeliculaDTO peliculaDTO)
		{
			var resultado = new List<CineDTO>();

			if (pelicula.PeliculasCines != null)
			{
				foreach (var peliculaCines in pelicula.PeliculasCines)
				{
					resultado.Add(new CineDTO()
					{
						Id=peliculaCines.CineId,
						Nombre=peliculaCines.Cine.Nombre,
						Latitud=peliculaCines.Cine.Ubicacion.Y,
						Longitud=peliculaCines.Cine.Ubicacion.X
					});
				}
			}
			return resultado;
		}

		private List<PeliculaActorDTO> MappearPeliculaActores(Peliculas pelicula, PeliculaDTO peliculaDTO)
		{
			var resultado = new List<PeliculaActorDTO>();

			if (pelicula.PeliculasActores != null)
			{
				foreach (var actorPelicula in pelicula.PeliculasActores)
				{
					resultado.Add(new PeliculaActorDTO() { Id = actorPelicula.ActorId,
						Nombre= actorPelicula.Actor.Nombre,
						Foto = actorPelicula.Actor.Foto,
						Orden=actorPelicula.Orden,
						Personaje=actorPelicula.Personaje
					});
				}
			}
			return resultado;
		}
		private List<GenerosDTO> MappearPeliculaGeneros(Peliculas pelicula, PeliculaDTO peliculaDTO)
		{
			var resultado = new List<GenerosDTO>();

			if(pelicula.PeliculasGeneros != null)
			{
				foreach(var genero in pelicula.PeliculasGeneros)
				{
					resultado.Add(new GenerosDTO() { Id = genero.GeneroId, 
						Nombre = genero.Genero.Nombre
					});
				}
			}
			return resultado;
		}
		private List<PeliculasActores> MapearPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO,
			Peliculas pelicula)
		{
			var resultado = new List<PeliculasActores>();

			if (peliculaCreacionDTO.Actores == null) { return resultado; }

			foreach (var actor in peliculaCreacionDTO.Actores)
			{
				resultado.Add(new PeliculasActores() { ActorId = actor.Id,Personaje=actor.Personaje });
			}
			return resultado;
		}

		private List<PeliculasGeneros> MapearPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO,
			Peliculas pelicula)
		{
			var resultado = new List<PeliculasGeneros>();

			if(peliculaCreacionDTO.GenerosIds == null) { return resultado; }

			foreach(var id in peliculaCreacionDTO.GenerosIds)
			{
				resultado.Add(new PeliculasGeneros() { GeneroId = id });
			}
			return resultado;
		}

		private List<PeliculasCines> MapearPeliculasCines(PeliculaCreacionDTO peliculaCreacionDTO,
			Peliculas pelicula)
		{
			var resultado = new List<PeliculasCines>();

			if (peliculaCreacionDTO.CinesIds == null) { return resultado; }

			foreach (var id in peliculaCreacionDTO.CinesIds)
			{
				resultado.Add(new PeliculasCines() { CineId = id });
			}
			return resultado;
		}
	}
}
