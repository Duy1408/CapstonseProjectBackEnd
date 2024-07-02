﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class OpeningForSale
    {
        public Guid OpeningForSaleID { get; set; }
        public string DescriptionName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime Deposittime { get; set; }
        public string Explain {  get; set; }
        public string MoneyType { get; set; }
        public double DepositMoney { get; set; }
        public double RevervationMoney { get; set;}
        public bool Status {  get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<OpenForSaleDetail>? openForSaleDetails { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }

    }
}
