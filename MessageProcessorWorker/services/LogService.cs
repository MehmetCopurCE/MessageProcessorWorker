using MessageProcessorWorker.services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.services
{
    public class LogService : ILogService
    {
        private readonly ILogger<MessageWorker> _logger;

        private readonly object _lock = new object();

        private readonly string _logFilePath;

        public LogService(ILogger<MessageWorker> logger, IConfiguration configuration)
        {
            _logger = logger;

            // appsettings.json dosyasından klasör yolunu oku
            string logFolderPath = configuration["Paths:LogPath"];

            // Eğer klasör yolu boşsa varsayılan bir yol ayarla
            if (string.IsNullOrEmpty(logFolderPath))
            {
                logFolderPath = Directory.GetCurrentDirectory(); // Varsayılan çalışma dizini
            }

            // Log dosyasının adını belirle (örneğin sabit bir ad veya dinamik bir tarih)
            string logFileName = "MessageProcessWorkerLog.txt"; // Sabit dosya adı
            // veya: string logFileName = $"MessageProcessWorkerLog{DateTime.Now:yyyyMMdd}.txt"; // Tarihli dosya adı

            // Tam dosya yolunu oluştur
            _logFilePath = Path.Combine(logFolderPath, logFileName);

            // Klasör yoksa oluştur
            var directory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.LogError(message, ex);
            lock (_lock)
            {
                EnsureLogFileExists();

                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {message}");
                    writer.WriteLine($"Exception: {ex.Message}");
                    writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
            lock (_lock)
            {
                EnsureLogFileExists();

                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - WARNING: {message}");
                }
            }
        }

        private void EnsureLogFileExists()
        {
            if (!File.Exists(_logFilePath))
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Log dosyası oluşturuldu.");
                }
            }
        }
    }

}
