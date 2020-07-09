using System;
using Microsoft.AspNetCore.Mvc;
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

        // GET api/values
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

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult FindById(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid ID!");

                Person retorno = _personService.FindById(id);

                if (retorno == null)
                    return NotFound("Person not found!");
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Person person)
        {
            try
            {
                Person retorno = _personService.Create(person);
                if (retorno == null)
                    return BadRequest("Invalid Person data");
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Person person)
        {
            try
            {
                Person retorno = _personService.Update(person);
                if (retorno == null)
                    return BadRequest("Invalid Person data");
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        // DELETE api/values/5
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
