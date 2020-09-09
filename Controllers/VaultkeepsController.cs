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
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _service;

    public VaultKeepsController(VaultKeepsService service)
    {
      _service = service;
    }

    [HttpGet("{id}")]
    public ActionResult<VaultKeep> GetById(int id)
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
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep newVaultKeep)
    {

      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newVaultKeep.UserId = userId;
        return Ok(_service.Create(newVaultKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<VaultKeep> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to put a keep in your vault.");
        }
        return Ok(_service.Delete(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}