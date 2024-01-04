using RestSharp;

namespace PcbManager.Main.App.Report
{
    public class ReportGeneratorAdapter : IReportGeneratorAdapter
    {
        public async Task<Stream> GenerateReportAsync(Stream stream)
        {
            var options = new RestClientOptions("http://127.0.0.1:8000");
            var client = new RestClient(options);
            var request = new RestRequest("report/file/from-file", Method.Post);
            request.AddFile("file", () => stream, "test.jpg");

            var response = await client.DownloadStreamAsync(request);
            return response!;
        }
    }
}
