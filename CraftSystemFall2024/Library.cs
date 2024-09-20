using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CraftSystemFall2024
{
    class Library
    {
        public static void Print(params object[] values)
        {
            string sep = " ";
            string end = "\n";

            if (values.Length == 0)
            {
                Console.Write(end);
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (object value in values)
            {
                stringBuilder.Append(value);
                stringBuilder.Append(sep);
            }

            // Removes the last separator
            stringBuilder.Length -= sep.Length;
            stringBuilder.Append(end);

            string output = stringBuilder.ToString();
            Console.Write(output);
        }

        public static string GetInput(string? message = null)
        {
            if (message != null)
                Console.Write(message);

            string? input = Console.ReadLine();

            if (input == null) return "";
            return input;
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static int ConvertStringToInt(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }

            return -1;
        }

        public static double ConvertStringToDouble(string input)
        {
            if (double.TryParse(input, out double result))
            {
                return result;
            }

            return -1;
        }

        public static float ConvertStringToFloat(string input)
        {
            if (float.TryParse(input, out float result))
            {
                return result;
            }

            return -1;
        }

        public static void LineBreak()
        {
            Print("--------------------"); // 20 - chars
        }

        public static void PrintMenu(string[] menu)
        {
            foreach (string s in menu)
            {
                Print(s);
            }
        }

        public static string LoadItemData()
        {
            string[] items = File.ReadAllLines("../../../data/Items.txt");
            string output = "";
            foreach (string s in items)
            {
                output += s + Environment.NewLine;
            }
            return output;
        }
        
        public static string LoadWelcomeInformation()
        {
            string path = "../../../data/welcome.txt";
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "File not found...";
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
