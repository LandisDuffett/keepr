using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }


        /*internal Vault GetById(int id)
        {
            string sql = "SELECT * FROM vaults WHERE id = @Id;";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }*/

        internal IEnumerable<Vault> Get(string userId)
        {
            string sql = "SELECT * FROM vaults WHERE userId = @userId;";
            return _db.Query<Vault>(sql, new { userId });
        }
        public bool Delete(string userId, int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id AND userId = @userId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { userId, id });
            return rowsAffected == 1;
        }

        internal Vault Create(Vault newVault)
        {
            string sql = @"INSERT INTO vaults
            (name, description, userId)
            VALUES
            (@name, @description, @userId);
            SELECT LAST_INSERT_ID();";
            newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
            return newVault;
        }
    }
}