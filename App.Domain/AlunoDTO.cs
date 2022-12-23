using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Domain {
    public class AlunoDTO {
        public int id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(50, ErrorMessage = "Nome tem no mínimo 2 caracteres e no máximo 50 caracteres.", MinimumLength = 2)]
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
//        [RegularExpression(@"[0-9]{4}\-[0-9]{2}\-[0-9]{2})",ErrorMessage = "Tipo de data Invalida YYYY-MM-DD.")]
        public string nascimento { get; set; }
        [Required(ErrorMessage = "O RA é obrigatório!")]
        [Range(1, 9099, ErrorMessage = "O intervalo para cadastro de RA está entre 1 e 9099.")]
        public int? ra { get; set; }
    }
}