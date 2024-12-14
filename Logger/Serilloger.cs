using System.Text;

namespace ACC01.Logger
{
    public class Serilloger
    {
        private static Serilloger? instance; 
        private readonly string logFilePath;

        private static readonly string logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ACC01", "Logger", "Logs");

        private Serilloger()
        {
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            logFilePath = Path.Combine(logFolderPath, "log.txt");
        }

        public static Serilloger GetInstance()
        {
            instance ??= new Serilloger();

            return instance;
        }

        public void Log(string algorithm, string type, long averageTimeInMs, long memoryUsed, int repetitions)
        {
            StringBuilder log = new();
            log.AppendLine($"=> {DateTime.Now}");
            log.AppendLine($"Algorithm: {algorithm}");
            log.AppendLine($"Type: {type}");
            log.AppendLine($"Average Time: {averageTimeInMs} ms (over {repetitions} repetitions)");
            log.AppendLine($"Memory Used: {memoryUsed / 1024} KB or {memoryUsed} bits");

            File.AppendAllText(logFilePath, log.ToString() + Environment.NewLine);
        }
    }
}