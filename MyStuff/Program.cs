using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inventory
{
    //************************************************************************
    // Title: Inventory
    // Application Type: framework – Console
    // Description: A General Purpose Inventory Tracker
    // Author: Joseph Wojcik
    // Date Created: 12-2-18
    // Last Modified: 12-5-18
    //************************************************************************


    class Program
    {
        static void Main(string[] args)
        {
            bool saved;

            saved = DisplayMainMenu();
            DisplayClosingScreen(saved);
        }

        /// <summary>
        /// Display Menu
        /// </summary>
        static bool DisplayMainMenu()
        {
            bool saved = true;
            string dataPath = @"Data\Data.txt";
            string menuChoice;
            bool loopRunning = true;
            List<Item> inventory;

            //
            // initialize inventory
            //
            inventory = InitializeInventory(dataPath);

            while (loopRunning)
            {
                DisplayInventory(inventory);

                Console.WriteLine("\t1) Add An Item");
                Console.WriteLine("\t2) Remove An Item");
                Console.WriteLine("\t3) Edit An Item");
                Console.WriteLine("\t4) Save Changes To File");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        DisplayAddItem(inventory);
                        DisplaySaveChanges(dataPath, inventory);
                        break;

                    case "2":
                        DisplayRemoveItem(inventory);
                        break;

                    case "3":
                        DisplaySaveChanges(dataPath, inventory);
                        DisplayEditItem(dataPath, inventory);
                        DisplaySaveChanges(dataPath, inventory);
                        break;

                    case "4":
                    case "s":
                        DisplaySaveChanges(dataPath, inventory);
                        break;

                    case "e":
                    case "E":
                        loopRunning = false;
                        break;

                    default:
                        break;
                }
            }
            DisplayHeader("Do You Want to Save Your Changes?");
            switch (Console.ReadLine().ToUpper())
            {
                case "NO":
                case "N":
                    saved = false;
                    break;
                default:
                    saved = true;
                    DisplaySaveChanges(dataPath, inventory);
                    break;
            }
            return saved;
        }

        #region EDITING METHODS

        /// <summary>
        /// Item Edit Method
        /// </summary>
        static void DisplayEditItem(string dataPath, List<Item> inventory)
        {
            string itemName;
            string propertyToEdit;

            itemName = DisplayItemToEdit(inventory);

            propertyToEdit = DisplayPropertyToEdit(dataPath, inventory);

            Console.WriteLine();

            DisplayAssignNewValue(inventory, itemName, propertyToEdit);

            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Get Name of Item to Edit
        /// </summary>
        static string DisplayItemToEdit(List<Item> inventory)
        {
            int itemNumber = 1;
            string userResponse;
            string itemName;

            Console.ForegroundColor = ConsoleColor.White;
            DisplayHeader("Edit an Item. Enter 'E' to Escape");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------".PadRight(25));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Name".PadRight(25) + "Quantity".PadRight(25));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------".PadRight(25));

            foreach (Item item in inventory)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(item.Name.PadRight(25));
                Console.WriteLine(item.Quantity.ToString().PadRight(25));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("---------------------------------".PadRight(25));
                itemNumber = itemNumber + 1;
            }
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.Write("What Item do You Want to Edit? Enter This Exactly - There is no Verification on Your Response: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            userResponse = Console.ReadLine().ToUpper();
            if (userResponse == "E")
            {
                DisplayMainMenu();
            }

            itemName = userResponse.ToUpper();
            return itemName;
        }

        /// <summary>
        /// Find Out What Property to Edit
        /// </summary>
        static string DisplayPropertyToEdit(string dataPath, List<Item> inventory)
        {
            string propertyToEdit = null;
            string userResponse;
            bool loopRunning = true;

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                DisplayHeader("Choose Property to Edit");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1) Name");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("2) Location");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3) Quantity");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("4) Value");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("-----------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.Write("What Property do You Want to Change?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                userResponse = Console.ReadLine().ToUpper();

                switch (userResponse)
                {
                    case "1":
                        propertyToEdit = "Name";
                        loopRunning = false;
                        break;

                    case "2":
                        propertyToEdit = "Location";
                        loopRunning = false;
                        break;

                    case "3":
                        propertyToEdit = "Quantity";
                        loopRunning = false;
                        break;

                    case "4":
                        propertyToEdit = "Value";
                        loopRunning = false;
                        break;

                    default:
                        break;
                }
            } while (loopRunning);

            return propertyToEdit;
        }

        /// <summary>
        /// Get New Value and Assign it
        /// </summary>
        static void DisplayAssignNewValue(List<Item> inventory, string itemName, string propertyToEdit)
        {
            string newValue;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Enter the New " + propertyToEdit + " of the Item: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            newValue = Console.ReadLine().ToUpper();

            foreach (Item item in inventory)
            {
                if (item.Name == itemName)
                {
                    switch (propertyToEdit)
                    {
                        case "Name":
                            item.Name = newValue;
                            break;

                        case "Location":
                            item.Location = newValue;
                            break;

                        case "Quantity":
                            item.Quantity = Convert.ToInt32(newValue);
                            break;

                        case "Value":
                            item.Value = Convert.ToDouble(newValue);
                            break;
                        default:
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("New Value Assigned");
                    break;
                }

            }
        }

        #endregion

        /// <summary>
        /// Save Inventory to .txt File
        /// </summary>
        static void DisplaySaveChanges(string dataPath, List<Item> inventory)
        {
            string characterString;

            List<string> charactersStringListWrite = new List<string>();

            // build the list to write to the text file line by line
            foreach (Item item in inventory)
            {
                characterString =
                    item.Name + "," +
                    item.Location + "," +
                    item.Quantity + "," +
                    item.Value;

                charactersStringListWrite.Add(characterString);
            }

            File.WriteAllLines(dataPath, charactersStringListWrite);
        }

        /// <summary>
        /// Add Item
        /// </summary>
        static void DisplayAddItem(List<Item> inventory)
        {
            double value = 0;
            int quantity = 0;
            Item newItem = new Item();
            Console.ForegroundColor = ConsoleColor.White;

            DisplayHeader("Add An Item");

            // set all properties
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Enter Name of Item: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            newItem.Name = Console.ReadLine().ToUpper();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Enter Location of Item: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            newItem.Location = Console.ReadLine().ToUpper();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Enter Value of Item: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
            } while (!double.TryParse(Console.ReadLine(), out value));
            newItem.Value = value;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("How Many?: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
            } while (!int.TryParse(Console.ReadLine(), out quantity));
            newItem.Quantity = quantity;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");


            // add house item to inventory list
            inventory.Add(newItem);

            Console.WriteLine();
            Console.WriteLine(newItem.Name + " Has Been Added to Inventory");

            DisplayContinuePrompt();

        }

        /// <summary>
        /// Display Inventory
        /// </summary>
        static void DisplayInventory(List<Item> inventory)
        {
            int itemNumber = 1;
            double total = 0;
            Console.ForegroundColor = ConsoleColor.White;
            DisplayHeader("\t\t\tInventory");

            // Distplay column headers
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Name: Location".PadRight(30) + "Quantity".PadRight(20) + "Idividual Value".PadRight(20) + "Total Value".PadLeft(15));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------".PadRight(25) + "--------------------------------------------------------".PadLeft(10));
            Console.ForegroundColor = ConsoleColor.Red;

            foreach (Item item in inventory)
            {
                Console.Write(item.StuffInfo().PadRight(30));
                Console.WriteLine(item.Quantity.ToString().PadRight(20) + item.Value.ToString("C2").PadRight(20) + (item.Value * item.Quantity).ToString("C2").PadLeft(15));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("---------------------------------".PadRight(25) + "--------------------------------------------------------".PadLeft(10));
                Console.ForegroundColor = ConsoleColor.Red;
                total = item.Value * item.Quantity + total;
                itemNumber = itemNumber + 1;
            }

            Console.WriteLine("Total Inventory Value" + total.ToString("C2").PadLeft(64));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Remove an Item
        /// </summary>
        static void DisplayRemoveItem(List<Item> inventory)
        {
            string userResponse;
            bool removeOrNot = false;
            int itemNumber = 1;
            int itemToRemove = 1;
            Console.ForegroundColor = ConsoleColor.White;
            DisplayHeader("Remove an Item. Enter 'E' to Escape");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Name".PadRight(25) + "Quantity".PadRight(25));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------".PadRight(25));

            foreach (Item item in inventory)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(itemNumber + ") " + item.Name.PadRight(25));
                Console.WriteLine(item.Quantity.ToString().PadRight(25));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("---------------------------------".PadRight(25));
                itemNumber = itemNumber + 1;
            }
            Console.WriteLine();

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("What Item Do You Want to Remove? (Integer on List): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                userResponse = Console.ReadLine().ToUpper();
                if (userResponse == "E")
                {
                    break;
                }
                removeOrNot = int.TryParse(userResponse, out itemToRemove);
            } while (!removeOrNot);

            if (removeOrNot)
            {
                inventory.RemoveAt(itemToRemove - 1);
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Instantiate Inventory From File
        /// </summary>
        static List<Item> InitializeInventory(string dataPath)
        {
            List<Item> inventory = new List<Item>();

            const char delineator = ',';

            List<string> CharacterStringList = new List<string>();

            // read each line and put it into an array and convert the array to a list
            CharacterStringList = File.ReadAllLines(dataPath).ToList();

            foreach (string characterString in CharacterStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = characterString.Split(delineator);

                inventory.Add(
                    new Item()
                    {
                        Name = properties[0],
                        Location = properties[1],
                        Quantity = Convert.ToInt32(properties[2]),
                        Value = double.Parse(properties[3]),
                    });
            }


            return inventory;
        }

        #region HELPER METHODS

        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + header);
            Console.WriteLine();
        }

        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void DisplayOpeningScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\t\tStuff Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayClosingScreen(bool saved)
        {
            Console.Clear();
            if (saved == true)
            {
                Console.WriteLine("Good Choice!");
            }
            else
            {
                Console.WriteLine("You Should've Saved Your Work");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tGood Bye");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #endregion
    }


    class Item
    {
        #region FIELDS

        private string _name;
        // location is an un-verified string
        // this is just so i don't have to add a new enum location everytime i have a new location
        private string _location;
        private int _quantity;
        private double _value;

        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        #region METHODS

        public string StuffInfo()
        {
            string stuffInfo;

            stuffInfo = _name + ": " + _location;

            return stuffInfo;
        }

        #endregion
    }
}
