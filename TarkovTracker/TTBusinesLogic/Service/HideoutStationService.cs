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
    public class HideoutStationService
    {
        private readonly IhideoutstationRepository _hideoutStationRepository;

        public HideoutStationService(IhideoutstationRepository hideoutStationRepository)
        {
            _hideoutStationRepository = hideoutStationRepository ?? throw new ArgumentNullException(nameof(hideoutStationRepository));
        }

        public HideoutStationService(string connectionString)
        {
            _hideoutStationRepository = new HideoutStationRepository(connectionString);
        }

        public List<HideoutStation> GetAllStations()
        {
            return _hideoutStationRepository.GetAll();
        }

        public HideoutStation GetStationById(int id)
        {
            return _hideoutStationRepository.GetById(id);
        }

        public bool AddStation(HideoutStation station)
        {
            var validator = new TarkovTrackerBLL.Validators.HideoutStationValidator();
            var validationResult = validator.Validate(station);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join("; ", validationResult.Errors));
            return _hideoutStationRepository.Add(station);
        }

        public bool UpdateStation(HideoutStation station)
        {
            var validator = new TarkovTrackerBLL.Validators.HideoutStationValidator();
            var validationResult = validator.Validate(station);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join("; ", validationResult.Errors));
            return _hideoutStationRepository.Update(station);
        }

        public bool DeleteStation(int id)
        {
            return _hideoutStationRepository.Delete(id);
        }

        public List<HideoutStation> GetStationsByName(string name)
        {
            var allStations = GetAllStations();
            return allStations.Where(s => s.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
