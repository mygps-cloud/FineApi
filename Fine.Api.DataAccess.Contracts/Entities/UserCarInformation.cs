using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.DataAccess.Contracts.Entities
{
    public class UserCarInformation
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string TechPassportId { get; set; }
        public int? CompanyId { get; set; }
    }
}
