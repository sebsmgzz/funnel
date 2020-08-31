using System;
using System.Collections.Generic;
using Funnel.Managers;
using Funnel.Models;

namespace Funnel.Commands
{
    public struct FoldersScan : ICommand
    {

        public string Command => "FOLDERS scan";

        public string Description => "Scans the given folder to view matching rules";

        public void Execute()
        {
            Console.Write("Folder name: ");
            string folderName = Console.ReadLine();
            Folder folder = Folder.Find(folderName);
            if(folder != null)
            {
                // Get mail groups out of folder
                List<Mail> mails = folder.GetMails();
                Dictionary<string, List<Mail>> mailsGroups = new Dictionary<string, List<Mail>>();
                foreach(Mail mail in mails)
                {
                    if(!mailsGroups.ContainsKey(mail.Address))
                    {
                        mailsGroups.Add(mail.Address, new List<Mail>() { mail });
                    }
                    else
                    {
                        mailsGroups[mail.Address].Add(mail);
                    }
                }
                // Display mail groups
                foreach(KeyValuePair<string, List<Mail>> group in mailsGroups)
                {
                    Console.Write($"{group.Value.Count, 3} ");
                    Console.Write($"mail items from ");
                    Console.Write($"{group.Key}\n");
                }
                return;
            }
            Console.WriteLine("Folder was not found.");
        }

    }
}
