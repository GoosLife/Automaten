using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaten
{
    internal class Admin
    {
        private VendingMachine VendingMachine { get; set; }

        public Admin(VendingMachine vm)
        {
            VendingMachine = vm;
        }

        /// <summary>
        /// Add a new product to the vending machines inventory.
        /// </summary>
        /// <param name="name">Name of the product. Can not be the same as a product that already exists in the vending machines inventory.</param>
        /// <param name="amount">Amount of the product to add.</param>
        /// <param name="price">The new product's price.</param>
        /// <returns></returns>
        public string AddWare(string name, int amount, int price)
        {
            Ware newWare = new Ware(name, amount, price);

            // Check if similar product already exists
            List<Ware> similarWares = VendingMachine.Inventory.FindAll(w => w.Name == newWare.Name);

            // Prompt admin to use the correct menu to update product information // TODO: Maybe this could ask the admin if he wants to update the product and call the appropriate method without going to the menu.
            if (similarWares.Any())
                return $"Ware with name {newWare.Name} already exists. If you want to refill this product, or update its pricing or name, please use the appropriate command from the menu.";

            // Add new product to vending machines inventory.
            VendingMachine.Inventory.Add(newWare);

            // Inform admin of succesful action.
            return $"{newWare.Name} succesfully added to inventory. Press any key to continue.";
        }

        /// <summary>
        /// Simulates restocking vending machine by adding to the "Amount" variable of the specified product in the vending machines inventory.
        /// </summary>
        /// <param name="wareIndex"></param>
        /// <param name="amount"></param>
        /// <returns>String notifying the user that the restocking was succesful, and the amount of the product available after restocking.</returns>
        public string RestockWare(int wareIndex, int amount)
        {
            // Add to the amount of products in stock.
            VendingMachine.Inventory[wareIndex].Amount += amount;

            // Notify user that restocking has concluded, and show how many of the product are now in stock.
            return $"{VendingMachine.Inventory[wareIndex].Name} restocked.\nNew amount of product: {VendingMachine.Inventory[wareIndex].Amount}.\nPress any key to continue.";
        }
        
        /// <summary>
        /// Simulates adding more coins to the vending machines cash balance.
        /// </summary>
        /// <param name="amount">Amount of Danish Kroner that was added to the vending machines cash balance</param>
        /// <returns>String notifying the user that the cash has been inserted, and prints the vending machines currently available balance.</returns>
        public string RefillCash(int amount)
        {
            // Add amount to vending machines cash balance.
            VendingMachine.CashBalance += amount;

            // Print result of adding amount to the vending machines cash balance. // TODO: Could keep track of specific coins to make sure that giving change is possible.
            return $"Cash balance refilled.\nCash in machine: {VendingMachine.CashBalance}.\nPress any key to continue.";
        }

        /// <summary>
        /// Edit name of a product in the vending machines inventory.
        /// </summary>
        /// <param name="wareIndex"></param>
        /// <param name="name"></param>
        /// <returns>String notifying user of succesful change</returns>
        public string EditWare(int wareIndex, string name)
        {
            // Store products previous name.
            string previousName = VendingMachine.Inventory[wareIndex].Name;

            // Change products name to the new name.
            VendingMachine.Inventory[wareIndex].Name = name;

            // Notify and give user overview of the change that has been committed // TODO: Maybe give user option to cancel at this time in case of a mistake?
            return $"Product {previousName} succesfully renamed to {VendingMachine.Inventory[wareIndex].Name}.\nPress any key to continue.";
        }

        /// <summary>
        /// Change the price of a product in the vending machines inventory.
        /// </summary>
        /// <param name="wareIndex"></param>
        /// <param name="price"></param>
        /// <returns>String notifying the user of a succesful change.</returns>
        public string EditWare(int wareIndex, int price)
        {
            // Store products previous price.
            int previousPrice = VendingMachine.Inventory[wareIndex].Price;

            // Change price to new price.
            VendingMachine.Inventory[wareIndex].Price = price;

            // Notify user and give overview of the change that's been committed.
            return $"Product price changed from {previousPrice} to {VendingMachine.Inventory[wareIndex].Price}";
        }

        /// <summary>
        /// Simulate retrieving the cash from the vending machine by emptying the vending machines cash balance.
        /// </summary>
        /// <returns></returns>
        public string WithdrawCash()
        {
            // Sets cash balance to 0.
            VendingMachine.CashBalance = 0;

            // Output result. // TODO: Log this somewhere so owners have overview of when vending machine has been emptied.
            return $"Vending machine cash has been succesfully withdrawn. New cash balance is 0.\nWarning: Vending machine will not work until cash has been refilled.\nPress any key to continue.";
        }
    }
}
