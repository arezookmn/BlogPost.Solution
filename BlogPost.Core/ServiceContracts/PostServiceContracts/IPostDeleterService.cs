using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.ServiceContracts.PostServicesInterface
{
    public interface IPostDeleterService
    {
        Task SoftDeletePostAsync(Guid postId);
        Task DeletePostAsync(Guid postId);
    }
}
