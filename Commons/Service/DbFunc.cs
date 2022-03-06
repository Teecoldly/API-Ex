using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using System.Dynamic;
using System.Collections.Generic;


namespace Common.Service.DbFunc
{
    public class SqlParam
    {
        public string name { get; set; }
        public object value { get; set; }
    }
    public class DbFunc
    {
        MySqlConnection con;
        public DbFunc()
        {
            var configuation = GetConfiguration();
            con = new MySqlConnection(configuation.GetSection("ConnectionStrings").GetSection("db").Value);
        }
        public static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            return builder.Build();
        }
        public MySqlDataReader ExecQuery(string sql)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;

            return command.ExecuteReader();
        }
        public int ExecNotQuery(string sql)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            return command.ExecuteNonQuery();
        }
        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Clone();
                con.Dispose();

            }
        }
        public int ExecNotQuery(string sql, List<SqlParam> SqlParams)
        {
            MySqlCommand command = con.CreateCommand();

            command.CommandText = sql;

            foreach (SqlParam param in SqlParams)
            {
                command.Parameters.Add(param.name, MySqlDbType.VarChar);
                command.Parameters[param.name].Value = param.value;
            }
            return command.ExecuteNonQuery();
        }
        public MySqlDataReader ExecQuery(string sql, List<SqlParam> SqlParams)
        {
            MySqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.CommandTimeout = 0;
            foreach (SqlParam param in SqlParams)
            {
                command.Parameters.Add(param.name, MySqlDbType.VarChar);
                command.Parameters[param.name].Value = param.value;
            }
            return command.ExecuteReader();
        }
        public dynamic GetData(string sql, List<SqlParam> SqlParams)
        {
            var data = new ExpandoObject() as IDictionary<string, Object>;
            MySqlDataReader dr = ExecQuery(sql, SqlParams);
            if (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    data.Add(dr.GetName(i), (dr[i].ToString() == "") ? null : dr[i]);
                }
            }
            dr.Close();
            return data;
        }
        public dynamic GetListData(string sql, List<SqlParam> SqlParams)
        {
            List<dynamic> data = new List<dynamic>();
            MySqlDataReader dr = ExecQuery(sql, SqlParams);
            while (dr.Read())
            {
                var d = new ExpandoObject() as IDictionary<string, Object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    d.Add(dr.GetName(i), (dr[i].ToString() == "") ? null : dr[i]);
                }
                data.Add(d);
            }
            dr.Close();
            return data;
        }
    }

}
