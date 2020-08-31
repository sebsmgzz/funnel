using System;
using System.Collections.Generic;
using System.Configuration;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesViewGroupLocation : ICommand
    {

        public string Command => "RULES view group location";

        public string Description => "Displays the details for the rules with the given folder location";

        public void Execute()
        {
            Console.Write("Folder name: ");
            string folderName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            List<Rule> rules = manager.Values();
            Console.Write($"{"Name",-10}\t");
            Console.Write($"{"Location",-20}\t");
            Console.Write($"{"Destination",-20}\t");
            Console.Write($"{"Addresses",-10}\n");
            foreach (Rule rule in rules)
            {
                if(rule.LocationFolderName == folderName)
                {
                    Console.Write($"{rule.Name,-10}\t");
                    Console.Write($"{rule.LocationFolderName,-20}\t");
                    Console.Write($"{rule.DestinationFolderName,-20}\t");
                    Console.Write($"{rule.AddressesCount,-10}\n");
                }
            }
        }

    }
}
