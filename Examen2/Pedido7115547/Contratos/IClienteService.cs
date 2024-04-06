using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Contratos
{
	public interface IClienteService
	{
		public Task<bool> InsertarCliente(Cliente cliente);	
		public Task<bool> EliminarCliente(int id);
		public Task<List<Cliente>> ListarClientes();
		
	}
}
