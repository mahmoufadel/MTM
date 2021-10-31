using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MTM.Domain.Model
{
	public class InventoryTrans
	{
		
		[Key]
		public Guid TransId { get; set; }
		public Guid InventoryId { get; set; }

		public virtual Inventory inventory { get; set; }

		public int Qunatity { get; set; }

		public decimal Price { get; set; }
	}
}
