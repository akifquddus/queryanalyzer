using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface IAnswersRepository
    {
        Answers Add(Answers answer);

        Answers Update(int AnswerId, Answers answer);

        Answers Get(int AnswerId);

        List<Answers> Get();

        Answers GetByUserQuestion(int userid, int questionid);

        bool Delete(int AnswerId);
    }
}
