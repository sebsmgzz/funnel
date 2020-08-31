using System;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesModifyDestination : ICommand
    {

        public string Command => "RULES modify destination";

        public string Description => "Modifies the destination of an existing rule in the application";

        public void Execute()
        {
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            Rule rule = manager.Find(ruleName);
            if(rule != null)
            {
                Console.Write("Folder name: ");
                string folderName = Console.ReadLine();
                rule.DestinationFolderName = folderName;
                if(manager.Update(rule))
                {
                    Console.Write("Rule updated successfully");
                }
                Console.WriteLine("File is not loaded.");
                return;
            }
            Console.WriteLine("Rule not found.");
            return;
        }

    }
}
