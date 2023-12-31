﻿using Boutique.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boutique.Entities.Concrate
{
    public class Visits : IEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Query { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string TimeZone { get; set; }
        public string Isp { get; set; }


        public string Org { get; set; }
        public string As { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string UserId { get; set; }
        public  User User { get; set; }
        public DateTime _Date { get; set; }
    }
}
