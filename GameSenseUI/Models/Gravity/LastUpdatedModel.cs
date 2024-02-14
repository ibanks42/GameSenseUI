using System.Text.Json.Serialization;

namespace GameSenseUI.Models.Gravity;

public class LastUpdatedModel
{
	[JsonPropertyName("file_exists")] public bool? FileExists { get; set; }

	[JsonPropertyName("absolute")] public int? Absolute { get; set; }

	[JsonPropertyName("relative")] public RelativeModel? Relative { get; set; }
}