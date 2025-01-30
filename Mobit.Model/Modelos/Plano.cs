using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobit.Models.Modelos
{
    public class Plano
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int FranquiaDados { get; set; }
        public int MinutosLigacao { get; set; }

        public ICollection<ClientePlano> ClientePlanos { get; set; } = new List<ClientePlano>();

    }
}
