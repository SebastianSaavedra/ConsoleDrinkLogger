using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace ConsoleDrinkLogger
{
    internal class UserInput
    {
        DrinkService drinkService = new DrinkService();
        internal void GetCategories()
        {
            var categories = drinkService.GetCategories();

            Console.WriteLine("Choose category:");
            string category = Console.ReadLine();

            while (!Validator.IsStringValid(category))
            {
                Console.WriteLine("Invalid Category\n");
                category = Console.ReadLine();
            }
            category = char.ToUpper(category[0]) + category.Substring(1);

            if (!categories.Any(x => x.strCategory == category))
            {
                Console.WriteLine("Category doesn't exist");
                GetCategories();
            }
            GetDrinks(category);
        }

        private void GetDrinks(string category)
        {
            var drinks = drinkService.GetDrinksByCategory(category);

            Console.WriteLine("Choose a drink or go back to category menu by typing 0:");

            string drink = Console.ReadLine();

            if (drink == "0") GetCategories();

            while (!Validator.IsIdValid(drink))
            {
                Console.WriteLine("\nInvalid category");
                drink = Console.ReadLine();
            }

            if (!drinks.Any(x => x.idDrink == drink))
            {
                Console.WriteLine("Category doesn't exist.");
                GetDrinks(category);
            }
            drinkService.GetDrink(drink);

            Console.WriteLine("Press any key to go back to categories menu");
            Console.ReadKey();
            if (!Console.KeyAvailable) GetCategories();
        }
    }
}
