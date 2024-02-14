using System.Text.Json.Serialization;

namespace GameSenseAPI.Models;

/// <summary>
///     Used to create a new event for the GameSense API. The structure can be seen in the official documentation.
///     <see
///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/json-handlers-screen.md#example-a-handler-to-display-custom-text" />
/// </summary>
/// <param name="hasText">
///     Whether or not the line has text.
/// </param>
/// <param name="lineName">
///     The name of the line. This will be visible in the SteelSeries Engine software.
/// </param>
public class EventLine(bool hasText, string lineName)
{
	/// <summary>
	///     Whether or not the line has text.
	/// </summary>
	[JsonPropertyName("has-text")] public bool HasText { get; set; } = hasText;

	/// <summary>
	///     The name of the line. This will be visible in the SteelSeries Engine software.
	/// </summary>
	[JsonPropertyName("context-frame-key")]
	public string ContextFrameKey { get; set; } = lineName;
}