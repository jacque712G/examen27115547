using Microsoft.EntityFrameworkCore;
using Pedido7115547.Context;
using Pedido7115547.Contratos;
using Pedido7115547.DTO;
using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Implementacion
{
	public class PedidoService : IPedidoService
	{
     	private readonly Contexto contexto;
		public PedidoService(Contexto _contexto)
		{
			this.contexto = _contexto;
		}
		public async Task<bool> InsertarPedido(PedidoDTO pedido)
		{
			bool sw = false;
			Pedido ped = new Pedido();
			ped.IdCliente = pedido.IdCliente;
			ped.Fecha = pedido.Fecha;
			ped.Total = pedido.Total;
			ped.Estado = pedido.Estado;
			ped.Cliente = null;
			ped.Detalles = null;
			contexto.Pedidos.Add(ped);
			contexto.SaveChanges();
			var idPedido = ped.Id;			
			foreach (var item in pedido.Detalles)
			{
				Detalle detalle = new Detalle();
				detalle.IdPedido = idPedido;
				detalle.IdProducto = item.IdProducto;
				detalle.Cantidad = item.Cantidad;
				detalle.Precio = item.Precio;
				detalle.Subtotal=item.Subtotal;
				detalle.Producto = null;
				detalle.Pedido = null;
				contexto.Detalles.Add(detalle);
			}
			int response = await contexto.SaveChangesAsync();
			if (response >= 1)
			{
				sw = true;
			}
			return sw;

		}

		public async Task<List<Pedido>> ListarPedidos()
		{
			var lista = await contexto.Pedidos.ToListAsync();
			return lista;

		}

		public async Task<bool> ModificarPedido(PedidoDTO pedido, int id)
		{
			bool sw = false;
			Pedido? existe = await contexto.Pedidos.FindAsync(id);
			if (existe != null)
			{
				existe.IdCliente = pedido.IdCliente;
				existe.Fecha = pedido.Fecha;
				existe.Total = pedido.Total;
				existe.Estado = pedido.Estado;
				contexto.SaveChanges();
				foreach (var item in pedido.Detalles)
				{
					Detalle? existeDet= await contexto.Detalles.FindAsync(item.Id);
					if (existeDet!=null)
					{
						existeDet.IdProducto = item.IdProducto;
						existeDet.Cantidad = item.Cantidad;
						existeDet.Precio = item.Precio;
						existeDet.Subtotal = item.Subtotal;
					}
					

				}
				await contexto.SaveChangesAsync();
				sw = true;
			}
			return sw;

		}

		public async  Task<Pedido> ObtenerPedidoById(int id)
		{
			Pedido? pedido = await contexto.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
			return pedido!;
		}
		public async Task<List<Reporte1DTO>> ListarReporte1()
		{
			var lista = await contexto.Detalles
					   .Include(prod => prod.Producto)
					   .Include(ped=>ped.Pedido)					   
				.Select(x =>
			new Reporte1DTO
			{
				NombreCliente=x.Pedido!.Cliente!.NombreCliente,
				FechaPedido=x.Pedido.Fecha,
				NombreProducto=x.Producto!.NombreProducto,
				Subtotal=x.Subtotal

			}).ToListAsync();
			return lista;
		}
		public async Task<List<Reporte2DTO>> ListarReporte2()
		{
			var lista = await contexto.Detalles
					   .Include(prod => prod.Producto)
					   .GroupBy(p => p.IdProducto)
					   .OrderByDescending(c => c.Count())
				.Select(x =>
			new Reporte2DTO
			{
				NombreProducto = x.First().Producto!.NombreProducto,
				Cantidad = x.Count()
			}).Take(3).ToListAsync();
			return lista;
		}	
		
	}
}
