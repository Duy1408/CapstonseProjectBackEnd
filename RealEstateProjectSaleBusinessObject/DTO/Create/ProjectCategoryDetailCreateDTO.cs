using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.DTO.Create
{
    public class ProjectCategoryDetailCreateDTO
    {
        public Guid ProjectID { get; set; }
        public Guid PropertyCategoryID { get; set; }
    }
}
