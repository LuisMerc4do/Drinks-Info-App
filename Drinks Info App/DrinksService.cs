using Drinks_Info_App.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Web;


namespace Drinks_Info_App
{
    public class DrinksService
    {
        public List<Category>GetCategories()
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);
            List<Category> categories = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;

                TableVisualisationEngine.ShowTable(categories, "Categories Menu");
                return categories;
            }
            return categories;
        }
    }
    public List<Drink> GetDrinksByCategories(string category)
    {
        var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
        var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
        var response = client.ExecuteAsync(request);
        List<Drink> drinks = new();
        if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string rawResponse = response.Result.Content;
            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

            drinks = serialize.DrinksList;

            TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");

            return drinks;
        }
        return drinks;
    }

}
