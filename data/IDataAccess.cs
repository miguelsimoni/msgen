using System;
using System.Data;
using System.Data.Common;

namespace msgen.data
{
    public interface IDataAccess
    {
        string ConnectionString
        {
            get;
            set;
        }

        bool checkConnection();

        int executeSql(string sql);

        DataTable query(string table);

        DataTable query(string table, string fields);

        DataTable query(string table, string fields, string expression);

        int insert(string table, string fields, string values);

        int insert(string table, string values);

        int update(string table, string field, string value, string expression);

        int delete(string table);

        int delete(string table, string expression);

        bool executeProcedure(string procedure, IDbDataParameter[] parameters);

        object executeScalarProcedure(string procedure, IDbDataParameter[] parameters);

        DataTable executeDataTableProcedure(string procedure, IDbDataParameter[] parameters);

    }
}
