using System;
using System.Collections.Generic;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct RulesOverrideLocation : ICommand
    {

        public string Command => "RULES override location";

        public string Description => "Applies the given rule in an overriden folder location";

        public void Execute()
        {
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            Rule rule = manager.Find(ruleName);
            if (rule != null)
            {
                Console.Write($"({rule.LocationFolderName} override) \n");
                Console.Write("Folder name: ");
                string folderName = Console.ReadLine();
                Folder folder = Folder.Find(folderName);
                if(folder != null)
                {
                    rule.LocationFolderName = folder.Name;
                    int mailsMoved = rule.Execute();
                    if (mailsMoved > -1)
                    {
                        Console.Write($"Moved {mailsMoved} mail ");
                        Console.Write($"from {rule.LocationFolderName} ");
                        Console.Write($"to {rule.DestinationFolderName} \n");
                        return;
                    }
                }
                Console.WriteLine("Folder was not found.");
            }
            Console.WriteLine("Rule was not found.");
        }

    }
}
