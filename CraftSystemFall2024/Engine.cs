using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using static System.Console;
using static CraftSystemFall2024.Library;

namespace CraftSystemFall2024
{
    public class Engine
    {
        private string applicationName = "Super Awesome Crafting System";
        private List<Recipe> recipes = new List<Recipe>();
        Player Player = new Player("Player");
        Vendor Vendor = new Vendor(VendorNames.Bob.ToString());
        
        public void Start()
        {
            //temporary data for testing
            //recipes.Add(
            //    new Recipe()
            //    {
            //        RecipeAmount = 1,
            //        RecipeName = "Test Recipe",
            //        RecipeRequirements = new List<Item>() 
            //        {
            //        new Item() {ItemName = "Water" },
            //        new Item() {ItemName = "Map" },
            //        new Item() {ItemName = "Shoe" }
            //        },
            //        RecipeValue = 10,

            //    });

            foreach (Recipe recipe in LoadRecipeData())
            {
                recipes.Add(recipe);
            }

            Title = $"{applicationName} by Echo Schwartz";
            Print($"Welcome {Player.PersonName} to {applicationName}");
            Print("Enter any key to play!");
            ReadKey();
            Menu();
        }

        private void SetPlayerName()
        {
                Player.ChangeName(GetInput("Enter your name: "));
        }

        private string ShowRecipes()
        {
            string output = "Recipes: \n";
            foreach(Recipe recipe in recipes)
            {
                output += $"{recipe.RecipeName}\n";
            }

            return output;
        }

        private void Menu()
        {
            string[] menuChoices = 
                {
                "1) Craft", 
                "2) Show Inventory", 
                "3) Show Recipies", 
                "4) Change Name", 
                "5) View Credits", 
                $"6) Enter {Vendor.PersonName}'s Shop",
                "0) Quit Game"
            };
            ClearScreen();
            Print("Main Menu");
            Print(Player.ViewSummary());
            LineBreak();
            PrintMenu(menuChoices);
            LineBreak();
            int choice = ConvertStringToInt(GetInput("Enter your choice as a number: "));

            switch(choice)
            {
                case 0:
                    return;
                case 1:
                    Print("Crafting functionality coming soon!");
                    break;
                case 2:
                    Print(Player.ShowInventory());
                    break;
                case 3:
                    Print(ShowRecipes());
                    break;
                case 4:
                    SetPlayerName();
                    Print($"Your name is now {Player.PersonName}!");
                    break;
                case 5:
                    Print(ShowCredits());
                    break;
                case 6:
                    EnterShop(Vendor);
                    break;
                default:
                    Print("Please only enter one of the choices listed.");
                    break;
            }
            Print("Enter any key to continue.");
            ReadKey();
            Menu();
        }

        private string ShowCredits()
        {
            return "Created by Echo Schwartz\nInstructed by Professor Baxter";
        }

        private void CraftItem(Recipe recipe)
        {
            foreach(Item item in recipe.RecipeRequirements)
            {
                if (Player.FindInventoryItemIndexByName(item.ItemName) == -1)
                {
                    Print($"{item.ItemName} is missing from your inventory");
                    break;
                }
            }

            foreach (Item item in recipe.RecipeRequirements)
            {
                Player.RemoveItemFromInventoryByName(item.ItemName);
            }

            Player.AddItemToInventorybyName(recipe.RecipeName);
            Print(recipe.RecipeName, "has been crafted!");
        }

