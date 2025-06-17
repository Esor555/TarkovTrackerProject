using System.Security.Claims;
using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.Service;

public class QuestsModel : PageModel
{
    private readonly UserQuestPageService _pageService;
    private readonly UserService _userService;
    private readonly UserQuestService _userQuestService;
    private readonly QuestService _questService;

    public List<UserQuest> UserQuests { get; set; }
    public List<Quest> AvailableQuests { get; set; }

    public QuestsModel(
        UserQuestPageService pageService,
        UserService userService,
        UserQuestService userQuestService,
        QuestService questService)
    {
        _pageService = pageService;
        _userService = userService;
        _userQuestService = userQuestService;
        _questService = questService;
    }

    public void OnGet()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        UserQuests = _pageService.GetUserQuests(userId);

        var userLevel = _userService.GetUserById(userId).Level;
        var userQuests = _userQuestService.GetAllUserQuests(userId);

        AvailableQuests = _questService.GetAvailableStartingQuestsForUser(userId, userLevel, userQuests);

        foreach (var quest in AvailableQuests)
        {
            bool alreadyAssigned = userQuests.Any(uq => uq.QuestId == quest.Id);
            if (!alreadyAssigned)
            {
                var userQuest = new UserQuest
                {
                    UserId = userId,
                    QuestId = quest.Id,
                    Status = "NotStarted"
                };

                _userQuestService.Add(userQuest);
                UserQuests = _pageService.GetUserQuests(userId);
            }
        }
    }

    public IActionResult OnPostUpdateStatus(int questId, string newStatus)
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        _pageService.UpdateQuestStatus(userId, questId, newStatus);

        if (newStatus == "Completed")
        {
            var quest = _questService.GetQuestById(questId);
            string questTitle = quest?.Title ?? "Unknown Quest";

            TempData["SuccessMessage"] = $"Quest \"{questTitle}\" completed successfully!";

            var nextQuests = _questService.GetQuestsByPreviousQuestId(questId);
            var userQuests = _userQuestService.GetAllUserQuests(userId);

            foreach (var nextQuest in nextQuests)
            {
                bool alreadyAssigned = userQuests.Any(uq => uq.QuestId == nextQuest.Id);
                if (!alreadyAssigned)
                {
                    var userQuest = new UserQuest
                    {
                        UserId = userId,
                        QuestId = nextQuest.Id,
                        Status = "NotStarted"
                    };

                    _userQuestService.Add(userQuest);
                }
            }
        }

        return RedirectToPage();
    }

}
