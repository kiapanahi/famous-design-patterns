using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.AdoNet
{
    // ADO.NET data access class. 

    public class Db
    {
        // ** Factory pattern

        static DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        public string connectionString { get; set; }

        public Db(string conn = null)
        {
            if (conn == null) // index is 1 because 0 = localdb
                connectionString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
            else
                connectionString = ConfigurationManager.ConnectionStrings[conn].ConnectionString;
        }


        // fast read and instantiate (i.e. make) a collection of objects

        public IEnumerable<T> Read<T>(string sql, Func<IDataReader, T> make, params object[] parms)
        {
            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(sql, connection, parms))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return make(reader);
                        }
                    }
                }
            }
        }

        // return a scalar object

        public object Scalar(string sql, params object[] parms)
        {

            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(sql, connection, parms))
                {
                    return command.ExecuteScalar();
                }
            }

        }

        // insert a new record

        public int Insert(string sql, params object[] parms)
        {

            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(sql + ";SELECT SCOPE_IDENTITY();", connection, parms))
                {
                    return int.Parse(command.ExecuteScalar().ToString());
                }
            }

        }

        // update an existing record

        public int Update(string sql, params object[] parms)
        {

            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(sql, connection, parms))
                {
                    return command.ExecuteNonQuery();
                }
            }

        }

        // delete a record

        public int Delete(string sql, params object[] parms)
        {
            return Update(sql, parms);
        }

        // creates a connection object

        DbConnection CreateConnection()
        {
            // ** Factory pattern in action

            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;
        }

        // creates a command object

        DbCommand CreateCommand(string sql, DbConnection conn, params object[] parms)
        {
            // ** Factory pattern in action

            var command = factory.CreateCommand();
            command.Connection = conn;
            command.CommandText = sql;
            command.AddParameters(parms);
            return command;
        }

        // creates an adapter object

        DbDataAdapter CreateAdapter(DbCommand command)
        {
            // ** Factory pattern in action

            var adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;
            return adapter;
        }
    }

    // extension methods

    public static class DbExtentions
    {
        // adds parameters to a command object

        public static void AddParameters(this DbCommand command, object[] parms)
        {
            if (parms != null && parms.Length > 0)
            {

                // ** Iterator pattern

                // NOTE: processes a name/value pair at each iteration

                for (int i = 0; i < parms.Length; i += 2)
                {
                    string name = parms[i].ToString();

                    // no empty strings to the database

                    if (parms[i + 1] is string && (string)parms[i + 1] == "")
                        parms[i + 1] = null;

                    // if null, set to DbNull

                    object value = parms[i + 1] ?? DBNull.Value;

                    // ** Factory pattern

                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = name;
                    dbParameter.Value = value;

                    command.Parameters.Add(dbParameter);
                }
            }
        }
    }
}
