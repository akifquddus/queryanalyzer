using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultGrowthRepository : IGrowthRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultGrowthRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public Growth Add(Growth growth)
        {
            this._context.Growth.Add(growth);
            if (this._context.SaveChanges() != 1)
                throw new Exception("Unable to save question in Database.");

            return this.Get(growth.GrowthId);
        }

        public bool Delete(int growthId)
        {
            var growthRemoved = _context.Growth.Find(growthId);
            if (growthRemoved != null)
            {
                _context.Remove(growthRemoved);
                var performedOperations = _context.SaveChanges();
                if (performedOperations > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public Growth Get(int growthId)
        {
            var growth = _context.Growth.Find(growthId);
            return growth;
        }

        public List<Growth> Get()
        {
            return _context.Growth.ToList();
        }

        public List<Growth> GetbyUser(int userId)
        {
            List<Growth> growth = this._context.Growth.Where(_ => _.UserId == userId).ToList();
            return growth;
        }

        public Growth Update(int id, Growth growth)
        {
            var growthP = _context.Growth.Find(id);
            if (growthP != null)
            {
                growthP.Weight = (growth.Weight > 0) ? growth.Weight : growthP.Weight;
                growthP.Height = (growth.Height > 0) ? growth.Height : growthP.Height;
                growthP.Head = (growth.Head > 0) ? growth.Head : growthP.Head;
                growthP.Date = (growth.Date != null) ? growth.Date : growthP.Date;

                _context.SaveChanges();
                return this.Get(growthP.GrowthId);
            }
            else
                return null;
        }
    }
}
