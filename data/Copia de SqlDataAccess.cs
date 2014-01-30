using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace msgen.data
{
    class SqlDataAccess : DataAccess
    {
        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public SqlDataAccess(string connectionString) : base(connectionString)
        {
            base.dataAdapter = new SqlDataAdapter();
            base.command = new SqlCommand();
            base.connetion = new SqlConnection(connectionString);
        }

        public int insert(string table, string values)
        {
            string cnn = ConnectionString;
            string cmd = "insert into " + table + " values(" + values + ")";
            SqlCommand command = new SqlCommand(cmd, new SqlConnection(cnn));
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

        public int insert(string table, string fields, string values)
        {
            string cnn = ConnectionString;
            string cmd = "insert into " + table + "(" + fields + ") values(" + values + ")";
            SqlCommand command = new SqlCommand(cmd, new SqlConnection(cnn));
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

        public int update(string table, string field, string value, string expression)
        {
            string cnn = ConnectionString;
            string cmd = "update " + table + " set " + field + "='" + value + "'";
            if(expression != null && expression != string.Empty)
                cmd += " where " + expression;
            SqlCommand command = new SqlCommand(cmd, new SqlConnection(cnn));
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

        public int delete(string table)
        {
            return delete(table, null);
        }

        public int delete(string table, string expression)
        {
            string cnn = ConnectionString;
            string cmd = "delete from " + table;
            if(expression != null && expression != string.Empty)
                cmd += " where " + expression;
            SqlCommand command = new SqlCommand(cmd, new SqlConnection(cnn));
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

        public bool executeProcedure(string procedure)
        {
            return executeProcedure(procedure, null);
        }

        public bool executeProcedure(string procedure, SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            if(parameters != null)
                cmd.Parameters.AddRange(parameters);
            int result = -1;
            try
            {
                cnn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch(InvalidOperationException ex)
            {
                Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            catch(SqlException ex)
            {
                Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            finally
            {
                cnn.Close();
            }
            return result > 0;
        }

        public object executeScalarProcedure(string procedure)
        {
            return executeScalarProcedure(procedure, null);
        }

        public object executeScalarProcedure(string procedure, SqlParameter[] parameters)
        {
            SqlConnection cnn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            if(parameters != null)
                cmd.Parameters.AddRange(parameters);
            object result = null;
            try
            {
                cnn.Open();
                result = cmd.ExecuteScalar();
            }
            catch(InvalidOperationException ex)
            {
                Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            catch(SqlException ex)
            {
                Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            finally
            {
                cnn.Close();
            }
            return result;
        }

    }
}
