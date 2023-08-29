using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Core.ServiceContracts.PostServicesInterface
{
    public interface IArticleDeleterService
    {
        Task SoftDeleteArticleAsync(Guid articleId);
        Task DeleteArticleAsync(Guid articleId);
    }
}
