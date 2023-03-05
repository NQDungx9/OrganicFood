using OrganicFoodMVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrganicFoodMVC.DataAccess.Repository.IRepository
{
    public interface IBrandRepositoty : IRepository<Brand>
    {
        void Update(Brand brand);
    }
}
