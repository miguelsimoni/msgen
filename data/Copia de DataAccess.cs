using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace msgen.data
{
    abstract class DataAccess : IDataAccess
    {
        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        protected IDbDataAdapter dataAdapter;
        protected IDbCommand command;
        protected IDbConnection connetion;

        public DataAccess(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DataTable query(string table)
        {
            return query(table, "*", null);
        }

        public DataTable query(string table, string fields)
        {
            return query(table, fields, null);
        }

        public DataTable query(string table, string fields, string expression)
        {
            StringBuilder cmd = new StringBuilder("select ");
            cmd.Append(fields);
            cmd.Append(" from ");
            cmd.Append(table);
            if(expression != null && expression != string.Empty)
            {
                cmd.Append(" where ");
                cmd.Append(expression);
            }
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataSet.Tables[0].TableName = table;
            return dataSet.Tables[0];
        }

        public int insert(string table, string values)
        {
            return insert(table, null, values);
        }

        public int insert(string table, string fields, string values)
        {
            StringBuilder cmd = new StringBuilder("insert into ");
            cmd.Append(table);
            cmd.Append("(");
            cmd.Append(fields);
            cmd.Append(")");
            cmd.Append(" values(");
            cmd.Append(values);
            cmd.Append(")");
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = connetion;
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
            StringBuilder cmd = new StringBuilder("update ");
            cmd.Append(table);
            cmd.Append(" set ");
            cmd.Append(field);
            cmd.Append("='");
            cmd.Append(value);
            cmd.Append("'");
            if(expression != null && expression != string.Empty)
            {
                cmd.Append(" where ");
                cmd.Append(expression);
            }
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = connetion;
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
            StringBuilder cmd = new StringBuilder("delete from ");
            cmd.Append(table);
            if(expression != null && expression != string.Empty)
            {
                cmd.Append(" where ");
                cmd.Append(expression);
            }
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
            command.Connection = connetion;
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

        public bool executeProcedure(string procedure, IDbDataParameter[] parameters)
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

        public object executeScalarProcedure(string procedure, IDbDataParameter[] parameters)
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