        private void EnterShop(Vendor vendor)
        {
            while (true)
            {
                ClearScreen();
                Print($"{vendor.PersonName}'s Shop");
                Print(Player.ViewSummary());
                LineBreak();
                Print(Vendor.Greet());
                Print(Vendor.ShowInventory());
                LineBreak();
                string input = GetInput("Enter the name of the item you'd like to buy, or enter nothing to exit: ");
                if (input == "")
                    break;
                int itemIndex = vendor.FindInventoryItemIndexByName(input);
                if (itemIndex == -1)
                {
                    VendorItemNotFound(vendor, input);
                    continue;
                }

                Item item = vendor.Inventory[itemIndex];
                double cost = item.ItemValue;

                if (cost > Player.Currency)
                {
                    VendorItemTooExpensive(vendor, item);
                    continue;
                }

                Player.Currency -= cost;
                vendor.Currency += cost;
                vendor.Inventory[itemIndex].ItemAmount--;
                if (vendor.Inventory[itemIndex].ItemAmount <= 0)
                    vendor.RemoveItemFromInventoryByIndex(itemIndex);

                itemIndex = Player.FindInventoryItemIndexByName(item.ItemName);
                if (itemIndex != -1)
                {
                    VendorBuyItemAgain(vendor, itemIndex);
                }
                else
                {
                    VendorBuyNewItem(vendor, item);
                }
            }
            Print(Vendor.Farewell());
        }

        private void VendorItemNotFound(Vendor vendor, string itemName)
        {
            Print(vendor.NoItem(itemName));
            Print("Enter any key to continue.");
            ReadKey();
        }

        private void VendorItemTooExpensive(Vendor vendor, Item item)
        {
            Print(vendor.NoCurrency(item, Player.Currency));
            Print("Enter any key to continue.");
            ReadKey();
        }

        private void VendorBuyItemAgain(Vendor vendor, int itemIndex)
        {
            Player.Inventory[itemIndex].ItemAmount++;
            Print("You have bought another", Player.Inventory[itemIndex].ItemName);
            Print(vendor.Thank());
            Print("Enter any key to continue.");
            ReadKey();
        }

        private void VendorBuyNewItem(Vendor vendor, Item item)
        {
            Player.Inventory.Add(new Item()
            {
                ItemName = item.ItemName,
                ItemValue = item.ItemValue,
                ItemAmount = 1
            });
            Print("You have bought a", item.ItemName);
            Print(vendor.Thank());
            Print("Enter any key to continue.");
            ReadKey();
        }

        public static List<Recipe> LoadRecipeData()
        {
            string fileName = "../../../data/Recipes.xml";
            List<Recipe> Recipes = new List<Recipe>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlNode root = doc.DocumentElement;
            XmlNodeList recipeList = root.SelectNodes("/recipes/recipe");
            XmlNodeList ingredientsList;

            foreach (XmlElement recipe in recipeList)
            {
                Recipe recipeToAdd = new Recipe();
                recipeToAdd.RecipeName = recipe.GetAttribute("title");
                recipeToAdd.RecipeDescription = recipe.GetAttribute("description");
                string yieldAmount = recipe.GetAttribute("yieldAmount");
                if (float.TryParse(yieldAmount, out float amount))
                { recipeToAdd.RecipeAmount = amount; }

                recipeToAdd.RecipeAmountType = recipe.GetAttribute("yieldType");
                string recipevalue = recipe.GetAttribute("value");
                if (float.TryParse(recipevalue, out float value))
                { recipeToAdd.RecipeValue = value; }

                ingredientsList = recipe.ChildNodes; //for ingredients

                foreach (XmlElement i in ingredientsList)
                {
                    string ingredientName = i.GetAttribute("itemName");
                    string ingredientAmountString = i.GetAttribute("amount");
                    float ingredientAmount = 0;
                    if (float.TryParse(ingredientAmountString, out float e))
                    { ingredientAmount = e; }
                    string ingredientAmountType = i.GetAttribute("amountType");
                    string tempIngredientValue = i.GetAttribute("value");
                    float ingredientValue = 0;
                    if (float.TryParse(tempIngredientValue, out float ingValue))
                    { ingredientValue = ingValue; }

                    recipeToAdd.RecipeRequirements.Add(
                    new Item()
                    {
                        ItemName = ingredientName,
                        ItemAmount = ingredientAmount,
                        ItemValue = ingredientValue
                    }
                    );
                }
                Recipes.Add(recipeToAdd);
            }
            return Recipes;
        }
    }
}