using RestSharp;

namespace DrinksInfo
{
    internal static class Validation
    {
        
        public static bool CheckCategory(string category)
        {
            if (category == "") throw new Exception("Input is Empty");
            DrinkService drinksService = new();
            var categories = drinksService.GetCategories();

            if (categories.Any(x => x.strCategory == category)) return true;
            else throw new Exception($"There is no category such a {category}");
        }
    }
}
