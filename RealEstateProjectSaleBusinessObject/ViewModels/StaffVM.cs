using System;
using System.Collections.Generic;
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
        public DateTime DateOfBirth { get; set; }
        public string Image { get; set; }
        public string Imagesignature { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string Placeoforigin { get; set; }
        public string PlaceOfresidence { get; set; }
        public DateTime DateRange { get; set; }
        public string Taxcode { get; set; }
        public string BankName { get; set; }
        public int BankNumber { get; set; }
        public bool Status { get; set; }
        public Guid AccountID { get; set; }
        public string Email { get; set; }
    }
}
