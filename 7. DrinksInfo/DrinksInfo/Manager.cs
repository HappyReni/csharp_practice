using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksInfo
{
    internal class Manager
    {
        public UI Ui { get; set; }
        public DrinkService DrinkServiceInstance { get; set; }
        private string Category { get; set; } = "";

        public Manager()
        {
            Ui = new UI();
            DrinkServiceInstance = new DrinkService();
            BeginService();
        }

        private void BeginService()
        {
            DrinkServiceInstance.GetCategories();
            ChooseCategory();
            int id = Ui.GetInput("Select ID of drink to see info.").val;
            DrinkServiceInstance.GetDrinksDetail(id);
        }

        private void ChooseCategory()
        {
            try
            {
                Category = Ui.GetInput("Select").str;
                if (Validation.CheckCategory(Category))
                {
                    DrinkServiceInstance.GetDrinksByCategory(Category);
                }
            }
            catch(Exception e)
            {
                UI.Write(e.Message);
                UI.WaitForInput();
                BeginService();
            }

        }
    }
}
