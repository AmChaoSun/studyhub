using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudyHub.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        DbSet<T> Records { get; }
        T GetById(int id);
        T Add(T record);
        T Update(T record);
        void Delete(T record); 
    }
}
