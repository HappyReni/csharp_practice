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
        public void GetCategories()
        {
            var client = new RestClient("http://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?c=list");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);

                List<Category> returnedList = serialize.CategoriesList;
                UI.MakeTable(returnedList, "Categories Menu");
            }
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

                foreach(var prop in detail.GetType().GetProperties())
                {
                    propList.Add(new
                    {
                        Key = prop.Name,
                        Value = prop.GetValue(detail)
                    });
                }
                foreach(var prop in propList)
                {
                    Console.WriteLine(prop);
                }
                UI.MakeTable(propList, "Drinks Info");
            }
        }
    }
}
