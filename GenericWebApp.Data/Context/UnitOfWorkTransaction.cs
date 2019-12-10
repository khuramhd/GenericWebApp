using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace GenericWebApp.Data.Context
{
    public class UnitOfWorkTransaction : IDisposable
    {
        private readonly IDbContextTransaction _transaction;
        public UnitOfWorkTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}