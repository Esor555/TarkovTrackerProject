using BaseObjects.BaseObject;
using System;
using System.Collections.Generic;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.test;

namespace TarkovTrackerBLL.Service
{
    public class TraderService
    {
        private readonly ITraderRepository _traderRepository;

        public TraderService(string connectionString)
        {
            _traderRepository = new TraderRepository(connectionString);
        }

        public List<Trader> GetAll()
        {
            try
            {
                return _traderRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving traders", ex);
            }
        }

        public Trader GetByName(string traderName)
        {
            if (string.IsNullOrWhiteSpace(traderName))
                throw new ArgumentException("Trader name is required");

            try
            {
                return _traderRepository.GetByName(traderName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving trader with name {traderName}", ex);
            }
        }

        public Trader GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid trader ID");

            try
            {
                return _traderRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error retrieving trader with ID {id}", ex);
            }
        }

        public bool Add(Trader trader)
        {
            if (trader == null)
                throw new ArgumentNullException(nameof(trader));

            if (string.IsNullOrWhiteSpace(trader.Name))
                throw new ArgumentException("Trader name is required");

            try
            {
                return _traderRepository.Add(trader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding trader", ex);
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid trader ID");

            try
            {
                return _traderRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error deleting trader with ID {id}", ex);
            }
        }

        public bool Update(Trader trader)
        {
            if (trader == null)
                throw new ArgumentNullException(nameof(trader));

            if (trader.Id <= 0)
                throw new ArgumentException("Invalid trader ID");

            try
            {
                return _traderRepository.Update(trader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error updating trader", ex);
            }
        }
    }
}
