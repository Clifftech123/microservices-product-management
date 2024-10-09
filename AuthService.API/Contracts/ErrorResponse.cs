namespace AuthService.API.Contracts
{

    public record ErrorResponse
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; internal set; }
    }
}
