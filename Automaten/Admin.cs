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
            return $"{newWare.Name} succesfully added to inventory.";
        }

        public string RefillWare(int wareIndex, int amount)
        {

        }
    }
}
