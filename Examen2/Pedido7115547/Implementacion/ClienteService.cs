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
	public class ClienteService : IClienteService
	{
		private readonly Contexto contexto;
		public ClienteService(Contexto _contexto)
		{
			this.contexto = _contexto;
		}

		public async Task<bool> EliminarCliente(int id)
		{
			bool sw = false;
			Cliente? existe = await contexto.Clientes.FindAsync(id);
			if (existe != null)
			{
				var existeListaPedidos = await contexto.Pedidos.Where(x=>x.IdCliente==existe.Id).ToListAsync();
				if (existeListaPedidos!=null)
				{
					foreach (var pedido in existeListaPedidos)
					{
						var existeDetalle = await contexto.Detalles.Where(x => x.IdPedido == pedido.Id).ToListAsync();
						if (existeDetalle!=null) 
						{
							foreach (var detalle in existeDetalle) 
							{
								contexto.Detalles.Remove(detalle);
							}
						}
						contexto.Pedidos.Remove(pedido);
					}
				}
				
				contexto.Clientes.Remove(existe);
				await contexto.SaveChangesAsync();
				sw = true;
			}
			return sw;

		}

		public async Task<bool> InsertarCliente(Cliente cliente)
		{
			bool sw = false;
			contexto.Clientes.Add(cliente);
			int response = await contexto.SaveChangesAsync();
			if (response == 1)
			{
				sw = true;
			}
			return sw;

		}

		public async Task<List<Cliente>> ListarClientes()
		{
			var lista = await contexto.Clientes.ToListAsync();
			return lista;

		}

		
	}
}
