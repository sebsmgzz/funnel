using System.Collections.Generic;

namespace Funnel.Models
{
    /// <summary>
    /// Represents a rule to apply to the outlook app
    /// </summary>
    public class Rule
    {

        #region Fields

        private string name;
        private string locationFolderName;
        private string destinationFolderName;
        private List<string> addresses;

        #endregion

        #region Properties

        /// <summary>
        /// The name of the rule
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The location folder location where to move the coincident mails
        /// </summary>
        public string LocationFolderName
        {
            get { return locationFolderName; }
            set { locationFolderName = value; }
        }

        /// <summary>
        /// The destination to which move th addresses when the rule is applied
        /// </summary>
        public string DestinationFolderName
        {
            get { return destinationFolderName; }
            set { destinationFolderName = value; }
        }

        /// <summary>
        /// The amount of addresses in the rule
        /// </summary>
        public int AddressesCount
        {
            get { return addresses.Count; }
        }

        /// <summary>
        /// A read-only list of all the available addresses in the rule
        /// </summary>
        public IList<string> Addresses
        {
            get { return addresses.AsReadOnly(); }
        }

        #endregion

        #region Constructor

        public Rule()
        {
            this.addresses = new List<string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a address to the rule
        /// </summary>
        /// <param name="address">The address to add</param>
        /// <returns>True when added, false if already contained</returns>
        public bool AddAddress(string address)
        {
            if(!addresses.Contains(address))
            {
                addresses.Add(address);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes an address from the rule
        /// </summary>
        /// <param name="address">The address to remove</param>
        /// <returns>True when removed, false otherwise
        /// of if adress is not on the list</returns>
        public bool RemoveAddress(string address)
        {
            return addresses.Remove(address);
        }

        /// <summary>
        /// Executes the rule
        /// </summary>
        /// <returns>The number of mails moved</returns>
        public int Execute()
        {
            Folder locationFolder = Folder.Find(locationFolderName);
            Folder destinationFolder = Folder.Find(destinationFolderName);
            if (locationFolder != null && destinationFolder != null)
            {
                List<Mail> mails = locationFolder.GetMails();
                int mailsMoved = 0;
                foreach (Mail mail in mails)
                {
                    if (addresses.Contains(mail.Address))
                    {
                        mail.MoveTo(destinationFolder);
                        mailsMoved += 1;
                    }
                }
                return mailsMoved;
            }
            return -1;
        }

        #endregion

    }
}
