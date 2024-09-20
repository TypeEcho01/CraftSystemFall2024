using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;
using static CraftSystemFall2024.Library;

namespace CraftSystemFall2024
{
    public class Engine
    {
        private string applicationName = "Super Awesome Crafting System";
        private List<Recipe> recipes = new List<Recipe>();
        Person Player = new Person("Player");
        Person Vendor = new Person();
        
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
                Player.ChangeName(GetInput("Enter your name:"));
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
            string[] menuChoices = {"1) Craft", "2) Show Inventory", "3) Show Recipies", "4) Change Name", "5) View Credits", "0) Quit Game" };
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
    }
}