    using BaseObjects.BaseObject;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using TarkovTrackerBLL.Service;


    namespace TarkovTracker.Pages
    {
        public class QuestDetailModel : PageModel
        {
            private readonly QuestService _questService;
            private readonly TraderService _traderService;
            public QuestDetailModel(IConfiguration config)
            {
                var connStr = config.GetConnectionString("1");
                _questService = new QuestService(connStr);
                _traderService = new TraderService(connStr);
            }

            [BindProperty(SupportsGet = true)]
            public int Id { get; set; }

            public Quest Quest { get; set; }
            public Quest PreviousQuest { get; set; }  
            public string TraderName { get; set; }
            public IActionResult OnGet()
            {
                
                if (Id <= 0)
                    return NotFound();

                Quest = _questService.GetQuestById(Id);
                if (Quest == null)
                    return NotFound();

                if (Quest.PreviousQuestId.HasValue)
                    PreviousQuest = _questService.GetQuestById(Quest.PreviousQuestId.Value);
                if(Quest.TraderId != null && Quest.TraderId > 0)
                {
                    try
                    {
                        TraderName = _traderService.GetById(Quest.TraderId).Name;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    } 
                    
                }
                return Page();
            }
        }
    }