using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenericWebApp.Data.Context;
using GenericWebApp.Domain;

namespace GenericWebApp.Business
{
    public interface IGameService
    {
        IList<Player> GetAllPlayers();
    }

    public class GameService : IGameService
    {
        private IUnitOfWork _unitOfWork;
        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Player> GetAllPlayers()
        {
            return _unitOfWork.Players.GetAll().ToList();
            // return new List<Player>() {new Player() {PlayerId = 1, Name = "Player One"}};
        }
    }
}
