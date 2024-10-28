namespace RealEstateProjectSale.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertToDateTime(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
