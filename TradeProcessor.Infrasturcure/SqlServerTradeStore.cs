using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.Infrasturcure
{
    public class SqlServerTradeStore: IDisposable, ITradeStore
    {
        private readonly SqlConnection _sqlConnection;

        public SqlServerTradeStore(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void InsertTradeRecords(List<TradeRecord> tradeRecords)
        {
            _sqlConnection.Open();

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                tradeRecords.ForEach(trade => ExecInsertTrade(transaction, trade));

                transaction.Commit();
            }

            _sqlConnection.Close();
        }

        private void ExecInsertTrade(SqlTransaction transaction, TradeRecord trade)
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

        public void Dispose()
        {
            _sqlConnection?.Dispose();
        }
    }
}
