﻿using System.Collections.Generic;
using MegaBuy.Calls.Callers;
using MegaBuy.PurchaseHistories;
using MegaBuy.PurchaseHistories.Data;
using MonoDragons.Core.Common;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class CallScenario
    {
        public Caller Caller { get; set; }
        public Product Product { get; set; }
        public Problem Problem { get; set; }
        public Optional<Purchase> Target { get; set; } 
        public IEnumerable<Purchase> Purchases { get; set; }
        public string ProductName => Product.Name;
        // @todo #1 Frontend: Display number in stock. Maybe should be attached to purchase
        public int NumInStock { get; set; } = 1;
    }
}
