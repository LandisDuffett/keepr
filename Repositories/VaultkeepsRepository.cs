using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultkeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultkeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal int Create(Vaultkeep newVaultkeep)
        {
            string sql = @"
        INSERT INTO vaultkeeps
        (vaultId, keepId)
        VALUES
        (@VaultId, @KeepId);
        SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVaultkeep);
        }

        internal IEnumerable<VaultkeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
        {
            string sql = @"
        SELECT 
        k.*,
        vk.id as vaultkeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE(vaultId = @vaultId AND userId = @userId);";
            return _db.Query<VaultkeepViewModel>(sql, new { vaultId, userId });
        }


        internal Vaultkeep GetById(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @Id";
            return _db.QueryFirstOrDefault<Vaultkeep>(sql, new { id });
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @Id";
            _db.Execute(sql, new { id });

        }
    }
}