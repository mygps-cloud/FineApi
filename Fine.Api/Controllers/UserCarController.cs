﻿using AutoMapper;
using Fine.Api.Filters;
using Fine.Api.VMs;
using FineApi.Service.Abstractions;
using FineApi.Service.DTOs;
using FineApi.Service.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[AuthorizationFilter]
public class UserCarController : ControllerBase
{
    private IMapper _mapper;
    private IUserCarInformationService _userService;
    private ILoggerService _loggerService;
    
    public UserCarController(IUserCarInformationService userService,IMapper mapper, ILoggerService loggerService)
    {
        _userService = userService;
        _mapper = mapper;
        _loggerService = loggerService;
    }
    
    [HttpGet(nameof(GetAllUserCars))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> GetAllUserCars([FromQuery]NexCarVM next)
    {
        try
        {
            var result = _mapper.Map<NextCarDTO>(next);
            var cars = await _userService.GetAllUserCarInformation(result);
            return Ok(cars);
        }
        catch (NoUserCarInformationException ex)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(417, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost(nameof(AddError))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> AddError(ErrorVM error)
    {
        try
        {
            var result = _mapper.Map<ErrorDto>(error);
            await _loggerService.PublishLog(result);
            return Ok();
        }
        catch (NoUserCarInformationException ex)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(417, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

