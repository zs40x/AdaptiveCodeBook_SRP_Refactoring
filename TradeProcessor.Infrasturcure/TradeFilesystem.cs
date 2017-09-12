using System;
using System.Collections.Generic;
using System.IO;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.Infrasturcure
{
    public class TradeFilesystem: ITradeFilesystem, IDisposable
    {
        private readonly FileStream _tradeFileStream;

        public TradeFilesystem(FileStream tradeFileStream)
        {
            _tradeFileStream = tradeFileStream;
        }

        public IEnumerable<TradeFileLine> FileContent()
        {
            var tradeFileLines = new List<TradeFileLine>();
            var lineNo = 1;

            using (var reader = new StreamReader(_tradeFileStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeFileLines.Add(new TradeFileLine(lineNo, line));
                    lineNo++;
                }
            }

            return tradeFileLines;
        }

        public void Dispose()
        {
            _tradeFileStream?.Dispose();
        }
    }
}
