using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace peliculasAPI.Utilidades
{
	public interface IAlmacenadorArchivo
	{
		public Task BorrarArchivo(string ruta, string contenedor);
		public Task<string> EditarArchivo(string contenedor, IFormFile archivo, string ruta);
		public Task<string> GuardarArchivo(string contenedor, IFormFile archivo);
	}
}
