using System;
using System.Data;
using System.Data.SqlClient;
using L6_P2_4_TagHelper.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace L6_P2_4_TagHelper.DAL
{
    public class Repository
    {
        private SqlConnection DbConnection { get; }
        private ILogger Logger { get; }

        public Repository(IConfiguration configuration, ILogger logger)
        {
            DbConnection = new SqlConnection(configuration.GetConnectionString("PartyDB"));
            Logger = logger;

            OpenConnection();
            //Create(new Party { Title = "God Party", Location = "San Morino", Date = DateTime.Now.AddDays(1) });
            //Edit(new Party { Id = 19, Title = "Hot Party1", Location = "Wall str 351", Date = DateTime.Now.AddDays(1) });
            //Delete(22);
        }

        protected void OpenConnection()
        {
            if (DbConnection.State != ConnectionState.Open)
                DbConnection.Open();
        }

        protected void ExecuteNonQuery(string sqlQuery)
        {
            try
            {
                using (var transaction = DbConnection.BeginTransaction())
                using (var command = new SqlCommand { Transaction = transaction, Connection = DbConnection })
                {
                    command.CommandText = sqlQuery;
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        protected SqlDataReader ExecuteReader(string sqlQuery)
        {
            try
            {
                using (var transaction = DbConnection.BeginTransaction())
                using (var command = new SqlCommand { Transaction = transaction, CommandText = sqlQuery, Connection = DbConnection })
                {
                    var reader = command.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }
    }
}
