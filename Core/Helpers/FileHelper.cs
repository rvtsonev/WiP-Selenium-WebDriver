namespace Core.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Opens a JSON file and returns its content as a string.
        /// </summary>
        /// <param name="projectName">The name of the project containing the file.</param>
        /// <param name="fileName">The name of the JSON file without the .json extension.</param>
        /// <returns>The content of the JSON file as a string.</returns>
        public static string OpenJsonFile(string projectName, string fileName)
        {
            var path = Path.Combine(PathHelper.GerRepositoryPath(), projectName, $"{fileName}.json");

            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Opens a text file and returns its content as a string.
        /// </summary>
        /// <param name="projectName">The name of the project containing the file.</param>
        /// <param name="file">The name of the file including its extension.</param>
        /// <returns>The content of the text file as a string.</returns>
        public static string OpenFile(string projectName, string file)
        {
            var path = Path.Combine(PathHelper.GerRepositoryPath(), projectName, file);

            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Creates or updates a text file with the provided content.
        /// </summary>
        /// <param name="fileName">The name of the file including its extension.</param>
        /// <param name="filePath">The path where the file will be stored or located.</param>
        /// <param name="text">The content to be written to the file.</param>
        public static void CreateOrUpdateFile(string fileName, string filePath, string text)
        {
            ConsoleLogger.Instance.Info($"Starting action to Creates or updates a text file '{fileName}' at '{filePath}'");

            if (File.Exists(filePath))
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(text);
                }

                ConsoleLogger.Instance.Debug($"New line added to '{fileName}' at '{filePath}'.");
            }
            else
            {
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine(text);
                }

                ConsoleLogger.Instance.Debug($"File '{fileName}' created with initial content at '{filePath}'.");
            }

            ConsoleLogger.Instance.Info($"File '{fileName}' was successfully created/updated at '{filePath}'.");
        }
    }
}
