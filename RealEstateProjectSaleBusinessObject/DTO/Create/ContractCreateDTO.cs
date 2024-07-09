﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ContractCreateDTO
    {
        [JsonIgnore]
        public Guid ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractType { get; set; }
        [JsonIgnore]
        public DateTime CreatedTime { get; set; }
        [JsonIgnore]
        public DateTime? UpdatedTime { get; set; }
        [JsonIgnore]
        public DateTime? DateSigned { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public double TotalPrice { get; set; }
        public string? Description { get; set; }
        public byte[]? ContractFile { get; set; }
        public string Status { get; set; }
        public Guid BookingID { get; set; }
        public Guid PaymentProcessID { get; set; }
    }
}
