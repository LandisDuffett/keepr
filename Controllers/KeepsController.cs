using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetMyKeeps()
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to see your keeps.");
        }
        return Ok(_ks.GetMyKeeps(user.Value));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [Authorize]
    [HttpPut]
    public ActionResult<Keep> Update([FromBody] Keep updatedKeep)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to put a keep in your vault.");
        }
        return Ok(_ks.Update(updatedKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Keep> Update(int id, [FromBody] Keep updatedKeep)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if ((user == null) || (user.Value != updatedKeep.UserId))
        {
          throw new Exception("You must be logged in to put a keep in your vault.");
        }
        updatedKeep.UserId = user.Value;
        updatedKeep.Id = id;
        return Ok(_ks.Update(updatedKeep));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        var res = _ks.GetById(id);
        if (res.IsPrivate)
        {
          Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
          if ((user == null) || (user.Value != res.UserId))
          {
            throw new Exception("You do not have access to this keep.");
          }
          return Ok(_ks.GetById(id));
        }
        else
        {
          return Ok(_ks.GetById(id));
        }
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newKeep.UserId = userId;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        Claim user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (user == null)
        {
          throw new Exception("You must be logged in to delete any keeps.");
        }
        return Ok(_ks.Delete(user.Value, id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }
}