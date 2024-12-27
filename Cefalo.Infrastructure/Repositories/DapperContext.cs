using Cefalo.Core.Common;
using Cefalo.Core.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cefalo.Infrastructure.Repositories
{
    public class DapperContext : IDapperContext, IDisposable
    {

        private readonly DatabaseOptions _databaseOptions;
        private readonly SqlConnection _connection;

        public DapperContext(IConfiguration configuration,
             IOptions<DatabaseOptions> databaseOptions
            )
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnectionStrings");
            //_databaseOptions = databaseOptions.Value;
            _connection = new SqlConnection(ConnectionString);
            TryOpenConnection();
        }

        public DapperContext(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            TryOpenConnection();
        }

        private void TryOpenConnection()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
            }
            catch (Exception) { }
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection.State == ConnectionState.Open) _connection.Close();
            await _connection.DisposeAsync();
        }


        #region QUERY OPERATIONS

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            var result = await _connection.QueryAsync<T>(sql, parameters);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result;
        }

        public async Task<T> First<T>(string sql, object parameters = null)
        {
            var item = await _connection.QueryFirstAsync<T>(sql, parameters);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return item;
        }

        public async Task<T> FirstOrDefault<T>(string sql, object parameters = null)
        {
            var item = await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return item;
        }

        public async Task<T> Single<T>(string sql, object parameters = null)
        {
            var item = await _connection.QuerySingleAsync<T>(sql, parameters);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return item;
        }

        public async Task<T> SingleOrDefault<T>(string sql, object parameters = null)
        {
            var item = await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return item;
        }

        #endregion


        #region COMMAND OPERATIONS

        public async Task<int> RunCommand(string sql, object parameters = null)
        {
            int result = 0;
            using var transaction = _connection.BeginTransaction();
            try
            {
                result = await _connection.ExecuteAsync(sql, parameters, transaction);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            return result;
        }

        public async Task<int> CreateAsync<T>(T value) where T : class
        {
            var result = await _connection.InsertAsync<T>(value);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result;
        }

        public async Task<bool> UpdateAsync<T>(T value) where T : class
        {
            var result = await _connection.UpdateAsync<T>(value);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result;
        }

        public async Task<bool> DeleteAsync<T>(T value) where T : class
        {
            var result = await _connection.DeleteAsync<T>(value);
            return result;
        }

        #endregion


        #region STORED PROCEDURE

        public List<T> StoredProcedureQuery<T>(string sql, object parameters = null)
        {
            IEnumerable<T> result = _connection.Query<T>(sql, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 3600);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result.ToList();
        }

        public async Task<List<T>> StoredProcedureQueryAsync<T>(string sql, object parameters = null)
        {
            IEnumerable<T> result = await _connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 3600);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result.ToList();
        }
        public async Task<T> StoredProcedureQueryFirst<T>(string sql, object parameters = null)
        {
            T result = await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 3600);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result;
        }
        public async Task<int> StoredProcedureCommand(string sql, object parameters = null)
        {
            int result = await _connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 3600);
            if (_connection.State == ConnectionState.Open) _connection.Close();
            return result;
        }

        //public void Dispose()
        //{
        //    //GC.SuppressFinalize(this);
        //    if (_connection.State == ConnectionState.Open) _connection.Close();
        //    _connection.Dispose();
        //}
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private System.Text.Json.Utf8JsonWriter _jsonWriter;
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _jsonWriter?.Dispose();
            }

            _disposed = true;
        }
        #endregion STORED PROCEDURE
    }
}
