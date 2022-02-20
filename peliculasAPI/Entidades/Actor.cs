﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.Entidades
{
	public class Actor
	{
		public int Id { get; set; }

		[Required]
		[StringLength(maximumLength:200)]
		public string Nombre { get; set; }
		public string Biiografia { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string Foto { get; set; }
		public List<PeliculasActores> PeliculasActores { get; set; }
	}
}
