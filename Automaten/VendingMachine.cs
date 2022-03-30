using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaten
{
    internal class VendingMachine
    {
        public Admin Admin { get; private set; }
        public Customer Customer { get; private set; }
        public int CashBalance { get; set; }
        public List<Ware> Inventory { get; set; }

        public VendingMachine(int cashBalance, List<Ware> inventory)
        {
            CashBalance = cashBalance;
            Inventory = inventory;

            Admin = new Admin(this);
            Customer = new Customer(this);
        }

        /// <summary>
        /// Processes a customers order.
        /// </summary>
        /// <param name="wareIndex"></param>
        /// <param name="cashPaid"></param>
        /// <returns>String informing the user of the status of the transaction</returns>
        public string ProcessOrder(int wareIndex, int cashPaid)
        {
            // Get the purchased ware from the inventory
            Ware ware = Inventory[wareIndex];

            // Check that customer has inserted enough cash to pay for the ware
            if (!(cashPaid >= ware.Price))
                return $"The product price is {ware.Price}. Please insert another {ware.Price - cashPaid}";

            // Simulate giving the user the product by subtracting the purchased ware from the inventory.
            GiveProduct(wareIndex);

            // Add the cash the customer paid to the vending machines cash balance.
            CashBalance += cashPaid;

            // Get the amount of change the user will get back.
            int change = GiveChange(ware.Price, cashPaid);

            // Tell user purchase succeeded and how much change they're getting back.
            return $"Purchase of {ware} successful.\nChange: {change}.";
        }

        /// <summary>
        /// Customer cancels order, so he gets his money back before it enters the vending machines cash balance.
        /// </summary>
        /// <param name="cashPaid"></param>
        /// <returns>String informing the user of how much cash they should get back.
        /// </returns>
        public string CancelOrder(int cashPaid)
        {
            return $"Your order was cancelled. {cashPaid}DKK returned to customer.";
        }

        /// <summary>
        /// Subtracts ware from inventory, simulating the product leaving the machine.
        /// </summary>
        /// <param name="wareIndex"></param>
        private void GiveProduct(int wareIndex)
        {
            Inventory[wareIndex].Amount--;
        }

        /// <summary>
        /// Subtract change from vending machines cash balance.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="cashPaid"></param>
        /// <returns>How much change was subtracted from the cashbalance.</returns>
        private int GiveChange(int price, int cashPaid)
        {
            CashBalance -= (cashPaid - price);
            return (cashPaid - price);
        }
    }
}
