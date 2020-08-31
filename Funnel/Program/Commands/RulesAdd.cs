using System;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesAdd : ICommand
    {

        public string Command => "RULES add";

        public string Description => "Adds a new rule to the application";

        public void Execute()
        {
            // setup rule
            Rule rule = new Rule();
            Console.Write("Rule name: ");
            rule.Name = Console.ReadLine();
            Console.Write("From folder (Location): ");
            rule.LocationFolderName = Console.ReadLine();
            Console.Write("To folder (Destination): "); 
            rule.DestinationFolderName = Console.ReadLine();
            // setup addresses
            Console.Write("Addresses count: ");
            if (Int32.TryParse(Console.ReadLine(), out int addressesCount))
            {
                // get addresses
                for (int i = 1; i <= addressesCount; i++)
                {
                    Console.Write($"Address {i}: ");
                    rule.AddAddress(Console.ReadLine());
                }
                // save rule
                RulesManager manager = new RulesManager();
                if (manager.Add(rule))
                {
                    if(RulesManager.TrySave(out string message))
                    {
                        Console.WriteLine("Rule saved successfully!");
                        return;
                    }
                    Console.WriteLine("Could not save the rule into the file.");
                    Console.WriteLine(message);
                    return;
                }
                Console.WriteLine("Something went wrong while adding the rule.");
                return;
            }
            Console.WriteLine("Invalid number.");
            return;
        }

    }
}
