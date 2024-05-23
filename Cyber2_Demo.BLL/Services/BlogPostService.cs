using Cyber2_Demo.BLL.Interfaces;
using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.BLL.Services
{
    public class BlogPostService : IBlogPostService
    {

        private readonly IBlogPostRepository _blogRepository;

        public BlogPostService(IBlogPostRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public BlogPost? Create(BlogPost blogPost)
        {
            return _blogRepository.Create(blogPost);
        }

        public bool Delete(int id)
        {
            BlogPost? post = _blogRepository.GetById(id);

            if(post is not null)
            {
                return _blogRepository.Delete(post);
            }
            return false;
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return _blogRepository.GetAll();
        }

        public BlogPost? GetById(int id)
        {
            return _blogRepository.GetById(id);
        }

        public BlogPost? Update(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
