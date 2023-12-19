using SecurityApi.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Data.Interfaces
{
    public interface ISesionRepository
    {
        Task<bool> Create(SesionInfo newLogin);

        Task<SesionInfo> GetByToken(string token);
    }
}
