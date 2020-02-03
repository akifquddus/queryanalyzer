using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface IQuestionRepository
    {
        Question Add(Question question);

        Question Update(int QuestionId, Question question);

        Question Get(int QuestionId);

        List<Question> Get();

        bool Delete(int QuestionId);
    }
}
