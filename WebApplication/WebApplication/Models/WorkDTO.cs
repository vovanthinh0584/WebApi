using System;

namespace WebApplication.Models
{
    public class WorkDTO
    {
        public string AssetId { get; set; }
        public string WorkerId { get; set; }
        public string AreaId { get; set; }
        public string ShiftId { get; set; }
        public DateTime WorkDate { get; set; }
        public string WorkNo { get; set; }

        public string BUID { get; set; }

    }
}
