using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM Keeps WHERE isPrivate = 0;";
      return _db.Query<Keep>(sql);
    }

    internal Keep GetById(int id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id;";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal IEnumerable<Keep> GetMyKeeps(string userId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @userId;";
      return _db.Query<Keep>(sql, new { userId });
    }

    public bool Delete(string userId, int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id AND userId = @userId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, new { userId, id });
      return rowsAffected == 1;
    }

    public bool Update(Keep updatedKeep)
    {
      string sql = @"UPDATE keeps
            SET
            name = @name,
            description = @description,
            img = @img,
            isPrivate = @isPrivate,
            views = @views,
            shares = @shares,
            keeps = @keeps
            WHERE id = @id AND userid = @userId LIMIT 1;";
      int rowsAffected = _db.Execute(sql, updatedKeep);
      return rowsAffected == 1;
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"INSERT INTO keeps
            (name, description, userId, img, isPrivate, views, shares, keeps)
            VALUES
            (@name, @description, @userId, @img, @isPrivate, @views, @shares, @keeps);
            SELECT LAST_INSERT_ID();";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      return newKeep;
    }
  }
}