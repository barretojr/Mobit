using Microsoft.AspNetCore.Mvc;
using Mobit.Data.Context;

namespace Mobit.TestCRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var totalClientes = _context.Cliente.Count();
            var totalPlanos = _context.Plano.Count();
            var totalPlanosAssociados = _context.ClientePlano.Count();
            var mediaPlanosPorCliente = totalClientes > 0 ? (double)totalPlanosAssociados / totalClientes : 0;

            // Gráfico de Pizza: Proporção de clientes associados a cada plano
            var planosAssociados = _context.ClientePlano
                .GroupBy(cp => cp.Plano.Nome)
                .Select(g => new
                {
                    Plano = g.Key,
                    Quantidade = g.Count()
                }).ToList();

            // Gráfico de Barras: Número de clientes cadastrados por mês no último ano
            var dataAtual = DateTime.UtcNow;
            var dataInicio = dataAtual.AddYears(-1);

            var clientesPorMes = _context.Cliente
                .Where(c => c.DataCadastro >= dataInicio) 
                .GroupBy(c => new { c.DataCadastro.Year, c.DataCadastro.Month })
                .Select(g => new
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    Quantidade = g.Count()
                }).OrderBy(g => g.Ano).ThenBy(g => g.Mes)
                .ToList();

            var dados = new
            {
                TotalClientes = totalClientes,
                TotalPlanos = totalPlanos,
                MediaPlanosPorCliente = Math.Round(mediaPlanosPorCliente, 2),
                GraficoPizza = planosAssociados,
                GraficoBarras = clientesPorMes
            };

            return Ok(dados);
        }
    }
}


