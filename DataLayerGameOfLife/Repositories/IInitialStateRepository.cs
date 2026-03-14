using DataLayerGameOfLife.Models;

namespace DataLayerGameOfLife.Repositories;

public interface IInitialStateRepository
{
    IEnumerable<InitialState> GetAll();
    InitialState? Get(int id);
    InitialState? Get(string name);
    void Add(InitialState initialState);
    void Update(InitialState initialState);
    void Delete(InitialState initialState);
}
