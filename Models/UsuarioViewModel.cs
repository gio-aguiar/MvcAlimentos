using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMvc.Models
{
    public class UsuarioViewModel
    {
        public int Id {get; set; }
        public string Name {get; set; }
        public string PasswordString {get; set; }
        public byte[] Foto {get; set; }
        public string Perfil {get; set; }
        public string Email {get; set; }
        public string Token {get; set; }

        public DateTime? DataAcesso {get;set;}
        
        public string Username {get; set;} = string.Empty;}
        
    }
