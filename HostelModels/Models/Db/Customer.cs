﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelModels.Models.Db
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Keyword { get; set; }
        public int TypeUser { get; set; }
    }
}
