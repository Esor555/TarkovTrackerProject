using System.Security.Claims;
using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.Service;

public class QuestsModel : PageModel
{
    private readonly UserQuestPageService _pageService;

    public List<UserQuest> UserQuests { get; set; }

    public QuestsModel(UserQuestPageService pageService)
    {
        _pageService = pageService;
    }

    public void OnGet()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        UserQuests = _pageService.GetUserQuests(userId);
    }

    public IActionResult OnPostUpdateStatus(int questId, string newStatus)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        _pageService.UpdateQuestStatus(userId, questId, newStatus);
        return RedirectToPage();
    }
}