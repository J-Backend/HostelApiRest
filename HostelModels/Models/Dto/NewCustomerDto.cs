using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelModels.Models.Dto
{
    public class NewCustomerDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Keyword { get; set; }
        public int TypeUser { get; set; }
    }
}
