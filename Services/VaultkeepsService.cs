using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultkeepsService
    {

        private readonly VaultkeepsRepository _repo;

        public VaultkeepsService(VaultkeepsRepository repo)
        {
            _repo = repo;
        }

        internal Vaultkeep Create(Vaultkeep newVaultkeep)
        {
            int id = _repo.Create(newVaultkeep);
            newVaultkeep.Id = id;
            return newVaultkeep;
        }

        internal IEnumerable<VaultkeepViewModel> GetKeepsByVaultId(int keepId, string userId)
        {
            return _repo.GetKeepsByVaultId(keepId, userId);
        }
        public Vaultkeep GetById(int Id)
        {
            Vaultkeep exists = _repo.GetById(Id);
            if (exists == null) { throw new Exception("Invalid keep id"); }
            return exists;
        }

        internal Vaultkeep Delete(int id)
        {
            Vaultkeep exists = GetById(id);
            _repo.Delete(id);
            return exists;
        }
    }
}