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
            {
                // Give customer their money back - although it doesn't practically do anything in this case, it is still important that the customer gets their money back upon an unsuccesful transaction.
                CancelOrder(cashPaid);
                return $"The product price is {ware.Price}. Please insert the full amount before proceeding with the purchase.";
            }

            // Simulate giving the user the product by subtracting the purchased ware from the inventory.
            GiveProduct(wareIndex);

            // Add the cash the customer paid to the vending machines cash balance.
            CashBalance += cashPaid;

            // Get the amount of change the user will get back.
            int change = GiveChange(ware.Price, cashPaid);

            // Tell user purchase succeeded and how much change they're getting back.
            return $"Purchase of {ware.Name} successful.\nChange: {change}.";
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

        public string ListWares()
        {
            string wares = "";

            for (int i = 0; i < Inventory.Count; i++)
            {
                // Only list those wares that are actually present in the machine as choices.
                if (Inventory[i].Amount > 0)
                    wares += i + ". " + Inventory[i] + "\n";
            }

            return wares;
        }
    }
}
