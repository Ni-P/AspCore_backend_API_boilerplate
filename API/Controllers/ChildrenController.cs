using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Children;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ChildrenController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<ChildDTO>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ChildDTO>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query {Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        // Add [Authorize(Policy = "IsOwnerCheck")] here for authorization 
        public async Task<ActionResult<Unit>> Edit(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        // Add [Authorize(Policy = "IsOwnerCheck")] here for authorization 
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new Delete.Command {Id = id});
        }
    }
}