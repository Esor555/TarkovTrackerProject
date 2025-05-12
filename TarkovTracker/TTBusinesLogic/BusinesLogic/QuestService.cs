using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.DAL;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.BusinesLogic
{
    public class QuestService
    {
        private readonly IquestRepository _questRepository;

        public QuestService(string connectionString)
        {
            _questRepository = new QuestRepository(connectionString);
        }

        public List<Quest> GetAllQuests()
        {
            return _questRepository.GetAll();
        }

        public Quest GetQuestById(int id)
        {
            return _questRepository.GetById(id);
        }

        public Quest GetQuestByTitle(string title)
        {
            return _questRepository.GetByName(title);
        }

        public bool AddQuest(Quest quest)
        {
            if (string.IsNullOrWhiteSpace(quest.Title)) return false;
            return _questRepository.Add(quest);
        }

        public bool UpdateQuest(Quest quest)
        {
            return _questRepository.Update(quest);
        }

        public bool DeleteQuest(int id)
        {
            return _questRepository.Delete(id);
        }
    }
}
