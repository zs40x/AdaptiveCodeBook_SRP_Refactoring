using System.IO;

namespace TradeProcessor.Tests.ConsoleApp.GM
{
    class TestDataInputFile
    {
        private readonly string _filePath;

        public TestDataInputFile(string filePath)
        {
            _filePath = filePath;
        }

        public void WithContent(string content)
        {
            File.Delete(_filePath);
            
            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (var writer = new StreamWriter(stream))
                {
                    if (!string.IsNullOrEmpty(content))
                    {
                        writer.WriteLine(content);
                    }
                }
            }
        }
        
    }
}
