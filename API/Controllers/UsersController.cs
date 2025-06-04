using System;
using System.Linq.Expressions;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    var users = await userRepository.GetMembersAsync();

    return Ok(users);
  }


  [HttpGet("{id:int}")]
  public async Task<ActionResult<MemberDto>> GetUser(int id)
  {
    var user = await userRepository.GetUserByIdAsync(id);
    if (user == null)
    {
      return NotFound();
    }

    var userToReturn = mapper.Map<MemberDto>(user);

    return Ok(userToReturn);
  }


  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDto>> GetUser(string username)
  {
    var user = await userRepository.GetMemberAsync(username);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }
}
