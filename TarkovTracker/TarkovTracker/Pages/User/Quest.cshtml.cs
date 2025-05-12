using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTBusinesLogic.BusinesLogic;
using TTBusinesLogic.Interfaces;

public class QuestsModel : PageModel
{
	private readonly IUserQuestService _userQuestService;

	public List<UserQuest> UserQuests { get; set; }

	public QuestsModel(IUserQuestService userQuestService)
	{
		_userQuestService = userQuestService;
	}

	public void OnGet()
	{
		int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
		UserQuests = _userQuestService.GetAllUserQuests(userId);
	}

	public IActionResult OnPostUpdateStatus(int questId, string newStatus)
	{
		int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		var updated = _userQuestService.Update(new UserQuest()
		{
			UserId = userId,
			QuestId = questId,
			Status = newStatus
		});

		if (!updated)
		{
			// Optionally handle failure
		}

		return RedirectToPage(); // Reload the page with updated data
	}
}