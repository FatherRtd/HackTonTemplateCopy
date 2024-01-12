namespace HackTonTemplate.Models
{
    public class AuthenticationData
    {
        public AuthenticationData() {}

        public AuthenticationData(bool isSuccessful, string message = "")
        {
            IsSuccessful = isSuccessful; 
            Message = message;
        }

        public DateTime ExpirationTime { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}
