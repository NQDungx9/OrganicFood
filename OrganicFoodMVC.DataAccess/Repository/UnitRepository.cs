using OrganicFoodMVC.DataAccess.Data;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrganicFoodMVC.DataAccess.Repository
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public UnitRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Unit unit)
        {
            var objFromDb = _db.Units.FirstOrDefault(s => s.Id == unit.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = unit.Name;
            }
        }
    }
}
