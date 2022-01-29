using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojangAPI.SecurityQuestion
{
    public class QuestionList : IEnumerable<Question>
    {
        internal QuestionList(Question[] q)
        {
            this.questions = new List<Question>(3);
            questions.AddRange(q);
        }

        public Question this[int index] => questions[index];
        public int Count => questions.Count;

        List<Question> questions;

        public Question GetQuestion(int index) => this[index];

        public bool CheckAllAnswered()
        {
            foreach (var item in this)
            {
                if (string.IsNullOrEmpty(item.Answer))
                    return false;
            }

            return true;
        }

        public IEnumerator<Question> GetEnumerator()
        {
            return questions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
