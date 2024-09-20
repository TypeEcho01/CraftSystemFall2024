using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftSystemFall2024
{
    public class Recipe
    {
        public string RecipeName;
        public double RecipeAmount = 1;
        public double RecipeValue = 1;
        public string RecipeDescription;
        public string RecipeAmountType;
        public List<Item> RecipeRequirements = new List<Item>();

        public Item? Craft(params Item[] items)
        {
            foreach (Item requirement in RecipeRequirements)
            {
                foreach (Item item in items)
                {
                    if (requirement.ItemName == item.ItemName)
                    {
                        break;
                    }
                    return null;
                }
            }
            return new Item() 
            {  
                ItemName = RecipeName,
                ItemAmount = RecipeAmount,
                ItemValue = RecipeValue
            };
        }
    }
}