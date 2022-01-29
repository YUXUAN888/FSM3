using MojangAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.SecurityQuestion
{
    public class QuestionFlowResponse : MojangAPIResponse
    {
        public QuestionFlowResponse(QuestionList questions)
        {
            this.Questions = questions;
        }

        public QuestionList Questions { get; }
    }
}
