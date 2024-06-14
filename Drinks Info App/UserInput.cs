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
                }
            }
            GetDrinksInput(categorySelection);
        }

        public void GetDrinksInput(string category)
        {
            drinksService.GetDrinksByCategories(category);
        }
    }
}
