using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace TextAdventure
{
    class loader
    {
        string adventurePath;
        string firstStage;
        public loader(String _path)
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
            loadStage(firstStage);

        }
        public bool loadStage(String file)
        {
            // Write an empty line before loading a stage
            Console.WriteLine(Environment.NewLine);

            var loadPath = adventurePath + @"\" + file;
            if(!File.Exists(loadPath)) {
                Console.WriteLine("[ERROR] Stage not found -- " + file);
                Console.WriteLine("Restarting game in 3 seconds..");
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
                loadStage(firstStage);

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

            var name = ini.Read("name","Area");
            var desc = ini.Read("desc","Area");
            var question = ini.Read("question","Area");
            var opts = ini.Read("options","Area").Split(',');
            var nextareas = ini.Read("nextareas", "Area").Split(',');
            writeStage(name, desc, question, opts, nextareas);
            return false;

        }

        private bool writeStage(String name, String desc, String question, String[] opts, String[] nextAreas) {
            Console.WriteLine(name);
            Console.WriteLine(desc);
            Console.WriteLine(question);
            Console.WriteLine("The options are: ");

            for (var i = 0; i < opts.Length; i++)
            {
                Console.WriteLine(String.Format("[{0}] {1}", i, opts[i]));
            }

            var answer = Console.ReadLine();

            for (var i = 0; i < opts.Length; i++ )
            {
                if (opts[i] == answer)
                {
                    var nextStage = nextAreas[i];
                    loadStage(nextStage);
                    break;
                }
            }

            return false;
        }
    }
}
