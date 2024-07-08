using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class CommentCreateDTO
    {
        [JsonIgnore]
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public DateTime CreateTime { get; set; }
        [JsonIgnore]
        public DateTime? UpdateTime { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }
        public Guid PropertyID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
