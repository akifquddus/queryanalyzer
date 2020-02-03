using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface IGrowthRepository
    {
        Growth Add(Growth growth);

        Growth Update(int id, Growth growth);

        Growth Get(int growthId);

        List<Growth> GetbyUser(int userId);

        List<Growth> Get();

        bool Delete(int growthId);
    }
}