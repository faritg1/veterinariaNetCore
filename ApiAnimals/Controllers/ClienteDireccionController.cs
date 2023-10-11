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
public class ClienteDireccionController : BaseControlleerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDireccionDto>>> Get(){
        var Cliente = await _unitOfWork.ClientesDirecciones.GetAllAsync();
        return _mapper.Map<List<ClienteDireccionDto>>(Cliente);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDireccionDto>> Get(int id){
        var cli = await _unitOfWork.ClientesDirecciones.GetByIdAsync(id);
        if(cli == null){
            return NotFound();
        }
        return _mapper.Map<ClienteDireccionDto>(cli);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteDireccionDto>> Post(ClienteDireccionDto clienteDireccionDto){
        var cli = _mapper.Map<ClienteDireccion>(clienteDireccionDto);
        _unitOfWork.ClientesDirecciones.Add(cli);
        await _unitOfWork.SaveAsync();

        if(cli == null){
            return BadRequest();
        }
        clienteDireccionDto.Id = cli.Id;
        return CreatedAtAction(nameof(Post), new{id = clienteDireccionDto.Id}, clienteDireccionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDireccionDto>> Put(int id, [FromBody] ClienteDireccionDto clienteDireccionDto){
        if(clienteDireccionDto.Id == 0){
            clienteDireccionDto.Id = id;
        }

        if(clienteDireccionDto.Id != id){
            return BadRequest();
        }

        if(clienteDireccionDto == null){
            return NotFound();
        }
        var cli = _mapper.Map<ClienteDireccion>(clienteDireccionDto);
        _unitOfWork.ClientesDirecciones.Update(cli);
        await _unitOfWork.SaveAsync();
        return clienteDireccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var cli = await _unitOfWork.ClientesDirecciones.GetByIdAsync(id);
        if(cli == null){
            return NotFound();
        }
        _unitOfWork.ClientesDirecciones.Remove(cli);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
