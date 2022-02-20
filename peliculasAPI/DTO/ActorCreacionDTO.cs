using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.DTO
{
	public class ActorCreacionDTO
	{
		[Required]
		[StringLength(maximumLength: 200)]
		public string Nombre { get; set; }
		public string Biiografia { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public IFormFile Foto { get; set; }
	}
}
