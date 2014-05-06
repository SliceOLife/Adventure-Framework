using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TextAdventure
{
    static class Program
    {
        public static string adventurePath = "adv";
        static void Main(string[] args)
        {
            // Let's check if there's an adventure.
            GameManager gameMan = new GameManager(adventurePath);
            Console.ReadLine();

        }
    }
}
