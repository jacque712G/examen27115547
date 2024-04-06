using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedido7115547.Models
{
	public class Cliente
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "El campo Nombre Cliente es requerido")]
		[StringLength(maximumLength: 100)]
		public string NombreCliente{ get; set; } = null!;

		[Required(ErrorMessage = "El campo Apellido es requerido")]
		[StringLength(maximumLength: 100)]
		public string Apellido { get; set; } = null!;
	}
}
