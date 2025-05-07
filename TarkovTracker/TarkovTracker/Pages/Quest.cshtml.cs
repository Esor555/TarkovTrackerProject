using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTBusinesLogic.BusinesLogic;


namespace TarkovTracker.Pages.Quests
{
    public class IndexModel : PageModel
    {
        private readonly QuestService _questService;

        public IndexModel(IConfiguration config)
        {
            string connStr = config.GetConnectionString("1");
            _questService = new QuestService(connStr);
        }

        public List<Quest> Quests { get; set; }

        [BindProperty]
        public Quest NewQuest { get; set; }

        public void OnGet()
        {
            Quests = _questService.GetAllQuests();
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
    }
}