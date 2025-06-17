using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.test;


namespace TarkovTrackerBLL.Service
{
    public class QuestService
    {
        private readonly IquestRepository _questRepository;

        public QuestService(string connectionString)
        {
            _questRepository = new QuestRepository(connectionString);
        }
        public List<Quest> GetAvailableStartingQuestsForUser(int userId, int userLevel, List<UserQuest> userQuests)
        {
            var allQuests = GetAllQuests();

            var assignedQuestIds = userQuests
                .Where(q => q.UserId == userId)
                .Select(q => q.QuestId)
                .ToHashSet();

            return allQuests
                .Where(q => q.PreviousQuestId == null &&
                            q.RequiredLevel <= userLevel &&
                            !assignedQuestIds.Contains(q.Id))
                .ToList();
        }
        public List<Quest> GetQuestsByPreviousQuestId(int previousQuestId)
        {
	        return _questRepository.GetAll()
		        .Where(q => q.PreviousQuestId == previousQuestId)
		        .ToList();
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
