using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AnimalsContext _context;

    private PaisRepository _paises;

    public UnitOfWork(AnimalsContext context)
    {
        _context = context;
    }

    public IPaisRepository Paises{
        get{
            if(_paises == null){
                _paises = new PaisRepository(_context);
            }
            return _paises;
        }

    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
