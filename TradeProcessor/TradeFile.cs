using System;
using System.Collections.Generic;
using System.IO;
using TradeProcessor.BusinessLogic;

namespace TradeProcessor.ConsoleApp
{
    internal class TradeFile: IDisposable
    {
        private readonly FileStream _tradeFileStream;

        public TradeFile(FileStream tradeFileStream)
        {
            _tradeFileStream = tradeFileStream;
        }

        public IEnumerable<TradeFileLine> TradeLines()
        {
            var lines = new List<TradeFileLine>();
            var lineNo = 1;

            using (var reader = new StreamReader(_tradeFileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(new TradeFileLine(lineNo, line));
                    lineNo++;
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
