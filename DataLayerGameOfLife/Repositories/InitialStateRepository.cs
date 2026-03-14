using DataLayerGameOfLife.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayerGameOfLife.Repositories;

internal sealed class InitialStateRepository : IInitialStateRepository
{
    private readonly GameOfLifeContext ctx;

    public InitialStateRepository() : this(new GameOfLifeContext()) { }

    public InitialStateRepository(GameOfLifeContext context)
    {
        ctx = context;
        ctx.Database.EnsureCreated();
    }

    public IEnumerable<InitialState> GetAll()
        => ctx.InitialStates.AsNoTracking().OrderBy(s => s.Name).ToList();

    public InitialState? Get(int id)
        => ctx.InitialStates.AsNoTracking().FirstOrDefault(s => s.Id == id);

    public InitialState? Get(string name)
        => ctx.InitialStates.AsNoTracking().FirstOrDefault(s => s.Name == name);

    public void Add(InitialState initialState)
    {
        ctx.InitialStates.Add(initialState);
        ctx.SaveChanges();
    }

    public void Update(InitialState initialState)
    {
        ctx.InitialStates.Update(initialState);
        ctx.SaveChanges();
    }

    public void Delete(InitialState initialState)
    {
        ctx.InitialStates.Remove(initialState);
        ctx.SaveChanges();
    }
}
