using Microsoft.EntityFrameworkCore;
using Pedido7115547.Context;
using Pedido7115547.Contratos;
using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Implementacion
{
	public class ProductoService : IProductoService
	{
		private readonly Contexto contexto;
		public ProductoService(Contexto _contexto)
		{
			this.contexto = _contexto;
		}

		public async Task<bool> InsertarProducto(Producto producto)
		{
			bool sw = false;
			contexto.Productos.Add(producto);
			int response = await contexto.SaveChangesAsync();
			if (response == 1)
			{
				sw = true;
			}
			return sw;

		}

		public async Task<List<Producto>> ListarProductos()
		{
			var lista = await contexto.Productos.ToListAsync();
			return lista;

		}

		public async Task<List<Producto>> ListarProductosFechas(DateTime fecha1, DateTime fecha2)
		{
			var lista = await contexto.Detalles
						.Where(x => x.Pedido!.Fecha >= fecha1 && x.Pedido.Fecha <= fecha2)
						.OrderByDescending(y=>y.Cantidad)
						.Select(prod=>new Producto 
						{ 
							Id = prod.Producto!.Id,
							NombreProducto=prod.Producto.NombreProducto
						})
						.ToListAsync();
			return lista;
		}
	}
}
