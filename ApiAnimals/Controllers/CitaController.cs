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
public class CitaController : BaseControlleerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CitaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CitaDto>>> Get(){
        var cita = await _unitOfWork.Citas.GetAllAsync();
        return _mapper.Map<List<CitaDto>>(cita);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CitaDto>> Get(int id){
        var cita = await _unitOfWork.Citas.GetByIdAsync(id);
        if(cita == null){
            return NotFound();
        }
        return _mapper.Map<CitaDto>(cita);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CitaDto>> Post(CitaDto citaDto){
        var cita = _mapper.Map<Cita>(citaDto);
        _unitOfWork.Citas.Add(cita);
        await _unitOfWork.SaveAsync();

        if(cita == null){
            return BadRequest();
        }
        citaDto.Id = cita.Id;
        return CreatedAtAction(nameof(Post), new{id = citaDto.Id}, citaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CitaDto>> Put(int id, [FromBody] CitaDto citaDto){
        if(citaDto.Id == 0){
            citaDto.Id = id;
        }

        if(citaDto.Id != id){
            return BadRequest();
        }

        if(citaDto == null){
            return NotFound();
        }
        var cita = _mapper.Map<Cita>(citaDto);
        _unitOfWork.Citas.Update(cita);
        await _unitOfWork.SaveAsync();
        return citaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var citas = await _unitOfWork.Citas.GetByIdAsync(id);
        if(citas == null){
            return NotFound();
        }
        _unitOfWork.Citas.Remove(citas);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
