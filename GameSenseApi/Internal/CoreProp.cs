using System.Text.Json.Serialization;

namespace GameSenseApi.Internal;

/// <summary>
///     Represents the coreProps.json file.
/// </summary>
internal class CoreProp
{
	/// <summary>
	///     The address of the GameSense server.
	/// </summary>
	[JsonPropertyName("address")] public string? Address { get; init; }

	// Not really sure what the next two are for. I assume EncryptedAddress is https? I couldn't find any documentation.
	[JsonPropertyName("encryptedAddress")] public string? EncryptedAddress { get; init; }

	[JsonPropertyName("ggEncryptedAddress")]
	public string? GgEncryptedAddress { get; init; }
}

// Example CoreProp File
// {
// "address": "127.0.0.1:50685",
// "encryptedAddress": "127.0.0.1:50686",
// "ggEncryptedAddress": "127.0.0.1:6327"
// }