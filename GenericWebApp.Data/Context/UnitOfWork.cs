using System;
using System.Threading.Tasks;

namespace GenericWebApp.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataDbContext _context;

        public UnitOfWork(DataDbContext context)
        {
            _context = context;
        }


        private PlayerRepository _playerRepository;

        // Repository Properties
        public PlayerRepository Players => _playerRepository ?? (_playerRepository = new PlayerRepository(_context));


        #region Supporting Methods

        // Update Method

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        // Transaction 
        public UnitOfWorkTransaction BeginTransaction()
        {
            return new UnitOfWorkTransaction(_context.Database.BeginTransaction());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #region Private Methods/Properties/Fields

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        #endregion

        #endregion
    }
}