using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CraftSystemFall2024
{
    public class Vendor: Person
    {
        public Vendor(string name): base(name)
        {

        }

        public override string About()
        {
            return "Vendor: " + base.About();
        }

        private string Speak()
        {
            return Speak("...");
        }

        private string Speak(string words)
        {
            return $"{PersonName}: \"{words}\"";
        }

        public string Greet()
        {
            return Speak($"Hello. My name is {PersonName} and welcome to my shop!");
        }

        public string Farewell()
        {
            return Speak("Goodbye! Thanks for visiting my shop.");
        }

        public string Thank()
        {
            return Speak("Pleaure doing business with you.");
        }

        public string NoItem()
        {
            return Speak("Sorry, but I don't have that.");
        }

        public string NoItem(string itemName)
        {
            return Speak($"Sorry, but I don't have {itemName}.");
        }

        public string NoCurrency()
        {
            return Speak($"Sorry, but you don't have enough to buy that.");
        }

        public string NoCurrency(Item item, double currency)
        {
            return Speak($"Sorry, but that costs {(item.ItemValue - currency).ToString("c")} more than you have.");
        }
    }
}