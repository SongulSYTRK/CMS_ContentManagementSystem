using CMS.Domain.Repositories.Interface.EntityTypeRepositories;
using CMS.Domain.UnitofWork;
using CMS.Infrastructure.Context;
using CMS.Infrastructure.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        private IAppUserRepository _appUserRepository;
        public IAppUserRepository AppUserRepository
        {
            get
            {
                if (_appUserRepository ==null)
                {
                    _appUserRepository = new AppUserRepository(_db);
                }
                return _appUserRepository;
            }
        }

        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_db);

                }

                return _categoryRepository;
            }
        }

        private IPageRepository _pageRepository;
        public IPageRepository PageRepository
        {
            get
            {
                if (_pageRepository == null)
                {
                    _pageRepository = new PageRepository(_db);
                }
                return _pageRepository;
            }
        }
        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_db);
                }
                return _productRepository;
            }

        }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }


        private bool isDispose = false;
        public async ValueTask DisposeAsync()
        {
            if (!isDispose)
            {
                isDispose = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        private async Task DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }
        public async Task executeSqlRaw(string sql, params object[] parameters)
        {
            await _db.Database.ExecuteSqlRawAsync(sql, parameters);
        }

       
    }
}
