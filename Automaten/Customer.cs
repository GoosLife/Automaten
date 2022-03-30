using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaten
{
    internal class Customer
    {
        private VendingMachine VendingMachine { get; set; }

        public Customer(VendingMachine vm)
        {
            VendingMachine = vm;
        }

        public string CancelOrder(int cashPaid)
        {
            return VendingMachine.CancelOrder(cashPaid);
        }

        public string MakeOrder(int wareIndex, int cashPaid)
        {
            return VendingMachine.ProcessOrder(wareIndex, cashPaid);
        }
    }
}
