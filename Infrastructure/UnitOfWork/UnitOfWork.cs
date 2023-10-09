using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AnimalsContext _context;

    private PaisRepository _paises;
    private CiudadRepository _ciudades;
    private CitaRepository _citas;
    private ClienteDireccionRepository _clientesdirecciones;
    private ClienteRepository _clientes;
    private ClienteTelefonoRepository _clientestelefonos;
    private DepartamentoRepository _departamentos;
    private MascotaRepository _mascotas;
    private RazaRepository _razas;
    private ServicioRepository _servicios;

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
    public ICitaRepository Citas{
        get{
            if(_citas == null){
                _citas = new CitaRepository(_context);
            }
            return _citas;
        }
    }
    public IClienteDireccionRepository ClientesDirecciones{
        get{
            if(_clientesdirecciones == null){
                _clientesdirecciones = new ClienteDireccionRepository(_context);
            }
            return _clientesdirecciones;
        }
    }
    public IClienteRepository Clientes{
        get{
            if(_clientes == null){
                _clientes = new ClienteRepository(_context);
            }
            return _clientes;
        }
    }
    public IClienteTelefonoRepository ClientesTelefonos{
        get{
            if(_clientestelefonos == null){
                _clientestelefonos = new ClienteTelefonoRepository(_context);
            }
            return _clientestelefonos;
        }
    }
    public IDepartamentoRepository Departamentos{
        get{
            if(_departamentos == null){
                _departamentos = new DepartamentoRepository(_context);
            }
            return _departamentos;
        }
    }
    public IMascotaRepository Mascotas{
        get{
            if(_mascotas == null){
                _mascotas = new MascotaRepository(_context);
            }
            return _mascotas;
        }
    }
    public IRazaRepository Razas{
        get{
            if(_razas == null){
                _razas = new RazaRepository(_context);
            }
            return _razas;
        }
    }
    public IServicioRepository Servicios{
        get{
            if(_servicios == null){
                _servicios = new ServicioRepository(_context);
            }
            return _servicios;
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
