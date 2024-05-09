namespace Core.Logger
{
    public class ConsoleLogger
    {
        private static ConsoleLogger _instance;
        private LogLevel _filterLevel;

        public ConsoleLogger(LogLevel filterLevel)
        {
            _filterLevel = filterLevel;
        }

        public static ConsoleLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConsoleLogger(LogLevel.INFO);
                }
                return _instance;
            }
        }

        /// <summary>
        /// Sets the log level for the whole ConsoleLogger instance
        /// </summary>
        /// <param name="filterLevel">info/debug/warn/error/fatal</param>
        public void SetLogLevel(string filterLevel)
        {
            switch (filterLevel)
            {
                case "info":
                    _filterLevel = LogLevel.INFO;
                    break;
                case "debug":
                    _filterLevel = LogLevel.DEBUG;
                    break;
                case "warn":
                    _filterLevel = LogLevel.WARN;
                    break;
                case "error":
                    _filterLevel = LogLevel.ERROR;
                    break;
                case "fatal":
                    _filterLevel = LogLevel.FATAL;
                    break;
                default:
                    throw new ArgumentException($"ERROR: No such filter level.");
            }
        }

        public void Log(LogLevel level, string message)
        {
            if (level <= _filterLevel)
            {
                string hyphens = new string('-', (int)level + 1);
                Console.WriteLine($"{hyphens}[{level}] {message}");
            }
        }

        public void Debug(string message)
        {
            Log(LogLevel.DEBUG, message);
        }

        public void Info(string message)
        {
            Log(LogLevel.INFO, message);
        }

        public void Warn(string message)
        {
            Log(LogLevel.WARN, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.ERROR, message);
        }

        public void Fatal(string message)
        {
            Log(LogLevel.FATAL, message);
        }
    }
}
