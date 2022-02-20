using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace peliculasAPI.Utilidades
{
	public static class HttpContextExtensions
	{
		public async static Task InsertaParametrosPaginacionEnCabecera<T>(
			this HttpContext httpContext,
			IQueryable<T> queryable)
		{
			if(httpContext == null) 
			{ 
				throw new ArgumentNullException(nameof(httpContext));
			}

			//contamos la cantidad de registros de una tabla
			double cantidad = await queryable.CountAsync();
			httpContext.Response.Headers.Add
				("cantidadTotalRegistro", cantidad.ToString());
		}
	}
}
