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
        public string DecisionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReservationPrice { get; set; }
        public string? Description { get; set; }
        public DateTime CheckinDate { get; set; }
        public bool Status { get; set; }
        public List<Booking>? Bookings { get; set; }
        public List<Notification>? Notifications { get; set; }

        public List<OpenForSaleDetail>? OpenForSaleDetails { get; set; }
        public Guid ProjectID { get; set; }
        public Project? Project { get; set; }

    }
}
