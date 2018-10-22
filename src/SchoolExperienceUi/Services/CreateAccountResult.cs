namespace SchoolExperienceUi.Services
{
    public class CreateAccountResult
    {
        public string ClientId { get; internal set; }
        public string ClientToken { get; internal set; }
        public string JwtToken { get; internal set; }
    }
}