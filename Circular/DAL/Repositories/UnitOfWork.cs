﻿using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private bool _disposed = false;

        public IUserRepository Users { get; }
        public IProjectRepository Projects { get; }


        public UnitOfWork(
            ApplicationDbContext dbContext,
            IUserRepository userRepository,
            IProjectRepository projectRepository
            )
        {
            _dbContext = dbContext;
            Users = userRepository;
            Projects = projectRepository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}