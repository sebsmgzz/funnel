using System;
using System.Collections.Generic;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesViewDetails : ICommand
    {

        public string Command => "RULES view details";

        public string Description => "Displays the details for the given rule";

        public void Execute()
        {
            // setup rule
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            Rule rule = manager.Find(ruleName);
            if (rule != null)
            {
                Console.WriteLine($"{"Name:", 20} {rule.Name}");
                Console.WriteLine($"{"Location folder:", 20} {rule.LocationFolderName}");
                Console.WriteLine($"{"Destination folder:", 20} {rule.DestinationFolderName}");
                Console.WriteLine($"{"Addresses:", 20}");
                IList<string> addresses = rule.Addresses;
                for(int i = 0; i < addresses.Count; i++)
                {
                    Console.WriteLine($"{$"{i}:", 20} {addresses[i]}");
                }
                return;
            }
            Console.WriteLine("Rule was not found.");
            return;
        }

    }
}
