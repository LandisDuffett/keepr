using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    public Vault GetById(int id)
    {
      Vault foundVault = _repo.GetById(id);
      if (foundVault == null)
      {
        throw new Exception("Invalid vault Id");
      }
      return foundVault;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }
    public string Delete(string userId, int id)
    {
      //GetById(id);
      bool deleted = _repo.Delete(userId, id);
      if (!deleted)
      {
        throw new Exception("You are not the creator of this vault!");
      }
      return "Deleted!";
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
  }
}