using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    internal class Manager
    {
        public UI ui { get; set; }
        public DrinkService drinkService { get; set; }

        public Manager()
        {
            ui = new UI();
            drinkService = new DrinkService();
            drinkService.GetCategories();
            string category = ui.GetInput("Select").str;
            drinkService.GetDrinksByCategory(category);
            int id = ui.GetInput("Select ID of drink to see info.").val;
            drinkService.GetDrinksDetail(id);
        }
    }
}
