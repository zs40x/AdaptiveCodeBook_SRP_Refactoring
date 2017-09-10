using System;
using System.Collections.Generic;
using System.IO;

namespace TradeProcessor.ConsoleApp
{
    public class TradeFile: IDisposable
    {
        private readonly FileStream _tradeFileStream;

        public TradeFile(FileStream tradeFileStream)
        {
            _tradeFileStream = tradeFileStream;
        }

        public IEnumerable<string> TradeLines()
        {
            var lines = new List<string>();

            using (var reader = new StreamReader(_tradeFileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }

        public void Dispose()
        {
            _tradeFileStream?.Dispose();
        }
    }
}
