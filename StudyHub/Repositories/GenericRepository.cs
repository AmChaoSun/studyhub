using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyHub.Models;
using StudyHub.Repositories.Interfaces;

namespace StudyHub.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StudyHubContext context;
        public GenericRepository(StudyHubContext context)
        {
            this.context = context;
        }

        public DbSet<T> Records
        {
            get
            {
                return context.Set<T>();
            }
        }

        public T Add(T record)
        {
            context.Add(record);
            context.SaveChanges();
            return record;

        }

        public void Delete(T record)
        {
            context.Remove(record);
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return Records;
        }

        public T GetById(int id)
        {
            return Records.Find(id);
        }

        public T Update(T record)
        {
            context.Attach(record);
            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();
            return record;
        }
    }
}
