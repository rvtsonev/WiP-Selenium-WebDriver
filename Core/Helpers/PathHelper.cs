using System.Reflection;

namespace Core.Helpers
{
    public class PathHelper
    {
        /// <summary>
        /// Gets the repository path where the executing assembly is located.
        /// </summary>
        public static string GerRepositoryPath()
        {
            return new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent.Parent.FullName;
        }

        /// <summary>
        /// Gets the path of the specified project within the repository.
        /// </summary>
        /// <param name="projectName">The name of the project.</param>
        /// <returns>The path of the project.</returns>
        public static string GetProjectPath(string projectName)
        {
            return Path.Combine(GerRepositoryPath(), projectName);
        }

        /// <summary>
        /// Gets the path of a specific directory within the specified project.
        /// </summary>
        /// <param name="projectName">The name of the project.</param>
        /// <param name="directory">The name of the directory.</param>
        /// <returns>The path of the directory within the project.</returns>
        public static string GetProjectSpecificPath(string projectName, string directory)
        {
            return Path.Combine(GerRepositoryPath(), projectName, directory);
        }

        /// <summary>
        /// Splits the specified string into an array of path components.
        /// </summary>
        /// <param name="text">The string to split into a path.</param>
        /// <returns>An array of path components.</returns>
        public static string[] SplitStringToPath(string text)
        {
            return text.Split(Path.AltDirectorySeparatorChar);
        }
    }
}
