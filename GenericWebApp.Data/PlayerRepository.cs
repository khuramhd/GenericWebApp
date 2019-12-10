using System;
using GenericWebApp.Data.Context;
using GenericWebApp.Domain;

namespace GenericWebApp.Data
{
    public class PlayerRepository : GenericRepository<Player>
    {
        public PlayerRepository(DataDbContext context) : base(context)
        {
        }
    }
}
