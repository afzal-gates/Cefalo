using Cefalo.Application.Commands;
using Cefalo.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cefalo.API.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var query= new GetEmployeeListQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cmd = new DeleteEmployeeCommand() { Id = id };
            await _mediator.Send(cmd);
            return NoContent();
        }
    }
}
