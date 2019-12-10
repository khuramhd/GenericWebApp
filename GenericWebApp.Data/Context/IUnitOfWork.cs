using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GenericWebApp.Data.Context
{
    public interface IUnitOfWork : IDisposable
    {
        PlayerRepository Players { get; }

        void SaveChanges();
        Task<bool> SaveChangesAsync();
        UnitOfWorkTransaction BeginTransaction();
    }
}
