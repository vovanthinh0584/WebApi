using System;

namespace WebApplication.Models
{
    public class InputDeviceParameterDTO
    {
        public string AssetId
        { get; set; }
        public DateTime InputDate { get; set; }
        public string OperatingId
        { get; set; }
        public string UMID { get; set; }
        public decimal Value
        { get; set; }
        public string Note

        { get; set; }

        public Guid RecordID

        { get; set; }

        public string BUID

        { get; set; }
        public string Lang

        { get; set; }
        public string UserId

        { get; set; }

        

    }
}
