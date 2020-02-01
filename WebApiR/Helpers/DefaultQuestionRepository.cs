using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultQuestionRepository : IQuestionRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultQuestionRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public Question Add(Question question)
        {
            this._context.Questions.Add(question);
            if (this._context.SaveChanges() != 1)
                throw new Exception("Unable to save question in Database.");

            return this.Get(question.QuestionId);
        }

        public bool Delete(int QuestionId)
        {
            var questionRemoved = _context.Questions.Find(QuestionId);
            if (questionRemoved != null)
            {
                _context.Remove(questionRemoved);
                var performedOperations = _context.SaveChanges();
                if (performedOperations > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public List<Question> Get()
        {
            return _context.Questions.ToList();
        }

        public Question Get(int QuestionId)
        {
            var question = _context.Questions.Find(QuestionId);
            return question;
        }

        public Question Update(int QuestionId,Question questionP)
        {
            var question = _context.Questions.Find(QuestionId);
            if (question != null)
            {
                question.Data = questionP.Data;
                _context.SaveChanges();
                return this.Get(question.QuestionId);
            }
            else
                return null;
        }
    }
}
