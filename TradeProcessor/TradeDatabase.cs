using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TradeProcessor.ConsoleApp
{
    internal class TradeDatabase: IDisposable
    {
        private readonly SqlConnection _sqlConnection;

        public TradeDatabase(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void InsertTradeRecords(IEnumerable<TradeRecord> tradeRecords)
        {
            _sqlConnection.Open();
            using (var transaction = _sqlConnection.BeginTransaction())
            {
                foreach (var trade in tradeRecords)
                {
                    var command = _sqlConnection.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "dbo.insert_trade";
                    command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                    command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                    command.Parameters.AddWithValue("@lots", trade.Lots);
                    command.Parameters.AddWithValue("@price", trade.Price);

                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            _sqlConnection.Close();

        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}
