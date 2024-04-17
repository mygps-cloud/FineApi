using AutoMapper;
using Fine.Api.VMs;
using FineApi.Service.Abstractions;
using FineApi.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Fine.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[AuthorizationFilter]
public class ReceivedSmsController : ControllerBase
{
    private readonly IReceivedSmsService _receivedSmsService;
    private readonly IMapper _mapper;
    public ReceivedSmsController(IReceivedSmsService receivedSmsService, IMapper mapper)
    {
        _receivedSmsService = receivedSmsService;
        _mapper = mapper;
    }
    [HttpPatch(nameof(UpdateFineStatus))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateFineStatus(List<FineDataVm> data)
    {
        try
        {
            var result=_mapper.Map<List<FineDataDto>>(data);
            await _receivedSmsService.UpdateReceivedSms(result);
            return Ok(new {message = "Fine Status Updated Succesfully",data = "updated"});
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "an error occured");
        }
    }
}

