using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {

    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      int id = _repo.Create(newVaultKeep);
      newVaultKeep.Id = id;
      return newVaultKeep;
    }

    internal IEnumerable<VaultKeepViewModel> GetKeepsByVaultId(int keepId, string userId)
    {
      return _repo.GetKeepsByVaultId(keepId, userId);
    }
    public VaultKeep GetById(int Id)
    {
      VaultKeep exists = _repo.GetById(Id);
      if (exists == null) { throw new Exception("Invalid keep id"); }
      return exists;
    }

    internal VaultKeep Delete(int id)
    {
      VaultKeep exists = GetById(id);
      _repo.Delete(id);
      return exists;
    }
  }
}