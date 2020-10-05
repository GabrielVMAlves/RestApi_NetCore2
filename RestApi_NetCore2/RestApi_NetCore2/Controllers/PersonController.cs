using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Services.Implementations;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

namespace RestApi_NetCore2.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private IPersonService _personService;

        public PersonController (IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindAll()
        {
            try
            {
                var teste = User.Identity;
                var profile = (User.Identity as ClaimsIdentity).Claims.Where(p => p.Type == "Profile").FirstOrDefault().Value;
                var retorno = _personService.FindAll();
                return Ok(retorno);
            } catch(Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
            
        }

        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [SwaggerResponse((201), Type = typeof(PersonVO))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
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
