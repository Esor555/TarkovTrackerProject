using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class HideoutModel : PageModel
{
    public record StationStatus(int StationId, string Name, int Level);

    public List<StationStatus> Stations = new();

    public async Task OnGetAsync()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        
    }
}