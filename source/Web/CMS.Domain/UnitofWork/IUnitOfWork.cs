
using CMS.Domain.Repositories.Interface.EntityTypeRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Domain.UnitofWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {

        // I made singleton becase I dont want to instance in different area. I will make totaly process. Unit of work desing pattern provide transcastion area.
        IAppUserRepository AppUserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPageRepository PageRepository { get; }
        IProductRepository ProductRepository { get; }

        Task Commit();  // We will savechange for all crud method. It is have more advantage(especially memory)


        Task executeSqlRaw(string sql, params object[] parameters);
        
    }
}
