using System.Collections.Generic;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.BusinessLogic
{
    public class SimpleTradeValidator
    {
        private ILog _log;

        public SimpleTradeValidator(ILog log)
        {
            _log = log;
        }


        public bool Validate(TradeFileLine tradeFileLine)
        {
            var columns = tradeFileLine.LineColumns;
            
            if (columns.Count < 1)
            {
                return false;
            }

            if (columns.Count < 3)
            {
                _log.Log($"WARN: Line {tradeFileLine.LineNo} malformed. Only {1} field(s) found.");
                return false;
            }

            if (columns[0].Length != 6)
            {
                _log.Log($"WARN: Trade currencies on line {tradeFileLine.LineNo} malformed: '{columns[0]}'");
            }

            if (!int.TryParse(columns[1], out _))
            {
                _log.Log($"WARN: Trade amount on line {tradeFileLine.LineNo} not a valid integer: '{columns[1]}'");
            }

            if (!decimal.TryParse(columns[2], out _))
            {
                _log.Log($"WARN: Trade price on line {tradeFileLine.LineNo} not a valid decimal: '{columns[2]}'");
            }

            return true;
        }
    }
}
