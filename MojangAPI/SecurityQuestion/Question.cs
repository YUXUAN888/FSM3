using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.SecurityQuestion
{
    public class Question
    {
        public Question(int questionId, string questionMsg, int answerId)
        {
            this.QuestionId = questionId;
            this.QuestionMessage = questionMsg;
            this.AnswerId = answerId;
        }

        public int QuestionId { get; internal set; }
        public string QuestionMessage { get; internal set; }
        public int AnswerId { get; internal set; }
        public string Answer { get; set; }
    }
}
