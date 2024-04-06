using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pedido7115547.DTO;

namespace Pedido7115547.Models
{
	public class Pedido
	{
		[Key]
		public int Id { get; set; }
		public int IdCliente { get; set; }

		[Required(ErrorMessage = "El campo Fecha es requerido")]
		public DateTime Fecha { get; set; }

		[Required(ErrorMessage = "El campo Total es requerido")]
		[StringLength(maximumLength: 50)]
		public double Total { get; set; }	

		[Required(ErrorMessage = "El campo Estado es requerido")]
		[StringLength(maximumLength: 20)]
		public string Estado { get; set; } = null!;	
		public virtual List<DetalleDTO> Detalles { get; set; } = new List<DetalleDTO>();

		[ForeignKey("IdCliente")]
		public virtual Cliente? Cliente { get; set; } 

	}
}
