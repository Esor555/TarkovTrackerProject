using BaseObjects.BaseObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TarkovTrackerBLL.Service;


namespace TarkovTracker.Pages
{
    public class QuestDetailModel : PageModel
    {
        private readonly QuestService _questService;

        public QuestDetailModel(IConfiguration config)
        {
            var connStr = config.GetConnectionString("1");
            _questService = new QuestService(connStr);
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Quest Quest { get; set; }
        public Quest PreviousQuest { get; set; }  

        public IActionResult OnGet()
        {
            if (Id <= 0)
                return NotFound();

            Quest = _questService.GetQuestById(Id);
            if (Quest == null)
                return NotFound();

            if (Quest.PreviousQuestId.HasValue)
                PreviousQuest = _questService.GetQuestById(Quest.PreviousQuestId.Value);

            return Page();
        }
    }
}