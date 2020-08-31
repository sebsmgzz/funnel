using System;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesModifyAddressessRemove : ICommand
    {

        public string Command => "RULES modify addressess remove";

        public string Description => "Removes an address to an existing rule in the application";

        public void Execute()
        {
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            Rule rule = manager.Find(ruleName);
            if(rule != null)
            {
                Console.Write("Address: ");
                string address = Console.ReadLine();
                if(rule.RemoveAddress(address))
                {
                    if(manager.Update(rule))
                    {
                        Console.WriteLine("Address removed successfully.");
                        return;
                    }
                    Console.WriteLine("Rules file is not loaded.");
                    return;
                }
                Console.WriteLine("Address is not contained in the rule.");
                return;
            }
            Console.WriteLine("Rule not found.");
            return;
        }

    }
}
