using Application;
using Application.Guest.DTO;
using Application.Guest.Ports;
using Microsoft.AspNetCore.Mvc;
using Application.Guest.Requests;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuestsController : Controller
    {
        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestManager _ports;

        public GuestsController(ILogger<GuestsController> logger, IGuestManager gestManager)
        {
            _logger = logger;
            _ports = gestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guest)
        {
            var request = new CreateGuestRequest { Data = guest };
            var res = await _ports.CreateGuest(request);

            if (res.Success) return Created("", res);

            if(res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }
            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
