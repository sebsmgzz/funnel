using Outlook = Microsoft.Office.Interop.Outlook;

namespace Funnel.Models
{
    /// <summary>
    /// Represents a mail object in the outlook app
    /// </summary>
    public class Mail
    {

        #region Fields

        private Outlook.MailItem mail;

        #endregion

        #region Properties

        /// <summary>
        /// The address of the sender
        /// </summary>
        public string Address
        {
            get { return mail.Sender.Address; }
        }

        #endregion

        #region Constructors

        public Mail(Outlook.MailItem mail)
        {
            this.mail = mail;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the mail to the given folder
        /// </summary>
        /// <param name="folder">The folder to move the mail</param>
        public void MoveTo(Folder folder)
        {
            folder.AddMail(mail);
        }

        #endregion

    }
}