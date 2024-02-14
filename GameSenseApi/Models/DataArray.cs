using System.Text.Json.Serialization;

namespace GameSenseAPI.Models;

/// <summary>
///     Represents an array of event lines.
/// </summary>
/// <param name="lines">
///     The lines to include in the array.
/// </param>
public class DataArray(IEnumerable<EventLine> lines)
{
	/// <summary>
	///     The lines included in the event.
	/// </summary>
	[JsonPropertyName("lines")] public IEnumerable<EventLine> Lines { get; } = lines;
}