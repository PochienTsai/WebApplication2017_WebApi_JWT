﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2017_WebApi_JWT.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}