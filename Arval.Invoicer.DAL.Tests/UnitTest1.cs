namespace Arval.Invoicer.DAL.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var configuration = new ZaptecConfiguration
            {
                BaseAddress = "https://api.zaptec.com/",
                TokenUrl = "oauth/token",
                Username = "sanderrgooss@gmail.com",
                Password = "tQRQ?NqK?X9j8jNC"
            };
            var connector = new ZaptecApiConnector(new HttpClient
            {
               BaseAddress = new Uri(configuration.BaseAddress) 
            }, configuration);

            var result = await connector.GetChargeHistoryAsync();
        }
    }
}