using entities_library.login;
using entities_library.post;

namespace entities_library.report;

public class Report {

    public long Id {get; set;}

    public required User User {get; set;}

    public ReportStatus ReportStatus {get; set;}

    public DateTime DateTime {get; set;}

    public required string Reason {get; set;}
}