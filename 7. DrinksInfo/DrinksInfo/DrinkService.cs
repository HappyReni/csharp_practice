using DrinksInfo.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    public class DrinkService
    {
        public List<Category> GetCategories()
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);
            var categories = new List<Category>();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                categories = serialize.CategoriesList;
                UI.MakeTable(categories, "Categories Menu");

                return categories;
            }
            return categories;
        }
        public void GetDrinksByCategory(string category)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"filter.php?c={category}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);

                List<Drink> returnedList = serialize.DrinkList;
                UI.MakeTable(returnedList, "Drinks");
            }
        }
        public void GetDrinksDetail(int id)
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"lookup.php?i={id}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<DrinkDetails>(rawResponse);

                List<DrinkDetail> returnedList = serialize.DetailList;
                var detail = returnedList[0];
                var propList = new List<object>();
                var trimmedName = "";

                foreach(var prop in detail.GetType().GetProperties())
                {
                    if (prop.Name.Contains("str"))
                    {
                        trimmedName = prop.Name.Substring(3);
                    }

                    if (prop.GetValue(detail) == null) continue;
                    
                    propList.Add(new
                    {
                        Key = trimmedName,
                        Value = prop.GetValue(detail)
                    });
                }
                UI.MakeTable(propList, "Drinks Info");
            }
        }
    }
}
