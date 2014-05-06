using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAdventure
{
    public static class inventory
    {
        private static List<String> inventoryList = new List<string>();
        private static List<String> inventoryUnlockAreas = new List<string>();
        private static List<String> inventoryPickupAreas = new List<string>();

        public static bool addToInventory(string ItemName, String ItemUnlockArea, String ItemPickupArea)
        {
            if (inventoryList.Contains(ItemName) || inventoryUnlockAreas.Contains(ItemUnlockArea) || inventoryPickupAreas.Contains(ItemPickupArea))
            {
                return false;
            }

            inventoryList.Add(ItemName);
            inventoryUnlockAreas.Add(ItemUnlockArea);
            inventoryPickupAreas.Add(ItemPickupArea);

            return true;
        }

        public static bool loadItem(String file)
        {
            var loadPath = file;
            if (!File.Exists(loadPath))
            {
                Console.WriteLine("[ERROR] Item not found -- " + file);
            }

            var ini = new IniFile(loadPath);
            string[] requirements = { "name", "pickuparea", "unlockarea" };
            foreach (string req in requirements)
            {
                if (!ini.KeyExists(req, "Item"))
                {
                    return false;
                }
            }

            var name = ini.Read("name", "Item");
            var pickupArea = ini.Read("pickuparea", "Item");
            var unlockArea = ini.Read("unlockarea", "Item");

            addToInventory(name, unlockArea, pickupArea);
            return true;

        }

        public static int inventoryCount()
        {
            return inventoryList.Count;
        }

        public static void writeInventory()
        {
            if (!inventoryList.Any())
            {
                Console.WriteLine("No items in your inventory!");
                return;
            }

            Console.WriteLine("Items available: ");
            foreach (string item in inventoryList)
            {
                Console.WriteLine(item);
            }
            
            // Newline for clarity
            Console.WriteLine();
        }
    }
}
