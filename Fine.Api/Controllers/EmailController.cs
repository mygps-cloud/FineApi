using FineApi.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController:ControllerBase
{
    private readonly INotificationServiceManager _notificationServiceManager;
    public EmailController(INotificationServiceManager notificationServiceManager)
    {
        _notificationServiceManager = notificationServiceManager;
    }
    
    [HttpPost(nameof(SendEmail))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> SendEmail()
    {
        await _notificationServiceManager.SendEmail();
        return Ok();
    }
}