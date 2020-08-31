using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Funnel.Models
{
    /// <summary>
    /// Represents a folder in the outlook app
    /// </summary>
    public class Folder
    {

        #region Fields

        private Outlook.Folder folder;

        #endregion

        #region Properties

        /// <summary>
        /// The amount of direct folders below this folder
        /// </summary>
        public int ChildsCount
        {
            get { return folder.Folders.Count; }
        }

        /// <summary>
        /// The name of the folder
        /// </summary>
        public string Name
        {
            get { return folder.Name; }
        }

        #endregion

        #region Constructors

        public Folder(Outlook.Folder folder)
        {
            this.folder = folder;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a deep list containing all the subfolders in this folder
        /// </summary>
        /// <returns>A list of the subfolders</returns>
        public List<Folder> GetAllChildren()
        {
            List<Folder> folders = new List<Folder>();
            this.ListFolders(folder, ref folders);
            return folders;
        }

        /// <summary>
        /// Gets a list of the sub folders matching the given func
        /// </summary>
        /// <param name="func">A method returning true when a folder is to be listed</param>
        /// <returns>The list of folders matching the method</returns>
        public List<Folder> GetDirectChildrenWhere(Func<Outlook.Folder, bool> func)
        {
            List<Folder> folders = new List<Folder>();
            Outlook.Folders childFolders = folder.Folders;
            foreach (Outlook.Folder childFolder in childFolders)
            {
                if(func.Invoke(childFolder))
                {
                    folders.Add(new Folder(childFolder));
                }
            }
            return folders;
        }

        /// <summary>
        /// Finds the subfolder matching the given name
        /// </summary>
        /// <param name="name">The name of the subfolder to match</param>
        /// <returns>The folder</returns>
        public Folder GetDirectChild(string name)
        {
            Outlook.Folders childFolders = folder.Folders;
            foreach (Outlook.Folder childFolder in childFolders)
            {
                if (childFolder.Name == name)
                {
                    return new Folder(childFolder);
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a list of all the mails in the folder
        /// </summary>
        /// <returns>A list containing all the mail elements in the folder</returns>
        public List<Mail> GetMails()
        {
            List<Mail> mails = new List<Mail>();
            foreach(object item in folder.Items)
            {
                if (item is Outlook.MailItem mail)
                {
                    mails.Add(new Mail(mail));
                }
            }
            return mails;
        }

        /// <summary>
        /// Stores the given mail items to the folder
        /// </summary>
        /// <param name="mails">The mail items</param>
        public void AddMail(params Outlook.MailItem[] mails)
        {
            foreach(Outlook.MailItem mail in mails)
            {
                mail.Move(folder);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Uses recursion to list all folders from an initial fodler and list
        /// </summary>
        /// <param name="folder">The folder from which to extract the subfolders</param>
        /// <param name="folders">The list into which list the folders</param>
        private void ListFolders(Outlook.Folder folder, ref List<Folder> folders)
        {
            Outlook.Folders childFolders = folder.Folders;
            foreach (Outlook.Folder childFolder in childFolders)
            {
                folders.Add(new Folder(childFolder));
                ListFolders(childFolder, ref folders);
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Finds the folder with the given name
        /// </summary>
        /// <param name="folderName">The name of the folder</param>
        /// <returns>The folder if found, null otherwise</returns>
        public static Folder Find(string folderName)
        {
            Connection connection = new Connection();
            Folder root = connection.GetRootFolder();
            List<Folder> folders = root.GetAllChildren();
            foreach (Folder folder in folders)
            {
                if (folder.Name == folderName)
                {
                    return folder;
                }
            }
            return null;
        }

        #endregion

    }
}
