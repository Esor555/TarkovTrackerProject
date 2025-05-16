using System.Security.Claims;
using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.test;

namespace TarkovTracker.Pages.quest{
public class IndexModel : PageModel
{
	private readonly QuestService _questService;
	private readonly IUserQuestService _userQuestService;

	public IndexModel(IConfiguration config)
	{
		string connStr = config.GetConnectionString("1");
		_questService = new QuestService(connStr);
		_userQuestService = new UserQuestService(new UserQuestRepository(connStr));
	}

	public List<Quest> Quests { get; set; }
	public List<UserQuest> UserQuests { get; set; }

	[BindProperty]
	public Quest NewQuest { get; set; }

	public void OnGet()
	{
		int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
		Quests = _questService.GetAllQuests();
		UserQuests = _userQuestService.GetAllUserQuests(userId)
			.Where(q => q.Status == "InProgress")
			.ToList();
	}

	public IActionResult OnPostAdd()
	{
		if (!ModelState.IsValid || NewQuest == null)
		{
			OnGet();
			return Page();
		}

		_questService.AddQuest(NewQuest);
		return RedirectToPage();
	}

	public IActionResult OnPostDelete(int id)
	{
		_questService.DeleteQuest(id);
		return RedirectToPage();
	}

	public IActionResult OnPostAssignToUser(int questId)
	{
		int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		var existing = _userQuestService.GetAllUserQuests(userId)
			.FirstOrDefault(q => q.QuestId == questId);

		if (existing == null)
		{
			var userQuest = new UserQuest
			{
				UserId = userId,
				QuestId = questId,
				Status = "InProgress"
			};
			_userQuestService.Add(userQuest);
		}

		return RedirectToPage();
	}

	public IActionResult OnPostComplete(int questId)
	{
		int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

		var userQuest = _userQuestService.GetAllUserQuests(userId)
			.FirstOrDefault(q => q.QuestId == questId);

		if (userQuest != null && userQuest.Status == "InProgress")
		{
			userQuest.Status = "Completed";
			_userQuestService.Update(userQuest);
		}

		return RedirectToPage();
	}

	public bool UserHasQuest(int questId)
	{
		return UserQuests.Any(q => q.QuestId == questId);
	}
	public UserQuest GetUserQuest(int questId)
	{
		return UserQuests.FirstOrDefault(q => q.QuestId == questId);
	}
}
	}