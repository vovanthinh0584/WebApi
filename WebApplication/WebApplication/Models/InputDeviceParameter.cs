using Microsoft.VisualBasic;
using System;

namespace WebApplication.Models
{
    public class InputDeviceParameterDTO
    {
        public string AssetId { get; set; }
        public DateTime InputDate { get; set; }
        public string OperatingId { get; set; }
        public string UMID { get; set; }
        public decimal Value { get; set; }
        public string Note { get; set; }

        public Guid RecordID { get; set; }

        public Int64 Id { get; set; }
        public string BUID { get; set; }
        public string Lang { get; set; }
        public string UserId { get; set; }
        public string Zone { get; set; }
        public string Device { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public Int64 Time { get; set; }
        public string ChecklistID { get; set; }
        public string StandardValue { get; set; }
        public bool Confirm { get; set; }
        public bool NonConfirm { get; set; }

    }
}
