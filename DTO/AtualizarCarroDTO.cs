using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_Gerenciamento.DTO
{
    public class AtualizarCarroDTO
    {
        public string? NovaMarca { get; set; }
        public string? NovoModelo { get; set; }
        public int NovoAno { get; set; }
    }
}