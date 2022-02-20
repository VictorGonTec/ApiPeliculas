using peliculasAPI.Validaciones;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.Entidades
{
	public class Generos
	{
		public int Id { get; set; }

		[Required(ErrorMessage ="El campo {0} es requerido")]
		[StringLength(maximumLength:50)]//longitud de la cadena
		[PrimeraLetraMayuscula]//validacion personalizada
		public string Nombre { get; set; }
		public List<PeliculasGeneros> PeliculasGeneros { get; set; }

		//[Range(18,120)]//rango de edad
		//public int Edad { get; set; }

		//[CreditCard]//validacion para tardeta de credito
		//public string TarjetaDeCeredito { get; set; }

		//[Url]
		//public string URL { get; set; }

		//public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		//{
		//	if (!string.IsNullOrEmpty(Nombre))
		//	{
		//		var primeraLetra = Nombre[0].ToString();

		//		if (primeraLetra != primeraLetra.ToUpper())
		//		{
		//			yield return new ValidationResult("La primera letra debe ser Mayuscula",
		//				new string[] {nameof(Nombre)});
		//		}
		//	}
		//}
	}
}
