using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAdventure
{
    public static class stage
    {
        public static void OnEnterStage(String stageFilename, String stageShownName, String loadPath)
        {
            // Show the stage, redirect output to screen manager, do checks for item pickups.
            DirectoryInfo d = new DirectoryInfo(loadPath);

            foreach (var item in d.GetFiles("*.ini"))
            {
                var ini = new IniFile(loadPath + item.Name);
                // Check if it contains a retrieve point for this stage
                if (!ini.KeyExists("pickuparea", "Item"))
                {
                    return;
                }

                var pickuparea = ini.Read("pickuparea", "Item");

                if (pickuparea == stageFilename)
                {
                    // This stage has an item for pickup
                    inventory.loadItem(loadPath + item.Name);
                }

             }
        }

        public static bool loadStage(String file)
        {
            // Write an empty line before loading a stage
            Console.WriteLine(Environment.NewLine);

            var loadPath = Program.adventurePath + @"\" + file;
            if (!File.Exists(loadPath))
            {
                Console.WriteLine("[ERROR] Stage not found -- " + file);
                Console.WriteLine("Restarting game in 3 seconds..");
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
                loadStage("beach.ini");

                return false;
            }

            var ini = new IniFile(loadPath);
            string[] requirements = { "name", "desc", "question", "options" };
            foreach (string req in requirements)
            {
                if (!ini.KeyExists(req, "Area"))
                {
                    return false;
                }
            }

            var name = ini.Read("name", "Area");
            var desc = ini.Read("desc", "Area");
            var question = ini.Read("question", "Area");
            var opts = ini.Read("options", "Area").Split(',');
            var nextareas = ini.Read("nextareas", "Area").Split(',');
            stage.OnEnterStage(file, name, Program.adventurePath + @"\items\");
            ScreenManager.writeStage(name, desc, question, opts, nextareas);
            return true;

        }
    }
}
