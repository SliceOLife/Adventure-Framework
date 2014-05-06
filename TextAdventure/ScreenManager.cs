using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventure
{
    static class ScreenManager
    {
        public static void writeStage(String name, String desc, String question, String[] opts, String[] nextAreas)
        {
            // Show item's in inventory
            inventory.writeInventory();
            Console.WriteLine(name);
            Console.WriteLine(desc);
            Console.WriteLine(question);
            Console.WriteLine("The options are: ");

            for (var i = 0; i < opts.Length; i++)
            {
                Console.WriteLine(String.Format("[{0}] {1}", i, opts[i]));
            }

            var answer = Console.ReadLine();

            for (var i = 0; i < opts.Length; i++)
            {
                if (opts[i] == answer)
                {
                    var nextStage = nextAreas[i];
                    stage.loadStage(nextStage);
                    break;
                }
            }
        }
    }
}
