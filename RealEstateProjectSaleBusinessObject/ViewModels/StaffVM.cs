using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleBusinessObject.ViewModels
{
    public class StaffVM
    {
        public Guid StaffID { get; set; }
        public string Name { get; set; }
        public string PersonalEmail { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public string? Image { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string Placeoforigin { get; set; }
        public string PlaceOfresidence { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfIssue { get; set; }
        public string? Taxcode { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public bool Status { get; set; }
        public Guid AccountID { get; set; }
        public string Email { get; set; }
    }
}
