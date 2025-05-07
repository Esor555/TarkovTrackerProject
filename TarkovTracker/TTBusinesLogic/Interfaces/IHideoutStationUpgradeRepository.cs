using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTBusinesLogic.BusinesLogic;

namespace TTBusinesLogic.Interfaces
{
	public interface IHideoutStationUpgradeRepository
	{
		List<HideoutStationUpgrade> GetAll();
		HideoutStationUpgrade GetByStationIdAndItemId(int stationId, int itemId);
		bool Add(HideoutStationUpgrade upgrade);
		bool Delete(int stationId, int itemId);
		bool Update(HideoutStationUpgrade upgrade);
	}
}
