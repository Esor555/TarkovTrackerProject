using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BaseObjects.BaseObject
{
    public class Quest
    {
        private int id;
        public int Id { get => id; set => id = value; }

        private string title;
        public string Title { get => title; set => title = value; }

        private string description;
        public string Description { get => description; set => description = value; }

        private int requiredLevel;
        public int RequiredLevel { get => requiredLevel; set => requiredLevel = value; }

        private int? previousQuestId;
        public int? PreviousQuestId { get => previousQuestId; set => previousQuestId = value; }

        private int traderId;
        public int TraderId { get => traderId; set => traderId = value; }

        private string wikiLink;
        public string WikiLink { get => wikiLink; set => wikiLink = value; }

        public Quest(int id, string title, string description, int requiredLevel, int? previousQuestId, int traderId, string wikiLink)
        {
            Id = id;
            Title = title;
            Description = description;
            RequiredLevel = requiredLevel;
            PreviousQuestId = previousQuestId;
            TraderId = traderId;
            WikiLink = wikiLink;
        }
        public Quest() { }
    }

}
