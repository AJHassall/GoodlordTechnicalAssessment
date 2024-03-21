namespace Goodlord_TechnicalAssessment_AdamHassall.Services.CSVProcessors
{
    public interface ICSVProcessor<T> where T : class
    {
        IEnumerable<T> ProcessCSV(string filePath);
    }
}
