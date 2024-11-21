using System.Text.RegularExpressions;

namespace RealEstateProjectSale.Helpers
{
    public class CustomerIdExtraction
    {
        public string ExtractCustomerIdFromUrl(string url)
        {
            // Sử dụng biểu thức chính quy để trích xuất GUID
            string pattern = @"_([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})$";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(url);

            if (match.Success)
            {
                return match.Groups[1].Value;  // Lấy GUID phía sau dấu gạch dưới
            }
            else
            {
                throw new Exception("Không tìm thấy GUID trong URL");
            }

        }
    }
}
