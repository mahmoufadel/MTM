using MTM.Domain.Inventories.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Domain.Model
{
	public class Inventory
	{
		[Key]
		public Guid Id { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		
	}
}
