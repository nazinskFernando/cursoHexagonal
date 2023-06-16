
using Application.Guest.Requests;

namespace Application.Guest.Ports
{
    public interface IGuestManager
    {
        Task<Response> CreateGuest(CreateGuestRequest request);
    }
}
