using Microsoft.AspNetCore.Mvc;
using FineApi.Domain.Abstractions;
using FineApi.Service.Exception;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[AuthorizationFilter]
public class ReceivedSmsController : ControllerBase
{
    private readonly IReceivedSmsService _receivedSmsService;
    public ReceivedSmsController(IReceivedSmsService receivedSmsService)
    {
        _receivedSmsService = receivedSmsService;
    }
    [HttpPatch(nameof(UpdateFineStatus))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateFineStatus(string receiptNumber,bool paid)
    {
        try
        {
             await _receivedSmsService.UpdateReceivedSms(receiptNumber, paid);
            return Ok(new {message = "Fine Status Updated Succesfully",data = "updated"});
        }
        catch (NoReceivedSmsOnThisReceiptNumberException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "an error occured");
        }
    }
}

