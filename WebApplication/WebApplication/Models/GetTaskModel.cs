namespace WebApplication.Models
{
    public class GetTaskModel
    {
        public string AssetId { get; set; }
        public string DetailErrorId { get; set; }
        public string DetailErrorName { get; set; }
        public string WorkNo { get; set; }
        public string RequestOrPlanNo { get; set; }
        public string WorkType { get; set; }
        public string Level { get; set; }
        public string Status { get; set; }
        public string RequestedContent { get; set; }
        public string Reason { get; set; }
    }
}
