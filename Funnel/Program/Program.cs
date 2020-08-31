using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Funnel.Commands;
using Funnel.Managers;

namespace Funnel
{
    public class Program
    {

        private const string ExitKeyWord = "exit";
        private const string HelpKeyWord = "help";

        private static List<ICommand> commands = 
            new List<ICommand>
            {
                new RulesAdd(),
                new RulesApplyAll(),
                new RulesApply(),
                new RulesRemove(),
                new RulesViewAll(),
                new RulesViewDetails(),
                new RulesViewGroupLocation(),
                new RulesViewGroupDestination(),
                new RulesModifyLocation(),
                new RulesModifyDestination(),
                new RulesModifyAddressessAdd(),
                new RulesModifyAddressessRemove(),
                new RulesOverrideLocation(),
                new FoldersScan()
            };

        private static void Main(string[] args)
        {
            Console.WriteLine($@"FUNNEL [Version {Properties.Settings.Default.Version}]");
            if (!RulesManager.TryLoad(out string message))
            {
                Console.WriteLine("Error loading rules file.");
                Console.WriteLine(message);
            }
            while (true)
            {
                Console.Write("FUNNEL> ");
                string input = Console.ReadLine();
                ICommand command = commands.Find((c) => c.Command == input);
                if (command != null)
                {
                    command.Execute();
                }
                else if (input == ExitKeyWord)
                {
                    break;
                }
                else if (input == HelpKeyWord)
                { 
                    foreach (ICommand c in commands)
                    {
                        Console.Write($"{c.Command,-35} \t");
                        Console.Write($"{c.Description} \n");
                    }
                }
                else
                {
                    Console.Write($"'{input}' is not recognized as a command.\n");
                    Console.Write($"Try typing '{HelpKeyWord}' to get help, ");
                    Console.Write($"or '{ExitKeyWord}' to exit the program.");
                }
                Console.Write("\n");
            }
        }

    }
}
