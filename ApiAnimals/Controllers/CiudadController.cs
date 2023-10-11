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
public class CiudadController : BaseControlleerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CiudadDto>>> Get(){
        var ciudad = await _unitOfWork.Ciudades.GetAllAsync();
        return _mapper.Map<List<CiudadDto>>(ciudad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CiudadDto>> Get(int id){
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if(ciudad == null){
            return NotFound();
        }
        return _mapper.Map<CiudadDto>(ciudad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CiudadDto>> Post(CiudadDto ciudadDto){
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Add(ciudad);
        await _unitOfWork.SaveAsync();

        if(ciudad == null){
            return BadRequest();
        }
        ciudadDto.Id = ciudad.Id;
        return CreatedAtAction(nameof(Post), new{id = ciudadDto.Id}, ciudadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto ciudadDto){
        if(ciudadDto.Id == 0){
            ciudadDto.Id = id;
        }

        if(ciudadDto.Id != id){
            return BadRequest();
        }

        if(ciudadDto == null){
            return NotFound();
        }
        var ciudad = _mapper.Map<Ciudad>(ciudadDto);
        _unitOfWork.Ciudades.Update(ciudad);
        await _unitOfWork.SaveAsync();
        return ciudadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
        if(ciudad == null){
            return NotFound();
        }
        _unitOfWork.Ciudades.Remove(ciudad);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
