using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.DAL;
using TTBusinesLogic.Interfaces;

namespace TTBusinesLogic.BusinesLogic
{
    public class HideoutStationService
    {
        private readonly IhideoutstationRepository _repository;

        public HideoutStationService(string connectionString)
        {
            _repository = new HideoutStationRepository(connectionString);
        }

        public List<HideoutStation> GetAllStations() => _repository.GetAll();

        public HideoutStation GetStationById(int id) => _repository.GetById(id);

        public HideoutStation GetStationByName(string name) => _repository.GetByName(name);

        public bool AddStation(HideoutStation station)
        {
            if (string.IsNullOrWhiteSpace(station.Name))
                return false;

            return _repository.Add(station);
        }

        public bool UpdateStation(HideoutStation station) => _repository.Update(station);

        public bool DeleteStation(int id) => _repository.Delete(id);
    }
}
