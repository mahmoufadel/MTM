using MTM.Domain.Inventories.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Application.Services.Inventory.Dto
{
	public class InventoryDto
	{
		public Guid Id { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }
	}
}
