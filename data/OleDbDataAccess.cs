using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace msgen.data
{
    public class OleDbDataAccess : DataAccess
    {

        public OleDbDataAccess(string connectionString) : base(connectionString)
        {
            base.connetion = new OleDbConnection(connectionString);
            base.command = new OleDbCommand();
            base.dataAdapter = new OleDbDataAdapter();
        }

    }
}
