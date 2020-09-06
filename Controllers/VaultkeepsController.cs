using Microsoft.AspNetCore.Mvc;
using Keepr.Models;
using Keepr.Services;
using System.Collections.Generic;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Keepr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultkeepsController : ControllerBase
    {
        private readonly VaultkeepsService _service;

        public VaultkeepsController(VaultkeepsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public ActionResult<Vaultkeep> GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Vaultkeep> Post([FromBody] Vaultkeep newVaultkeep)
        {

            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                newVaultkeep.VkuserId = userId;
                return Ok(_service.Create(newVaultkeep));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Vaultkeep> Delete(int id)
        {
            try
            {
                return Ok(_service.Delete(id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}