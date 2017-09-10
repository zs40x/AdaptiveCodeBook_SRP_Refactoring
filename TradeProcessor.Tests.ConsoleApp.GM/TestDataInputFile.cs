using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    writer.WriteLine(content);
                }
            }
        }
        
    }
}
