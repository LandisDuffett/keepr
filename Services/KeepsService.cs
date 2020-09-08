using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    public Keep GetById(int id)
    {
      Keep foundKeep = _repo.GetById(id);
      if (foundKeep == null)
      {
        throw new Exception("Invalid keep Id");
      }
      return foundKeep;
    }
    public IEnumerable<Keep> GetMyKeeps(string userId)
    {

      return _repo.GetMyKeeps(userId);
    }

    public Keep Update(Keep updatedKeep)
    {
      Keep foundKeep = GetById(updatedKeep.Id);
      updatedKeep.Id = foundKeep.Id;
      updatedKeep.UserId = foundKeep.UserId;
      updatedKeep.Name = updatedKeep.Name == null ? foundKeep.Name : updatedKeep.Name;
      updatedKeep.Description = updatedKeep.Description == null ? foundKeep.Description : updatedKeep.Description;
      updatedKeep.Img = updatedKeep.Img == null ? foundKeep.Img : updatedKeep.Img;
      updatedKeep.IsPrivate = updatedKeep.IsPrivate != foundKeep.IsPrivate ? updatedKeep.IsPrivate : foundKeep.IsPrivate;
      updatedKeep.Shares = updatedKeep.Shares > foundKeep.Shares ? updatedKeep.Shares : foundKeep.Shares;
      updatedKeep.Views = updatedKeep.Views > foundKeep.Views ? updatedKeep.Views : foundKeep.Views;
      updatedKeep.Keeps = updatedKeep.Keeps > foundKeep.Keeps ? updatedKeep.Keeps : foundKeep.Keeps;
      bool updated = _repo.Update(updatedKeep);
      if (!updated)
      {
        throw new Exception("Sorry, you are not the owner of this keep and cannot edit it.");
      }
      return updatedKeep;
    }

    public string Delete(string userId, int id)
    {
      GetById(id);
      bool deleted = _repo.Delete(userId, id);
      if (!deleted)
      {
        throw new Exception("You are not the creator of this keep!");
      }
      return "Deleted!";
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }
  }
}