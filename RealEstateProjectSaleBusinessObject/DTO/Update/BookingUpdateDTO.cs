﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Update
{
    public class BookingUpdateDTO
    {
        public IFormFile? BookingFile { get; set; }
        public IFormFile? RefundImage { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
        public Guid? CustomerID { get; set; }
        public Guid? StaffID { get; set; }
        public Guid? OpeningForSaleID { get; set; }
        public Guid? ProjectCategoryDetailID { get; set; }
        public Guid? DocumentTemplateID { get; set; }

    }
}
