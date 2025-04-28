using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
    
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int idCliente { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public int id { get; set; }
    
    }
}