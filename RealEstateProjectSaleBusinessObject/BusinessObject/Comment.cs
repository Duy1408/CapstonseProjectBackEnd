using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.BusinessObject
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool Status { get; set; }
        public Guid PropertyID { get; set; }
        public Property? Property { get; set; }
        public Guid CustomerID { get; set; }
        public Customer? Customer { get; set; }
    }
}
