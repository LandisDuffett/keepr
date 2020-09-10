using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep newVaultKeep)
    {
      string sql = @"
        INSERT INTO vaultkeeps
        (vaultId, keepId, userId)
        VALUES
        (@VaultId, @KeepId, @userId);
        SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
      string sql = @"
        SELECT 
        k.*,
        vk.id as vaultkeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE(vaultId = @vaultId AND vk.userId = @userId);";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId, userId });
    }


    internal VaultKeep GetById(int id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @Id;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultkeeps WHERE id = @Id;";
      _db.Execute(sql, new { id });

    }
  }
}