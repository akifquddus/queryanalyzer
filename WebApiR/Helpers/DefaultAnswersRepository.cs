using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultAnswersRepository : IAnswersRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultAnswersRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public Answers Add(Answers answer)
        {
            this._context.Answers.Add(answer);
            if (this._context.SaveChanges() != 1)
                return null;

            return this.Get(answer.AnswerId);
        }

        public bool Delete(int AnswerId)
        {
            var AnswersRemoved = _context.Answers.Find(AnswerId);
            if (AnswersRemoved != null)
            {
                _context.Remove(AnswersRemoved);
                var performedOperations = _context.SaveChanges();
                if (performedOperations > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public List<Answers> Get()
        {
            return _context.Answers.ToList();
        }

        public Answers Get(int AnswersId)
        {
            var answer = _context.Answers.Find(AnswersId);
            return answer;
        }

        public Answers GetByUserQuestion(int userid, int questionid)
        {
            Answers answer = this._context.Answers.Where(_ => _.UserId == userid && _.QuestionId == questionid).FirstOrDefault();
            return answer;
        }

        public Answers Update(int AnswerId, Answers answerP)
        {
            var answer = _context.Answers.Find(AnswerId);
            if (answer != null)
            {
                answer.Data = answerP.Data;
                _context.SaveChanges();
                return this.Get(answer.QuestionId);
            }
            else
                return null;
        }
    }
}
