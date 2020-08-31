using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Funnel.Models
{
    /// <summary>
    /// Represents a connection to the outlook app
    /// </summary>
    public class Connection
    {

        #region Fields

        private Application application;

        #endregion

        #region Constructors

        public Connection()
        {
            application = new Outlook.Application();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the root folder in the application
        /// </summary>
        /// <returns>The root folder model</returns>
        public Folder GetRootFolder()
        {
            Outlook.Folder root = application.Session.DefaultStore.GetRootFolder() as Outlook.Folder;
            return new Folder(root);
        }

        #endregion

    }
}
