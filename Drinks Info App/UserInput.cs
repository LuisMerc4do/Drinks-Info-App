namespace Drinks_Info_App
{
    internal class UserInput
    {
        DrinksService drinksService = new();

        public void GetCategoriesInput()
        {
            drinksService.GetCategories();
            Console.WriteLine("Choose Category");

            string? readResult = null;
            bool validResult = false;
            string categorySelection = "";
            while (!validResult)
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    categorySelection = readResult.Trim();
                    validResult = true; 
                }
            }
            GetDrinksInput(categorySelection);
        }

        private void GetDrinksInput(string category)
        {
            var drinks = drinksService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or go back to category menu by typing 0:");
            string? readResult = null;
            bool validResult = false;
            string drinkSelection = "";
            while (!validResult)
            {
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    drinkSelection = readResult.Trim();
                    validResult = true;
                }
            }
            if (drinkSelection == "0") GetCategoriesInput();

            if (!drinks.Any(x => x.idDrink == drinkSelection))
            {
                Console.WriteLine("Drink doesn't exist.");
                GetDrinksInput(category);
            }


            drinksService.GetDrink(drinkSelection);

            Console.WriteLine("Press any key to go back to categories menu");
            Console.ReadKey();
            if (!Console.KeyAvailable) GetCategoriesInput();

        }
    }
}
