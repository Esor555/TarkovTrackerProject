using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class QuestsModel : PageModel
{
    public record QuestStatus(int QuestId, string Title, string Status);

    public List<QuestStatus> Quests = new();

    public async Task OnGetAsync()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        using var connection = new SqlConnection("YourConnectionString");
        await connection.OpenAsync();

        var command = new SqlCommand(@"
            SELECT q.id, q.title, ISNULL(uq.status, 'NotStarted') as status
            FROM quest q
            LEFT JOIN user_quest uq ON q.id = uq.quest_id AND uq.user_id = @userId
            ORDER BY q.title;", connection);

        command.Parameters.AddWithValue("@userId", userId);

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Quests.Add(new QuestStatus(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2)
            ));
        }
    }
}