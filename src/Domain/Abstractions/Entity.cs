using System.Text.Json.Serialization;

namespace Talapker.Common.Domain.Abstractions;

public abstract class Entity
{
    [JsonInclude] public Guid Id { get; set; }
}