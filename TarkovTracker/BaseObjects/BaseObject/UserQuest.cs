﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects.BaseObject
{
    public class UserQuest
    {
        private int userId;
        public int UserId { get => userId; set => userId = value; }

        private int questId;
        public int QuestId { get => questId; set => questId = value; }

        private string status;
        public string Status { get => status; set => status = value; }
        public Quest Quest { get; set; }

        public UserQuest(int userId, int questId, string status)
        {
            UserId = userId;
            QuestId = questId;
            Status = status;
        }
        public UserQuest() { }
    }
}
