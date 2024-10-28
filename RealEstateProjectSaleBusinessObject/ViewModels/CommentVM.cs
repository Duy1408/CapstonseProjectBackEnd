using RealEstateProjectSaleBusinessObject.JsonConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class CommentVM
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
        public bool Status { get; set; }
        public Guid PropertyID { get; set; }
        public string PropertyName { get; set; }
        public Guid CustomerID { get; set; }
        public string PersonalEmail { get; set; }
    }
}
