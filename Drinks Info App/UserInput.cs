namespace Drinks_Info_App
{
    internal class UserInput
    {
        DrinksService drinksService = new();

        // Method to get the categories input from the user
        public void GetCategoriesInput()
        {
            // Retrieve and display categories
            drinksService.GetCategories();
            Console.WriteLine("Choose Category");

            // Variable to store the user input and a flag for input validity
            string? readResult = null;
            bool validResult = false;
            string categorySelection = "";

            // Loop until valid input is received
            while (!validResult)
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    categorySelection = readResult.Trim();
                    validResult = true;
                }
            }

            // Get the drinks for the selected category
            GetDrinksInput(categorySelection);
        }

        // Method to get the drinks input from the user for a selected category
        private void GetDrinksInput(string category)
        {
            // Retrieve and display drinks for the selected category
            var drinks = drinksService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or go back to category menu by typing 0:");
            string? readResult = null;
            bool validResult = false;
            string drinkSelection = "";

            // Loop until valid input is received
            while (!validResult)
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    drinkSelection = readResult.Trim();
                    validResult = true;
                }
            }

            // If the user wants to go back to the categories menu
            if (drinkSelection == "0") GetCategoriesInput();

            // If the selected drink does not exist
            if (!drinks.Any(x => x.idDrink == drinkSelection))
            {
                Console.WriteLine("Drink doesn't exist.");
                GetDrinksInput(category);
            }

            // Retrieve and display details of the selected drink
            drinksService.GetDrink(drinkSelection);

            // Prompt the user to go back to categories menu
            Console.WriteLine("Press any key to go back to categories menu");
            Console.ReadKey();
            if (!Console.KeyAvailable) GetCategoriesInput();
        }
    }
}