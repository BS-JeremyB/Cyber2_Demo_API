using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.BLL.Interfaces
{
    public interface IAuthService
    {
        public string GenerateToken(Utilisateur utilisateur);
    }
}
