using System.IO;
using System.Reflection;

namespace Funnel
{
    public class FilesManager
    {

        #region Fields

        public readonly string RulesName = "Rules.xml";

        #endregion

        #region Properties

        public string RulesFullPath => GetFileFullPath(RulesName);

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes the name of an embedded resource and loads it in to a stream.
        /// </summary>
        /// <param name="resourceName">The resource name</param>
        /// <returns>The resource stream</returns>
        public Stream GetEmbeddedResourceStream(string resourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceFullName = GetResourceFullName(resourceName);
            Stream stream = assembly.GetManifestResourceStream(resourceFullName);
            return stream;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the full path of a file
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns>The file full path</returns>
        private string GetFileFullPath(string fileName)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            return $@"{assemblyDirectory}\{fileName}";
        }

        /// <summary>
        /// Gets the full name of a resource
        /// </summary>
        /// <param name="resourceName">The resource name</param>
        /// <returns>The resource full name</returns>
        private string GetResourceFullName(string resourceName)
        {
            return $"Funnel.Resources.{resourceName}";
        }

        #endregion

    }
}
