using FineApi.Domain.Abstractions;
using FineApi.Service.Exception;
using Microsoft.AspNetCore.Mvc;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[AuthorizationFilter]
public class UserCarController : ControllerBase
{
    private IUserCarInformationService _userService;
    
    public UserCarController(IUserCarInformationService userService)
    {
        _userService = userService;
    }
    
    [HttpGet(nameof(GetAllUserCars))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public async Task<IActionResult> GetAllUserCars()
    {
        try
        {
            var cars = await _userService.GetAllUserCarInformation();
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
}

