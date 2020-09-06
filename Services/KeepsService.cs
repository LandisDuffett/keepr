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