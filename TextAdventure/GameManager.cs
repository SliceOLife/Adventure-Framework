using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TextAdventure
{
    class GameManager
    {
        string adventurePath;
        string firstStage;
        public GameManager(String _path)
        {
            adventurePath = _path;
            var mainPath = _path + @"\main.ini";

            if(!File.Exists(mainPath)) {
                return;
            }

            var ini = new IniFile(mainPath);

            Console.WriteLine(ini.Read("title", "Intro"));
            Console.WriteLine(ini.Read("desc", "Intro"));

            if(!ini.KeyExists("firstStage", "Start")) {
                return;
            }

            firstStage = ini.Read("firstStage", "Start");
            stage.loadStage(firstStage);

        }
    }
}
