using System;
using Microsoft.AspNetCore.Mvc;
using RestApi_NetCore2.Data.VO;
using RestApi_NetCore2.Services;
using Tapioca.HATEOAS;

namespace RestApi_NetCore2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindAll()
        {
            try
            {
                var retorno = _bookService.FindAll();
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindById(long id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid ID!");

                BookVO retorno = _bookService.FindById(id);

                if (retorno == null)
                    return NotFound("Person not found!");
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Create([FromBody] BookVO book)
        {
            try
            {
                BookVO retorno = _bookService.Create(book);
                if (retorno == null)
                    return BadRequest("Invalid Person data");
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }

        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Update([FromBody] BookVO book)
        {
            try
            {
                BookVO retorno = _bookService.Update(book);
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
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(long id)
        {
            try
            {
                _bookService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}
