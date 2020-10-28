using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

using com.Lighteous.Pokayoke.Enums;
using com.Lighteous.Pokayoke.Utils;

namespace com.Lighteous.Pokayoke {
    public class DatabasePokayoke : PokayokeBase {

        public DatabasePokayoke(string connectionString, DbConnectionType dbType) : base() { _connectionString = connectionString; _dbType = dbType; Title = string.Format("{0} Available", DatabaseCatalog.Name(connectionString)); }
        public DatabasePokayoke(string name, string connectionString, DbConnectionType dbType) : base() { _connectionString = connectionString; _dbType = dbType; Title = string.Format("{0} Database Available", name); }

        private string _connectionString { get; set; }
        private DbConnectionType _dbType { get; set; }

        public override void Test() {
            switch(_dbType) {
                case DbConnectionType.SQLServer:
                    TestSqlConnection();
                    break;
                case DbConnectionType.OLEDB:
                    TestOLEDBConnection();
                    break;
                case DbConnectionType.ODBC:
                    TestODBCConnection();
                    break;
            }
        }

        private void TestSqlConnection() {

            var connection = new SqlConnection();

            try {
                if (!string.IsNullOrEmpty(_connectionString)) {
                    connection = new SqlConnection(_connectionString);
                    connection.Open();
                    Pass = true;
                    Fail = !Pass;
                }
            }
            catch(Exception err) {
                Errors = ErrorMessage.Coalesce(err);
                Pass = false;
                Fail = !Pass;
            }
            finally {
                if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
            }

        }

        private void TestOLEDBConnection() {

            var connection = new OleDbConnection();

            try {
                if (!string.IsNullOrEmpty(_connectionString)) {
                    connection = new OleDbConnection(_connectionString);
                    connection.Open();
                    Pass = true;
                    Fail = !Pass;
                }
            }
            catch (Exception err) {
                Errors = ErrorMessage.Coalesce(err);
                Pass = false;
                Fail = !Pass;
            }
            finally {
                if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
            }

        }

        private void TestODBCConnection() {

            var connection = new OdbcConnection();

            try {
                if (!string.IsNullOrEmpty(_connectionString)) {
                    connection = new OdbcConnection(_connectionString);
                    connection.Open();
                    Pass = true;
                    Fail = !Pass;
                }
            }
            catch (Exception err) {
                Errors = ErrorMessage.Coalesce(err);
                Pass = false;
                Fail = !Pass;
            }
            finally {
                if (connection.State == System.Data.ConnectionState.Open) { connection.Close(); }
            }

        }
    }
}
