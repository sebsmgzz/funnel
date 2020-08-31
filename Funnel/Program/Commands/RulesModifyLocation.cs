using System;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesModifyLocation : ICommand
    {

        public string Command => "RULES modify location";

        public string Description => "Modifies the location of an existing rule in the application";

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
                rule.LocationFolderName = folderName;
                manager.Update(rule);
                return;
            }
            Console.WriteLine("Rule not found.");
            return;
        }

    }
}
