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
    public class ClienteFunction
    {
        private readonly ILogger<ClienteFunction> _logger;
		private readonly IClienteService repos;

		public ClienteFunction(ILogger<ClienteFunction> logger,IClienteService repos)
        {
            _logger = logger;
			this.repos = repos;
		}
		[Function("ListarCliente")]
		[OpenApiOperation("Listarspec", "ListarCliente", Description = "Sirve para listar todos los clientes")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Cliente>), Description = "Mostrara una Lista de Clientes")]
		public async Task<HttpResponseData> ListarCliente([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ListarCliente")] HttpRequestData req)
		{
			
			try
			{
				var listaIdiomas = repos.ListarClientes();
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

		[Function("InsertarCliente")]
		[OpenApiOperation("Insertarspec", "InsertarIdioma", Description = "Sirve para Insertar un Cliente")]
		[OpenApiRequestBody("application/json", typeof(Cliente), Description = "Cliente modelo")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Cliente), Description = "Mostrara el Cliente Creado")]
		public async Task<HttpResponseData> InsertarCliente([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarCliente")] HttpRequestData req)
		{		
			try
			{
				var cliente = await req.ReadFromJsonAsync<Cliente>() ?? throw new Exception("Debe ingresar un Cliente con todos sus datos");
				bool seGuardo = await repos.InsertarCliente(cliente);
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

		[Function("EliminarCliente")]
		[OpenApiOperation("Eliminarspec", "EliminarCliente", Description = "Sirve para Eliminar un Cliente")]
		[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int))]
		public async Task<HttpResponseData> EliminarCliente([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "EliminarCliente/{id}")] HttpRequestData req, int id)
		{
			
			try
			{
				bool seElimino = await repos.EliminarCliente(id);
				if (seElimino)
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
