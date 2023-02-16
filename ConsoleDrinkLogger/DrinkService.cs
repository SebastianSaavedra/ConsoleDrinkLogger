using ConsoleDrinkLogger.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utils;

namespace ConsoleDrinkLogger
{
    internal class DrinkService
    {
        const string API_URL = "http://www.thecocktaildb.com/api/json/v1/1/";

        internal List<Category> GetCategories()
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);

            List<Category> categories = new List<Category>();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                Categories serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;

                TableVisualisationEngine.ShowTable(categories,"Categories Menu");
                return categories;
            }
            return categories;
        }

        internal List<Drink> GetDrinksByCategory(string category)
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest($"filter.php?c={HttpUtility.UrlEncode(category)}");
            var response = client.ExecuteAsync(request);

            List<Drink> drinks = new List<Drink>();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;

                Drinks serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                drinks = serialize.DrinksList;

                TableVisualisationEngine.ShowTable(drinks, "Drinks Menu");
                return drinks;
            }
            return drinks;
        }

        internal void GetDrink(string id)
        {
            var client = new RestClient(API_URL);
            var request = new RestRequest($"lookup.php?i={id}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                DrinkDetailObject serialize = JsonConvert.DeserializeObject<DrinkDetailObject>(rawResponse);

                List<DrinkDetail> drinkDetailsList = serialize.DrinkDetailList;

                TableVisualisationEngine.ShowTable(drinkDetailsList,"Categories Menu");
            }
        }
    }
}
