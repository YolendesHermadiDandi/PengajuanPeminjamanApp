using API.DTOs.Employees;
using API.DTOs.Fasilities;
using Client.Contracts;
using Client.Repositories;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositries
{
    public class FasilityRepository : GeneralRepository<FasilityDto, Guid>, IFasilityRepository
    {

        public FasilityRepository(string request="Fasility/") : base(request) { }

    }
}
