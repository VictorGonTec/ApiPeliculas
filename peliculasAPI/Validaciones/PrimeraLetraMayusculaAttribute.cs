using System.ComponentModel.DataAnnotations;

namespace peliculasAPI.Validaciones
{
	//validacion personalizada
	public class PrimeraLetraMayusculaAttribute:ValidationAttribute
	{
		protected override ValidationResult IsValid(object value,ValidationContext validationContext)
		{
			//validamos que el dato no sea requerido debido a que Required ya lo hace 
			if (value == null || string.IsNullOrEmpty(value.ToString()))
			{
				return ValidationResult.Success;
			}

			//obtenemos la primera letra de la cadena
			var primeraLetra = value.ToString()[0].ToString();

			if (primeraLetra != primeraLetra.ToUpper())
			{
				return new ValidationResult("La primera letra debe ser Mayuscula");
			}

			return ValidationResult.Success;
		}
	}
}
