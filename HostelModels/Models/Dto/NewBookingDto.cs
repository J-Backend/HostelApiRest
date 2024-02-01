using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelModels.Models.Dto
{
    public class NewBookingDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int HotelId { get; set; }
        public int CustomerId { get; set; }
    }
}
