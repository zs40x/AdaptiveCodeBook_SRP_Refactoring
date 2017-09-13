using System.Collections.Generic;
using TradeProcessor.Core.Domain;

namespace TradeProcessor.BusinessLogic
{
    public class SimpleTradeValidator
    {
        public TradeLineValidationResult Validate(TradeFileLine tradeFileLine)
        {
            var columns = tradeFileLine.LineColumns;
            var logMessages = new List<string>();
            
            if (columns.Count < 1)
            {
                return new TradeLineValidationResult(false, new List<string>());
            }

            if (columns.Count < 3)
            {
                return new TradeLineValidationResult(false, new List<string> { $"WARN: Line {tradeFileLine.LineNo} malformed. Only {1} field(s) found." });
            }

            if (columns[0].Length != 6)
            {
                logMessages.Add($"WARN: Trade currencies on line {tradeFileLine.LineNo} malformed: '{columns[0]}'");
            }

            if (!int.TryParse(columns[1], out _))
            {
                logMessages.Add($"WARN: Trade amount on line {tradeFileLine.LineNo} not a valid integer: '{columns[1]}'");
            }

            if (!decimal.TryParse(columns[2], out _))
            {
                logMessages.Add($"WARN: Trade price on line {tradeFileLine.LineNo} not a valid decimal: '{columns[2]}'");
            }

            return new TradeLineValidationResult(true, logMessages);
        }
    }
}
