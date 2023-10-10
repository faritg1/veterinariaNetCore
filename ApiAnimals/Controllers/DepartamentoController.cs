using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiAnimals.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiAnimals.Controllers;
public class DepartamentoController : BaseControlleerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var Depar = await _unitOfWork.Departamentos.GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(Depar);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartamentoDto>> Get(int id){
        var Depar = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (Depar == null){
            return NotFound();
        }
        return _mapper.Map<DepartamentoDto>(Depar);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DepartamentoDto>> Post(DepartamentoDto departamentoDto){
        var Depar = _mapper.Map<Departamento>(departamentoDto);
        this._unitOfWork.Departamentos.Add(Depar);
        await _unitOfWork.SaveAsync();

        if(Depar == null){
            return BadRequest();
        }
        departamentoDto.Id = Depar.Id;
        return CreatedAtAction(nameof(Post), new {id = departamentoDto.Id}, departamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody]DepartamentoDto departamentoDto){
        if(departamentoDto.Id == 0){
            departamentoDto.Id = id;
        }

        if(departamentoDto.Id != id){
            return BadRequest();
        }

        if(departamentoDto == null){
            return NotFound();
        }
        var Depar = _mapper.Map<Departamento>(departamentoDto);
        _unitOfWork.Departamentos.Update(Depar);
        await _unitOfWork.SaveAsync();
        return departamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var Depar = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if(Depar == null){
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(Depar);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
