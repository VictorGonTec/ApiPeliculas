using peliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.DTO
{
	public class GeneroCreacionDTO
	{
		[Required(ErrorMessage = "El campo {0} es requerido")]
		[StringLength(maximumLength: 50)]//longitud de la cadena
		[PrimeraLetraMayuscula]//validacion personalizada
		public string Nombre { get; set; }
	}
}
