using E3.ReactorManager.ReportsManager.Model.Data;
using System.Collections.Generic;

namespace E3.ReactorManager.ReportsManager.Model.Interfaces
{
    public interface IReportPrinter
    {
        event ShowReportPreviewEventHandler ShowReportPreviewEvent;
        event ClearReportPreviewEventHandler ClearReportPreviewEvent;
        event ReportGenerationInProgressEventHandler ReportGenerationInProgressEvent;

        void ClearReportPreview();

        void PrintReportSections(string reportHeader, IList<ReportSection> sections, string reportLogoPath = null);
    }

    public delegate void ShowReportPreviewEventHandler(string reportFilePath);
    public delegate void ClearReportPreviewEventHandler();
    public delegate void ReportGenerationInProgressEventHandler();

    /// <summary>
    /// Additional Data to be printed on the Report
    /// </summary>
    public class LabelValuePair
    {
        public LabelValuePair(string label, string value)
        {
            Label = label; Value = value;
        }

        public string Label { get; set; }

        public string Value { get; set; }
    }
}
