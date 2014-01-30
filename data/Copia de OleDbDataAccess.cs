using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace msgen.data
{
    class OleDbDataAccess : DataAccess
    {
        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public OleDbDataAccess(string connectionString) : base(connectionString)
        {
        }

        public DataTable query(string table, string fields, string expression)
        {
            string cnn = ConnectionString;
            string cmd = "select " + fields + " from " + table;
            if(expression != null && expression != string.Empty)
                cmd += " where " + expression;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd, cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.TableName = table;
            return dt;
        }

        public int update(string table, string field, string value, string expression)
        {
            string cnn = ConnectionString;
            string cmd = "update " + table + " set " + field + "='" + value + "'";
            if(expression != null && expression != string.Empty)
                cmd += " where " + expression;
            OleDbCommand command = new OleDbCommand(cmd, new OleDbConnection(cnn));
            int result = -1;
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        public int delete(string table, string expression)
        {
            string cnn = ConnectionString;
            string cmd = "delete from " + table;
            if(expression != null && expression != string.Empty)
                cmd += " where " + expression;
            OleDbCommand command = new OleDbCommand(cmd, new OleDbConnection(cnn));
            int result = -1;
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

    }
}
