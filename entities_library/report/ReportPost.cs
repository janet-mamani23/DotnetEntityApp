using entities_library.post;

namespace entities_library.report;

public class ReportPost : Report
{
    public required Post ReportedPost {get; set;}
}