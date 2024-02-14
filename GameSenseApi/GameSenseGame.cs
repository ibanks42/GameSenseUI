using System.Text.Json;
using System.Text.Json.Serialization;
using GameSenseApi.Internal;
using GameSenseApi.Utils;
using RestSharp;

namespace GameSenseApi;

/// <summary>
///     Represents a game that is registered with the GameSense server.
///     Reference:
///     <see
///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/sending-game-events.md#registering-a-game" />
/// </summary>
public class GameSenseGame
{
	/// <summary>
	///     The RestClient to use for sending requests.
	/// </summary>
	private readonly RestClient? _restClient;

	/// <param name="restClient">
	///     The RestClient to use for sending requests.
	/// </param>
	/// <param name="game">
	///     The game name. This is the unique identifier for the game.
	/// </param>
	/// <param name="displayName">
	///     The game display name. This is the name that will be displayed in the SteelSeries Engine UI.
	/// </param>
	/// <param name="developer">
	///     The game developer. This is the name of the developer that will be displayed in the SteelSeries Engine UI.
	/// </param>
	internal GameSenseGame(RestClient? restClient, string game, string? displayName, string? developer)
	{
		_restClient = restClient;
		Game = game;
		DisplayName = displayName;
		Developer = developer;
	}

	/// <summary>
	///     The game name. This is the unique identifier for the game.
	/// </summary>
	[JsonPropertyName("game")] public string Game { get; set; }

	/// <summary>
	///     The game display name. This is the name that will be displayed in the SteelSeries Engine UI.
	/// </summary>
	[JsonPropertyName("game_display_name")]
	public string? DisplayName { get; set; }

	/// <summary>
	///     The game developer. This is the name of the developer that will be displayed in the SteelSeries Engine UI.
	/// </summary>
	[JsonPropertyName("developer")] public string? Developer { get; set; }

	/// <summary>
	///     Registers an event for the game.
	/// </summary>
	/// <param name="eventName">
	///     The event name. This is the unique identifier for the event.
	/// </param>
	/// <param name="iconCode">
	///     The icon code to use for the event. <br /><br />
	///     See here for available icon codes:
	///     <see href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/event-icons.md" />
	/// </param>
	/// <param name="lines">
	///     The lines to display in the event. <br /><br />
	///     These are the titles of the data that will be displayed in the SteelSeries Engine UI.
	///     Max 3 lines.
	/// </param>
	/// <returns>
	///     The GameSenseEvent that was registered, or null if the registration failed.
	/// </returns>
	public async Task<GameSenseEvent?> BindEvent(string eventName, int iconCode, params string[] lines)
	{
		var regEvent = new GameSenseEvent(this, _restClient, eventName.ToUpper(), iconCode, lines);

		var response = await _restClient.PostMessageAsync(Endpoints.BindEvent, regEvent);

		return response is { IsSuccessful: true }
			? regEvent
			: null;
	}
}