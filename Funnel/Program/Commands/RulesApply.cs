using Funnel.Managers;
using Funnel.Models;
using System;

namespace Funnel.Commands
{
    public struct RulesApply : ICommand
    {

        public string Command => "RULES apply";

        public string Description => "Applies the specified rule; if contained in the assets";

        public void Execute()
        {
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            Rule rule = manager.Find(ruleName);
            if(rule != null)
            {
                int mailsMoved = rule.Execute();
                if(mailsMoved > -1)
                {
                    Console.Write($"Moved {mailsMoved} mail ");
                    Console.Write($"from {rule.LocationFolderName} ");
                    Console.Write($"to {rule.DestinationFolderName} \n");
                    return;
                }
                Console.WriteLine("Folder was not found.");
            }
            Console.WriteLine("Rule was not found.");
        }

    }
}
