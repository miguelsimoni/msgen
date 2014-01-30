using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace msgen.data
{
    public abstract class DataAccess : IDataAccess
    {
        protected IDbDataAdapter dataAdapter;
        protected IDbCommand command;
        protected IDbConnection connetion;

        private string connectionString;
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool checkConnection()
        {
            try
            {
                connetion.Open();
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                connetion.Close();
            }
            return true;
        }

        public int executeSql(string sql)
        {
            command.Connection = connetion;
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
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
            command.Connection = connetion;
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
            dataAdapter.SelectCommand = command;
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
            if(fields != null && fields != string.Empty)
            {
                cmd.Append("(");
                cmd.Append(fields);
                cmd.Append(")");
            }
            cmd.Append(" values(");
            cmd.Append(values);
            cmd.Append(")");
            command.Connection = connetion;
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
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
            command.Connection = connetion;
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
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
            command.Connection = connetion;
            command.CommandText = cmd.ToString();
            command.CommandType = CommandType.Text;
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
            prepareProcedureCommand(procedure, parameters);
            int result = -1;
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch(InvalidOperationException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            catch(DbException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            finally
            {
                command.Connection.Close();
            }
            return result > 0;
        }

        public object executeScalarProcedure(string procedure)
        {
            return executeScalarProcedure(procedure, null);
        }

        public object executeScalarProcedure(string procedure, IDbDataParameter[] parameters)
        {
            prepareProcedureCommand(procedure, parameters);
            object result = null;
            try
            {
                command.Connection.Open();
                result = command.ExecuteScalar();
            }
            catch(InvalidOperationException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            catch(DbException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        public DataTable executeDataTableProcedure(string procedure, IDbDataParameter[] parameters)
        {
            prepareProcedureCommand(procedure, parameters);
            DataSet dataSet = new DataSet();
            try
            {
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataSet);
            }
            catch(InvalidOperationException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            catch(DbException ex)
            {
                //Logger.logEvent(ex.ToString(), Logger.LogType.Error, source);
            }
            finally
            {
                command.Connection.Close();
            }
            return dataSet.Tables[0];
        }

        private void prepareProcedureCommand(string procedure, IDbDataParameter[] parameters)
        {
            command.Connection = connetion;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedure;
            if(parameters != null)
            {
                foreach(IDbDataParameter param in parameters)
                {
                    command.Parameters.Add(param);
                }
            }
        }

    }
}
