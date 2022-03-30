﻿using DLL.DataContext;
using DLL.Repositories;
using System;
using System.Threading.Tasks;

namespace DLL.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        Task<int> SaveChangesAsync();
        Task<bool> SaveCompletedAsync();
    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

    
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private IDepartmentRepository _departmentRepository;
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;


        public IDepartmentRepository DepartmentRepository => 
            _departmentRepository ?? new DepartmentRepository(_context);

        public IStudentRepository StudentRepository =>
           _studentRepository ?? new StudentRepository(_context);

        public ICourseRepository CourseRepository =>
            _courseRepository ??= new CourseRepository(_context);

        public async Task<bool> ApplicationSaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }




        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        Task<int> IUnitOfWork.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveCompletedAsync()
        {
            throw new NotImplementedException();
        }
    }
}

