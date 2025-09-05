using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Talapker.Common.Domain.Abstractions;

public abstract record BaseCommand
{
    [JsonIgnore]
    [BindNever]
    public string? InvokedUserId  { get; set; }
}