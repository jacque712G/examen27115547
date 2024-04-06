using Microsoft.EntityFrameworkCore;
using Pedido7115547.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Context
{
	public class Contexto : DbContext
	{
		public Contexto(DbContextOptions<Contexto> options) : base(options)
		{
		}
		public virtual DbSet<Producto> Productos { get; set; }
		public virtual DbSet<Cliente> Clientes { get; set; }
		public virtual DbSet<Pedido> Pedidos { get; set; }
		public virtual DbSet<Detalle> Detalles { get; set; }

	}
}
