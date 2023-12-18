using SecurityApi.Core.Security.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Interfaces
{
    public interface IJwtFactoryService
    {
        Token GenerateToken(User user);
    }
}
