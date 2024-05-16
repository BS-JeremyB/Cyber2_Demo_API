using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.Domain.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Contenu { get; set; }
        public int Utilisateur_Id { get; set; }
    }
}
