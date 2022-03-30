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

        /// <summary>
        /// Simulates a customer cancelling the order and getting his money back.
        /// </summary>
        /// <param name="cashPaid"></param>
        /// <returns></returns>
        public string CancelOrder(int cashPaid)
        {
            return VendingMachine.CancelOrder(cashPaid);
        }

        /// <summary>
        /// Simulates customer ordering a specific product after having inserted a specific amount of cash into the vending machine.
        /// </summary>
        /// <param name="wareIndex"></param>
        /// <param name="cashPaid"></param>
        /// <returns></returns>
        public string MakeOrder(int wareIndex, int cashPaid)
        {
            return VendingMachine.ProcessOrder(wareIndex, cashPaid);
        }
    }
}
