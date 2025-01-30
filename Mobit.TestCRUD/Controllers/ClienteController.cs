using Microsoft.AspNetCore.Mvc;
using Mobit.Data.Context;
using Mobit.Models.Modelos;
using System.Runtime.CompilerServices;

namespace Mobit.TestCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public ClienteController(AppDbContext context)
        {
            _Context = context;
        }
        // /Cliente/Listar
        [HttpGet]
        public IActionResult Listar()
        {
            var clientes = _Context.Cliente.ToList();
            return Ok(clientes);
        }

        // /Cliente/Listar/$Id
        [HttpGet("{id}")]
        public IActionResult Listar(Guid id)
        {
            var cliente = _Context.Cliente.SingleOrDefault(x => x.Id == id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        // /Cliente/CriarCliente
        [HttpPost]
        public IActionResult CriarCliente(Cliente cliente)
        {
            _Context.Cliente.Add(cliente);
            _Context.SaveChanges();
            return CreatedAtAction(nameof(Listar), new { id = cliente.Id }, cliente);
        }

        // /Cliente/EditarCliente/$Id
        [HttpPut("{id}")]
        public IActionResult EditarCliente(Guid id, Cliente form)
        {
            var cliente = _Context.Cliente.SingleOrDefault(x => x.Id == id);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            cliente.Nome = form.Nome;
            cliente.CPF = form.CPF;
            cliente.Email = form.Email;
            cliente.Telefone = form.Telefone;

            _Context.SaveChanges();
            return NoContent();
        }

        // /Cliente/ExcluirCliente/$Id
        [HttpDelete("{id}")]
        public IActionResult ExcluirCliente(Guid id)
        {
            var cliente = _Context.Cliente.SingleOrDefault(x => x.Id == id);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            _Context.Cliente.Remove(cliente);
            _Context.SaveChanges();

            return NoContent();
        }

        // /Cliente/AssociarPlano/$ClienteId&$PlanoId
        [HttpPost]
        public IActionResult AssociarPlano(Guid clienteId, Guid planoId)
        {
            var cliente = _Context.Cliente.SingleOrDefault(x => x.Id == clienteId);
            if (cliente == null) return NotFound("Cliente não encontrado.");

            var plano = _Context.Plano.SingleOrDefault(y => y.Id == planoId);
            if (plano == null) return NotFound("Plano não encontrado.");

            var clientePlano = new ClientePlano
            {
                Id = Guid.NewGuid(),
                ClienteId = clienteId,
                PlanoId = planoId
            };

            _Context.ClientePlano.Add(clientePlano);
            _Context.SaveChanges();

            return NoContent();
        }
    }
}

