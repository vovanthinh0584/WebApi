using System;

namespace WebApplication.Models.RequestBody.GetTask
{
    public class QueryGetTaskSummary
    {
        public string WorkNo { get; set; }
        public DateTime WorkDate { get; set; }
        public string RequestOrPlanNo { get; set; }
        public string WorkType { get; set; }
        public string Status { get; set; }
        public string Level { get; set; }
        public string RequestedContent { get; set; }
        public string MTNRequestNum { get; set; }
        public DateTime MTNRequestDateTime { get; set; }
        public string Equipment { get; set; }
        public string DescriptionInformation { get; set; }
    }
}
