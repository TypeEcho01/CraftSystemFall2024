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

        //public static string LoadItemData()
        //{
        //    string[] items = File.ReadAllLines("../../../data/Items.txt");
        //    string output = "";
        //    foreach (string s in items)
        //    {
        //        output += s + Environment.NewLine;
        //    }
        //    return output;
        //}
        
        //public static string LoadWelcomeInformation()
        //{
        //    string path = "../../../data/welcome.txt";
        //    if (File.Exists(path))
        //    {
        //        return File.ReadAllText(path);
        //    }
        //    return "File not found...";
        //}
    }
}
