using System;
using System.Collections.Generic;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesViewAll : ICommand
    {

        public string Command => "RULES view all";

        public string Description => "Displays the list of rules in the application";

        public void Execute()
        {
            RulesManager manager = new RulesManager();
            List<Rule> rules = manager.Values();
            Console.Write($"{"Name",        -10}\t");
            Console.Write($"{"Location",    -20}\t");
            Console.Write($"{"Destination", -20}\t");
            Console.Write($"{"Addresses",   -10}\n");
            foreach (Rule rule in rules)
            {
                Console.Write($"{rule.Name,                 -10}\t");
                Console.Write($"{rule.LocationFolderName,   -20}\t");
                Console.Write($"{rule.DestinationFolderName,-20}\t");
                Console.Write($"{rule.AddressesCount,       -10}\n");
            }
        }

    }
}
