using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Message { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
    public string? Source { get; set; }
    public string? TargetSite { get; set; }
    public string? Data { get; set; }
    public string? HelpLink { get; set; }
    public string? HResult { get; set; }
    public string? Date { get; set; }
}