using PackIT.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.ValueObjects
{
    public record PackingItem
    {
        public PackingItem(string name, uint quantity)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyPackingListItemException();
            }

            Name = name;
            Quantity = quantity;
           
        }

        public PackingItem(string name, uint quantity, bool isPacked) : this(name, quantity)
        {
          
            IsPacked = isPacked;
        }
        public string Name { get; }
        public uint Quantity { get; }
        public bool IsPacked { get; init; }
  
    }
}
