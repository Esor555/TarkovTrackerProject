using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseObjects.BaseObject;
using TarkovTrackerBLL.Service;
using TarkovTrackerDAL.Interfaces;
using TarkovTrackerDAL.test;

namespace TarkovTrackerBLL.Service
{
    public class TraderService
    {
        private readonly ITraderRepository _traderRepository;

        public TraderService(ITraderRepository traderRepository)
        {
            _traderRepository = traderRepository ?? throw new ArgumentNullException(nameof(traderRepository));
        }

        public TraderService(string connectionString)
        {
            _traderRepository = new TraderRepository(connectionString);
        }

        public List<Trader> GetAllTraders()
        {
            return _traderRepository.GetAll();
        }

        public Trader GetTraderById(int id)
        {
            return _traderRepository.GetById(id);
        }

        public bool AddTrader(Trader trader)
        {
            return _traderRepository.Add(trader);
        }

        public bool UpdateTrader(Trader trader)
        {
            return _traderRepository.Update(trader);
        }

        public bool DeleteTrader(int id)
        {
            return _traderRepository.Delete(id);
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
                throw new Exception($"Error retrieving trader with name {traderName}", ex);
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
                throw new Exception("Error adding trader", ex);
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
                throw new Exception($"Error deleting trader with ID {id}", ex);
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
                throw new Exception("Error updating trader", ex);
            }
        }
    }
}
