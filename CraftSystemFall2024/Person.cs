using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CraftSystemFall2024
{
    public class Person
    {
        public string PersonName = "Anonymous";
        public double Currency = 10.5;
        public List<Item> Inventory = new List<Item>();
        public Person(){ }
        public Person(string name)
        {
            PersonName = name;
            //test data temporary
            //Item itemOne = new Item();
            Inventory.Add(new Item() 
            {
                ItemName = "Water", 
                ItemAmount = 6, 
                ItemValue = 2.0 
            });
            Inventory.Add(new Item()
            {
                ItemName = "Map",
                ItemAmount = 1,
                ItemValue = .25
            });
            Inventory.Add(new Item()
            {
                ItemName = "Shoe",
                ItemAmount = 2,
                ItemValue = .5
            });
        }
        public string ShowInventory()
        {
            string output = $"Inventory for {PersonName}:\n";
            foreach(Item item in Inventory)
            {
                output += $"   * {item.ItemAmount} of {item.ItemName} ({item.ItemValue.ToString("c")} each){Environment.NewLine}";
            }

            return output;

        }
        public string About()
        {
            //interpolation
            string output = $"{PersonName} {Currency.ToString("c")}";
            //concatenation
            //string output = PersonName + " " + Currency.ToString("c");
            //composite formatting
            //string output = "{0} {1.ToString(\"c\")}", PersonName, Currency;
            return output;
        }

        public string ViewSummary()
        {
            return $"{PersonName}\n{Currency.ToString("c")}";
        }
        public void ChangeName(string name)
        {
            PersonName = name;
        }

        public int FindInventoryItemIndexByName(string itemName)
        {
            itemName = itemName.ToLower();
            int index = 0;

            foreach(Item item in Inventory)
            {
                if (item.ItemName.ToLower() == itemName)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void AddItemToInventorybyName(string itemName)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemFromInventoryByName(string itemName)
        {
            int index = FindInventoryItemIndexByName(itemName);
            if (index == -1)
                return;

            Inventory.RemoveAt(index);
        }

        public void RemoveItemFromInventoryByIndex(int index)
        {
            if (index > Inventory.Count - 1 || index < 0)
                return;

            Inventory.RemoveAt(index);
        }

        public Item? Trade(string name, double currency)
        {
            int itemIndex = FindInventoryItemIndexByName(name);
            if (itemIndex == -1)
                return null;

            Item item = Inventory[itemIndex];

            if (item.ItemValue > currency)
                return null;

            RemoveItemFromInventoryByIndex(itemIndex);
            return item;
        }
    }
}