using BaseObjects.BaseObject;

public interface ITraderRepository
{
	List<Trader> GetAll();
	Trader GetByName(string traderName);
	Trader GetById(int id);
	bool Add(Trader trader);
	bool Delete(int id);
	bool Update(Trader trader);
}