using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BDModel
{
    public partial class SistemaWebEntities
    {
        private static string _DbConnection = @"Server=GARYDIAZ\GEDP;Database=SistemaWeb;Persist Security Info=True;User=sa;Password=gadipa90;";       
        private static string _EfConnection = "metadata=res://*/SistemaWeb.csdl|res://*/SistemaWeb.ssdl|res://*/SistemaWeb.msl;provider=System.Data.SqlClient;provider connection string='";
        private static string _DefaultConnectionString;
        public static string DefaultConnectionString
        {
            get
            {
                return _DefaultConnectionString ?? (_DefaultConnectionString = string.Concat(_EfConnection, _DbConnection, "'"));
            }
        }

        public static void SetDbConnection(string server, string user, string password, string database)
        {
            _DbConnection = "Server=" + server + ";Database=" + database + ";Persist Security Info=True;User=" + user + ";Password=" + password + ";";
            _DefaultConnectionString = string.Concat(_EfConnection, _DbConnection, "'");
        }

        public static string RefreshDbConnection()
        {
            return _DefaultConnectionString = string.Concat(_EfConnection, _DbConnection, "'");
        }

        public DbTransaction BeginTransaction()
        {
            if (this.Database.Connection == null) return null;

            if (this.Database.Connection == null) throw new ApplicationException("No Existing Connection to DB");
            if (this.Database.Connection.State == System.Data.ConnectionState.Closed) this.Database.Connection.Open();

            return this.Database.Connection.BeginTransaction();
        }

        public bool ConnectionIsOpen()
        {
            return this.Database.Connection == null || this.Database.Connection.State == System.Data.ConnectionState.Open;
        }

        public void CloseConnection()
        {
            if (this.Database.Connection == null) return;

            if (this.Database.Connection.State != System.Data.ConnectionState.Closed) this.Database.Connection.Close();
        }
    }
}
