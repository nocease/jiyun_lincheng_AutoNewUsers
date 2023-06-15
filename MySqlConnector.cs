using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class MySqlConnector
    {
        private string connectionString;

        public MySqlConnector()
        {
            string server = "127.0.0.1";
            string database = "jiyun";
            string username = "root";
            string password = "123456";
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        private MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
        }

        public int doSQL(string sql)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public string queryList(string sql)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return JsonConvert.SerializeObject(dataTable);
                }
            }
        }

        public string queryOne(string sql)
        {
            using (MySqlConnection connection = OpenConnection())
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }
                        return JsonConvert.SerializeObject(row);
                    }
                    return null;
                }
            }
        }
    }
}