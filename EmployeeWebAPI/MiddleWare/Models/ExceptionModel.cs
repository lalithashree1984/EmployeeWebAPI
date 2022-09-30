namespace EmployeeWebAPI.MiddleWare.Models
{
    public class ExceptionModel
    {
        public string HttpMethod { get; set; } 
        public string QueryStringValue { get; set; }
        public string RequestBody { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }

        public string InnerException { get; set; }
        public string InnerExceptionStackTrace { get; set; }


    }
}
