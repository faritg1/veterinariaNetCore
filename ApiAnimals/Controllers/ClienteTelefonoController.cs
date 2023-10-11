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
public class ClienteTelefonoController : BaseControlleerApi
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClienteTelefonoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteTelefonoDto>>> Get(){
        var cli = await _unitOfWork.ClientesTelefonos.GetAllAsync();
        return _mapper.Map<List<ClienteTelefonoDto>>(cli);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteTelefonoDto>> Get(int id){
        var cli = await _unitOfWork.ClientesTelefonos.GetByIdAsync(id);
        if(cli == null){
            return NotFound();
        }
        return _mapper.Map<ClienteTelefonoDto>(cli);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClienteTelefonoDto>> Post(ClienteTelefonoDto clienteTelefonoDto){
        var cli = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
        _unitOfWork.ClientesTelefonos.Add(cli);
        await _unitOfWork.SaveAsync();

        if(cli == null){
            return BadRequest();
        }
        clienteTelefonoDto.Id = cli.Id;
        return CreatedAtAction(nameof(Post), new{id = clienteTelefonoDto.Id}, clienteTelefonoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteTelefonoDto>> Put(int id, [FromBody] ClienteTelefonoDto clienteTelefonoDto){
        if(clienteTelefonoDto.Id == 0){
            clienteTelefonoDto.Id = id;
        }

        if(clienteTelefonoDto.Id != id){
            return BadRequest();
        }

        if(clienteTelefonoDto == null){
            return NotFound();
        }
        var cli = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
        _unitOfWork.ClientesTelefonos.Update(cli);
        await _unitOfWork.SaveAsync();
        return clienteTelefonoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var cli = await _unitOfWork.ClientesTelefonos.GetByIdAsync(id);
        if(cli == null){
            return NotFound();
        }
        _unitOfWork.ClientesTelefonos.Remove(cli);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
