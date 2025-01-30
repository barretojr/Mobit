using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobit.Models.Modelos
{
    public class ClientePlano
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid PlanoId { get; set; }
        public Plano Plano { get; set; }
    }
}
