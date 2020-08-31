using System;
using Funnel.Managers;

namespace Funnel.Commands
{
    public struct RulesRemove : ICommand
    {

        public string Command => "RULES remove";

        public string Description => "Removes a rule from the application";

        public void Execute()
        {
            Console.Write("Rule name: ");
            string ruleName = Console.ReadLine();
            RulesManager manager = new RulesManager();
            if (manager.Remove(ruleName))
            {
                Console.WriteLine("Rule removed succesfully!");
                return;
            }
            Console.WriteLine("Something when wrong.");
        }

    }
}
