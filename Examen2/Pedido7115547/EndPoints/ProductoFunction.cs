using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pedido7115547.Contratos;
using Pedido7115547.Models;
using System.Net;

namespace Pedido7115547.EndPoints
{
    public class ProductoFunction
    {
        private readonly ILogger<ProductoFunction> _logger;
		private readonly IProductoService repos;

		public ProductoFunction(ILogger<ProductoFunction> logger,IProductoService repos)
        {
            _logger = logger;
			this.repos = repos;
		}
		[Function("ListarProductosRangosFecha")]
		[OpenApiOperation("Listarspec", "ListarProductosRangosFecha", Description = "Sirve para listar todos los Productos")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Producto>), Description = "Mostrara una Lista de Producto")]
		[OpenApiParameter(name: "fecha1", In = ParameterLocation.Path, Required = true, Type = typeof(DateTime))]
		[OpenApiParameter(name: "fecha2", In = ParameterLocation.Path, Required = true, Type = typeof(DateTime))]
		public async Task<HttpResponseData> ListarProductosRangosFecha([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProductosRangosFecha/{fecha1}/{fecha2}")] HttpRequestData req,DateTime fecha1,DateTime fecha2)
		{
			try
			{
				var listaIdiomas = repos.ListarProductosFechas(fecha1,fecha2);
				var respuesta = req.CreateResponse(HttpStatusCode.OK);
				await respuesta.WriteAsJsonAsync(listaIdiomas.Result);
				return respuesta;
			}
			catch (Exception e)
			{

				var error = req.CreateResponse(HttpStatusCode.InternalServerError);
				await error.WriteAsJsonAsync(e.Message);
				return error;
			}
		}

		[Function("ListarProductos")]
		[OpenApiOperation("Listarspec", "ListarProductos", Description = "Sirve para listar todos los Productos")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Producto>), Description = "Mostrara una Lista de Producto")]
		public async Task<HttpResponseData> ListarProductos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProductos")] HttpRequestData req)
		{			
			try
			{
				var listaIdiomas = repos.ListarProductos();
				var respuesta = req.CreateResponse(HttpStatusCode.OK);
				await respuesta.WriteAsJsonAsync(listaIdiomas.Result);
				return respuesta;
			}
			catch (Exception e)
			{

				var error = req.CreateResponse(HttpStatusCode.InternalServerError);
				await error.WriteAsJsonAsync(e.Message);
				return error;
			}
		}

		[Function("InsertarProducto")]
		[OpenApiOperation("Insertarspec", "InsertarProducto", Description = "Sirve para Insertar un Producto")]
		[OpenApiRequestBody("application/json", typeof(Producto), Description = "Producto modelo")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Producto), Description = "Mostrara el Producto Creado")]
		public async Task<HttpResponseData> InsertarProducto([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarProducto")] HttpRequestData req)
		{			
			try
			{
				var producto = await req.ReadFromJsonAsync<Producto>() ?? throw new Exception("Debe ingresar un Producto con todos sus datos");
				bool seGuardo = await repos.InsertarProducto(producto);
				if (seGuardo)
				{
					var respuesta = req.CreateResponse(HttpStatusCode.OK);
					return respuesta;
				}
				return req.CreateResponse(HttpStatusCode.BadRequest);
			}
			catch (Exception e)
			{

				var error = req.CreateResponse(HttpStatusCode.InternalServerError);
				await error.WriteAsJsonAsync(e.Message);
				return error;
			}
		}

	}
}
