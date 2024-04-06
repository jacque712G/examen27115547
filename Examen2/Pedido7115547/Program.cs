using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pedido7115547.Context;
using Pedido7115547.Contratos;
using Pedido7115547.Implementacion;

var host = new HostBuilder()
	.ConfigureFunctionsWebApplication()
	.ConfigureServices(services =>
	{
		var configuration = new ConfigurationBuilder()
	   .SetBasePath(Directory.GetCurrentDirectory())
	   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
	   .AddEnvironmentVariables()
	   .Build();

		services.AddApplicationInsightsTelemetryWorkerService();
		services.ConfigureFunctionsApplicationInsights();
		services.AddDbContext<Contexto>(options => options.UseSqlServer(
				  configuration.GetConnectionString("cadenaConexion")));
		services.AddScoped<IProductoService, ProductoService>();
		services.AddScoped<IClienteService, ClienteService>();
		services.AddScoped<IPedidoService, PedidoService>();




	})
	.Build();

host.Run();
