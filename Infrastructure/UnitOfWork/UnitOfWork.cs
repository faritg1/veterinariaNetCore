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
    private CiudadRepository _ciudades;

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

    public ICiudadRepository Ciudades{
        get{
            if(_ciudades == null){
                _ciudades = new CiudadRepository(_context);
            }
            return _ciudades;
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
