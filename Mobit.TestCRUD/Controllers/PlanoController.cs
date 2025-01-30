using Microsoft.AspNetCore.Mvc;
using Mobit.Data.Context;
using Mobit.Models.Modelos;
using System.Runtime.CompilerServices;

namespace Mobit.TestCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly AppDbContext _Context;

        public PlanoController(AppDbContext context)
        {
            _Context = context;
        }
        // /Plano/Listar
        [HttpGet]
        public IActionResult Listar()
        {
            var plano = _Context.Plano.ToList();
            return Ok(plano);
        }

        // /Plano/Listar/$Id
        [HttpGet("{id}")]
        public IActionResult Listar(Guid id)
        {
            var plano = _Context.Plano.SingleOrDefault(x => x.Id == id);
            if (plano == null) return NotFound();
            return Ok(plano);
        }

        // /Plano/CriarPlano
        [HttpPost]
        public IActionResult CriarPlano(Plano plano)
        {
            _Context.Plano.Add(plano);
            _Context.SaveChanges();
            return CreatedAtAction(nameof(Listar), new { id = plano.Id }, plano);
        }

        // /Plano/EditarPlano/$Id
        [HttpPut("{id}")]
        public IActionResult EditarPlano(Guid id, Plano form)
        {
            var plano = _Context.Plano.SingleOrDefault(x => x.Id == id);
            if (plano == null) return NotFound("Plano não encontrado.");

            plano.Nome = form.Nome;
            plano.Preco = form.Preco;
            plano.FranquiaDados = form.FranquiaDados;
            plano.MinutosLigacao = form.MinutosLigacao;

            _Context.SaveChanges();
            return NoContent();
        }

        // /Plano/ExcluirPlano/$Id
        [HttpDelete("{id}")]
        public IActionResult ExcluirPlano(Guid id)
        {
            var plano = _Context.Plano.SingleOrDefault(x => x.Id == id);
            if (plano == null) return NotFound("Plano não encontrado.");

            _Context.Plano.Remove(plano);
            _Context.SaveChanges();

            return NoContent();
        }

        
    }
}

