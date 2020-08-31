using System;
using System.Collections.Generic;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesApplyAll : ICommand
    {

        public string Command => "RULES apply all";

        public string Description => "Applies all rules contained in the assets";

        public void Execute()
        {
            RulesManager manager = new RulesManager();
            List<Rule> rules = manager.Values();
            int rulesApplied = 0;
            foreach(Rule rule in rules)
            {
                Console.Write($"{rule.Name, 15} ");
                int mailsMoved = rule.Execute();
                if(mailsMoved > -1)
                {
                    Console.Write($"{mailsMoved, 3} mail moved ");
                    Console.Write($"from {rule.LocationFolderName} ");
                    Console.Write($"to {rule.DestinationFolderName}. \n");
                    rulesApplied += 1;
                    continue;
                }
                Console.WriteLine("Some folder was not found.");
            }
            Console.WriteLine($"Applied {rulesApplied} rules");
        }

    }
}
