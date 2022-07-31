﻿using PackIT.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Exceptions
{
    public class PackingItemNotFoundException : PackITException
    {
        public PackingItemNotFoundException(string itemName) : base($"Packing Item {itemName} was not found.")
        {
            ItemName = itemName;
        }

        public string ItemName { get; }
    }
}
