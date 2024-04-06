using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Contratos
{
	public interface IProductoService
	{
		public Task<bool> InsertarProducto(Producto producto);	
		public Task<List<Producto>> ListarProductos();
		public Task<List<Producto>> ListarProductosFechas(DateTime fecha1, DateTime fecha2);

	}
}
