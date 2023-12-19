using SecurityApi.Infrastructure.Data.Interfaces;
using SecurityApi.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Infrastructure.Data.Repositories
{
    public class MockSesionRepository : ISesionRepository
    {
        private List<SesionInfo> _sesions;
        public MockSesionRepository()
        {
            _sesions = new List<SesionInfo>();
        }

        public async Task<bool> Create(SesionInfo newLogin)
        {
            if (!_sesions.Contains(newLogin))
            {
                _sesions.Add(newLogin);
            }
            return true;
        }

        public async Task<SesionInfo?> GetByToken(string token)
        {
            return _sesions.Where(s => s.Token == token).FirstOrDefault();
        }
    }
}
