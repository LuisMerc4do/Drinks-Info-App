using Drinks_Info_App.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection;
using System.Web;

namespace Drinks_Info_App
{
    public class DrinksService
    {
        // Method to get the list of categories
        public List<Category> GetCategories()
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);
            List<Category> categories = new();

            // If the response is successful
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;

                // Display the categories in a table
                TableVisualisationEngine.ShowTable(categories, "Categories Menu");
                return categories;
            }
            return categories;
        }

        // Method to get the list of drinks by category
        internal List<Drink> GetDrinksByCategory(string category)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
            var response = client.ExecuteAsync(request);
            List<Drink> drinks = new();

            // If the response is successful
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                drinks = serialize.DrinksList;

                // Display the drinks in a table
                TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");
                return drinks;
            }
            return drinks;
        }

        // Method to get details of a specific drink
        internal void GetDrink(string drink)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"lookup.php?i={drink}");
            var response = client.ExecuteAsync(request);

            // If the response is successful
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);
                List<DrinkDetail> returnedList = serialize.DrinkDetailList;
                DrinkDetail drinkDetail = returnedList[0];
                List<object> prepList = new();
                string formattedName = "";

                // Prepare the drink details for display
                foreach (PropertyInfo prop in drinkDetail.GetType().GetProperties())
                {
                    if (prop.Name.Contains("str"))
                    {
                        formattedName = prop.Name.Substring(3);
                    }
                    if (!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                    {
                        prepList.Add(new
                        {
                            Key = formattedName,
                            Value = prop.GetValue(drinkDetail)
                        });
                    }
                }

                // Display the drink details in a table
                TableVisualisationEngine.ShowTable(prepList, drinkDetail.strDrink);
            }
        }
    }
}