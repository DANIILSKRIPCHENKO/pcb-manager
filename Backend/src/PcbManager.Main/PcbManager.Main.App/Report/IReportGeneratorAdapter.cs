namespace PcbManager.Main.App.Report
{
    public interface IReportGeneratorAdapter
    {
        public Task<Stream> GenerateReportAsync(Stream stream);
    }
}
