﻿using System;
using Microsoft.AspNetCore.Mvc;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Services.Implementations;

namespace RestApi_NetCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private IPersonService _personService;

        public PersonController (IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                var retorno = _personService.FindAll();
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult FindById(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid ID!");

                PersonVO retorno = _personService.FindById(id);

                if (retorno == null)
                    return NotFound("Person not found!");
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        [HttpPost]
        public IActionResult Create([FromBody] PersonVO person)
        {
            try
            {
                PersonVO retorno = _personService.Create(person);
                if (retorno == null)
                    return BadRequest("Invalid Person data");
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        [HttpPut]
        public IActionResult Update([FromBody] PersonVO person)
        {
            try
            {
                PersonVO retorno = _personService.Update(person);
                if (retorno == null)
                    return BadRequest("Invalid Person data");
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _personService.Delete(id);
                return NoContent();
            } catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}
