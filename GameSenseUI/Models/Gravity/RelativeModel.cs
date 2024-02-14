using System.Text.Json.Serialization;

namespace GameSenseUI.Models.Gravity;

public class RelativeModel
{
	[JsonPropertyName("days")] public int? Days { get; set; }

	[JsonPropertyName("hours")] public int? Hours { get; set; }

	[JsonPropertyName("minutes")] public int? Minutes { get; set; }
}