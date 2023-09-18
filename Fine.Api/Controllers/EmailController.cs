using FineApi.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController:ControllerBase
{
    private readonly IEmailSenderService _emailSenderService;
    public EmailController(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }
    
    [HttpPost(nameof(SendEmail))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> SendEmail()
    {
        await _emailSenderService.SendEmail();
        return Ok();
    }
}