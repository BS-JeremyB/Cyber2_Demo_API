using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.DAL.Interfaces
{
    public interface IBlogPostRepository
    {
        public BlogPost? Create(BlogPost blogPost);
        public BlogPost? Update(BlogPost blogPost);
        public bool Delete(BlogPost blogPost);
        public BlogPost? GetById(int id);
        public IEnumerable<BlogPost> GetAll();
    }
}
