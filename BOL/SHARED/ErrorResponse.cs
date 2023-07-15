namespace BOL.SHARED
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string RedirectToUrl { get; set; } = "/";
    }
}
