using System;

namespace Automaten
{
    public class Program
    {
        static VendingMachine machine;
        public static void Main(string[] args)
        {
            // Create machine for testing purposes

            Ware cola = new Ware("Cola", 10, 10);
            Ware fanta = new Ware("Fanta", 10, 10);
            Ware monster = new Ware("Monster", 10, 15);

            List<Ware> list = new List<Ware>();

            list.Add(cola);
            list.Add(fanta);
            list.Add(monster);

            machine = new VendingMachine(1000, list);

            // Main menu text
            string mainMenu = "==============================\n";
            mainMenu += "  1. Purchase product\n";
            mainMenu += "\n";
            mainMenu += "  9. Access Admin Menu\n"; // TODO: This should probably be locked behind a password or something.
            mainMenu += "  X. Shut down vending machine\n";
            mainMenu += "==============================\n";

            // Admin menu text
            string adminMenu = "==============================\n";
            adminMenu += "  1. Add new product\n";
            adminMenu += "  2. Restock product\n";
            adminMenu += "  3. Refill cash\n";
            adminMenu += "  4. Edit product name\n";
            adminMenu += "  5. Edit product price\n";
            adminMenu += "\n";
            adminMenu += "  X. Exit admin menu\n";
            adminMenu += "==============================\n";

            do
            {
                Console.Clear();
                Console.WriteLine(mainMenu);

                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    // Purchase product
                    case "1":
                        int wareIndex;
                        int cashPaid;

                        Console.Clear();

                        // Simulate user inputting money by manually entering the amount of money the user has put into the machine.
                        Console.WriteLine("How much cash do you wish to insert into the machine?: ");

                        if (int.TryParse(Console.ReadLine(), out cashPaid))
                        { 

                            do
                            {
                                // Show purchasable wares
                                Console.WriteLine("Choose item to purchase: \n");

                                Console.WriteLine(machine.ListWares());

                                // Get ware to purchase
                                input = Console.ReadLine();

                            } while (!int.TryParse(input, out wareIndex) && machine.Inventory[wareIndex] != null);

                            // Clear console
                            Console.Clear();

                            // Give user the option to cancel the order
                            Console.WriteLine($"You are about to purchase {machine.Inventory[wareIndex].Name} for {machine.Inventory[wareIndex].Price}.\nDo you wish to proceed? (Y/N)");

                            // User must reply yes or no to the cancel prompt
                            do
                            {
                                input = Console.ReadLine();
                            } while (input.ToLower() != "y" && input.ToLower() != "n");

                            Console.Clear();

                            // If user wishes to cancel, cancel order, otherwise proceed with the order.
                            if (input == "n")
                                Console.WriteLine(machine.Customer.CancelOrder(cashPaid));
                            else
                                Console.WriteLine(machine.Customer.MakeOrder(wareIndex, cashPaid));

                            Console.ReadKey();
                        }

                        break;
                    
                    // Show admin menu
                    case "9":

                        bool showAdminMenu = true;

                        do
                        {
                            Console.Clear();

                            Console.WriteLine(adminMenu);

                            input = Console.ReadLine();

                            switch (input)
                            {
                                // Add new product
                                case "1":
                                    string wareName = "";
                                    int warePrice = 0;
                                    int wareAmount = 0;

                                    Console.Clear();
                                    
                                    // Get product name
                                    Console.WriteLine("Enter the name of the product to add: ");
                                    wareName = Console.ReadLine();

                                    // Get product price
                                    Console.WriteLine($"Enter the price of {wareName}: ");
                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out warePrice));

                                    // Get initial stock amount of product
                                    Console.WriteLine($"Enter amount of {wareName} you wish to stock now (or 0): ");
                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out wareAmount));

                                    Console.Clear();
                                    Console.WriteLine(machine.Admin.AddWare(wareName, wareAmount, warePrice));

                                    Console.ReadKey();
                                    break;

                                // Restock product
                                case "2":
                                    Console.Clear();

                                    // Choose product to restock
                                    Console.WriteLine("Which product do you want to restock?");
                                    Console.WriteLine(machine.ListWares());

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out wareIndex) && machine.Inventory[wareIndex] != null); // I can use wareIndex (and wareAmount) here because it is declared under "case 1:" in the first switch case

                                    // Choose how many to add to the machine
                                    Console.WriteLine("How many do you want to add?\n");

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out wareAmount)); // Similarly, I can use wareAmount here because it is declared in case 1: of this switch case.

                                    // TODO: Perhaps declare these variables, that are used multiple times (wareIndex and wareAmount), somewhere it reads more intuitively.

                                    Console.Clear();

                                    // Restock the machine
                                    Console.WriteLine(machine.Admin.RestockWare(wareIndex, wareAmount));

                                    Console.ReadKey();
                                    break;

                                // Refill cash
                                case "3":
                                    int cashAmount;

                                    Console.Clear();
                                    Console.WriteLine("How much cash do you wish to add to the machines cash balance?");

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out cashAmount));

                                    Console.Clear();

                                    Console.WriteLine(machine.Admin.RefillCash(cashAmount));

                                    Console.ReadKey();
                                    break;
                                
                                // Edit product name
                                case "4":
                                    Console.Clear();

                                    Console.WriteLine("Choose the product that you want to change the name of: ");
                                    Console.WriteLine(machine.ListWares());

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out wareIndex) && machine.Inventory[wareIndex] != null);

                                    Console.WriteLine($"Choose {machine.Inventory[wareIndex]}'s new name: ");
                                    input = Console.ReadLine();

                                    Console.Clear();

                                    Console.WriteLine(machine.Admin.EditWare(wareIndex, input));

                                    Console.ReadKey();
                                    break;

                                // Edit product price
                                case "5":
                                    Console.Clear();

                                    Console.WriteLine("Choose the product that you want to change the price of: ");
                                    Console.WriteLine(machine.ListWares());

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out wareIndex) && machine.Inventory[wareIndex] != null);

                                    Console.WriteLine($"Choose {machine.Inventory[wareIndex]}'s new price (current price: {machine.Inventory[wareIndex].Price}): ");

                                    do
                                        input = Console.ReadLine();
                                    while (!int.TryParse(input, out warePrice));

                                    Console.Clear();

                                    Console.WriteLine(machine.Admin.EditWare(wareIndex, warePrice));
                                    Console.ReadKey();
                                    break;

                                // Exit admin menu
                                default:
                                    showAdminMenu = false;
                                    break;

                            }

                        } while (showAdminMenu);

                        break;
                    
                    // Exit program
                    case "x":
                        
                        Environment.Exit(0);
                        break; // Otherwise, I get "Can't fall through from one case label to another", although this break will never be reached as Exit() shuts down the application.
                    
                    // If user chooses an invalid option, do nothing.
                    default:
                        break;
                }
            } while (true); // TODO: Could change this condition to one that actually allows this program to break out of this loop without having to call Exit()/Environment.Exit()
        }
    }
}