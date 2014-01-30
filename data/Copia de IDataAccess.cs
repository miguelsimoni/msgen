using System;
using System.Data;
using System.Data.Common;

namespace msgen.data
{
    interface IDataAccess
    {
        string ConnectionString
        {
            get;
            set;
        }

        DataTable query(string table, string fields, string expression);

        int insert(string table, string fields, string values);

        int update(string table, string field, string value, string expression);

        int delete(string table, string expression);

        bool executeProcedure(string procedure, IDbDataParameter[] parameters);

        object executeScalarProcedure(string procedure, IDbDataParameter[] parameters);
    }
}
