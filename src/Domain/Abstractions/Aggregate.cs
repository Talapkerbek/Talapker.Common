using System.Text.Json.Serialization;

namespace Talapker.Common.Domain.Abstractions;

public abstract class Aggregate : Entity
{
    
    /// <summary>
    /// Version of aggregate. Must update after adding every successful event.
    /// </summary>
    public long Version { get; set; }
    
    // Uncommitted events
    [JsonIgnore] private readonly List<object> _uncommittedEvents = new List<object>();

    /// <summary>
    /// Gets list of uncommitted events to save them after.
    /// </summary>
    /// <returns>Uncommitted events</returns>
    public IEnumerable<object> GetUncommittedEvents()
    {
        return _uncommittedEvents;
    }

    /// <summary>
    /// Clearing uncommitted events list after saving.
    /// </summary>
    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    /// <summary>
    /// Adds event to uncommitted events list.
    /// </summary>
    /// <param name="event">Event to add to list.</param>
    protected void AddUncommittedEvent(object @event)
    {
        _uncommittedEvents.Add(@event);
    }

    protected void IncrementVersion()
    {
        Version++;
    }
}