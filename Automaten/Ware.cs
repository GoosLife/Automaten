using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaten
{
    internal class Ware
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public Ware(string name, int amount, int price)
        {
            Name = name;
            Amount = amount;
            Price = price;
        }
    }
}
