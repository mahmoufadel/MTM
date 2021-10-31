using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Domain.Inventories.ValueObjects
{
    public struct InventoryId
    {
        private readonly Guid _Id;

        public InventoryId(Guid taskId)
        {
            if (taskId.Equals(Guid.Empty))
                throw new ArgumentNullException($"Task Id does not have any value");

            _Id = taskId;
        }

        public Guid ToGuid()
        {
            return _Id;
        }
    }
}
