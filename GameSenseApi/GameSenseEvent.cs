using System.Dynamic;
using System.Text.Json.Serialization;
using GameSenseApi.Internal;
using GameSenseAPI.Models;
using GameSenseApi.Utils;
using RestSharp;

namespace GameSenseApi;

/// <summary>
///     Represents an event that is registered with the GameSense server.
///     Reference: <see href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/sending-game-events.md" />
///     and <seealso href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/json-handlers-screen.md" />
/// </summary>
public class GameSenseEvent
{
	/// <summary>
	///     Rest client to send data to the GameSense server.
	/// </summary>
	private readonly RestClient? _restClient;

	/// <summary>
	///     Create a new GameSenseEvent
	/// </summary>
	/// <param name="gameSenseGame">
	///     The GameSenseGame that this event is associated with
	/// </param>
	/// <param name="client">
	///     Rest client to send data to the GameSense server.
	/// </param>
	/// <param name="eventName">
	///     Name of the event.
	/// </param>
	/// <param name="iconId">
	///     Icon ID of the event.
	///     See here for list of available icons
	///     <see href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/event-icons.md" />
	/// </param>
	/// <param name="lines">
	///     The name of the lines to register with the GameSense API.
	/// </param>
	internal GameSenseEvent(GameSenseGame gameSenseGame, RestClient? client, string eventName, int iconId,
		string[] lines)
	{
		GameSenseGame = gameSenseGame;
		_restClient = client;
		EventName = eventName;
		IconId = iconId;
		Handlers = new[] { new EventHandlers(lines.Select(line => new EventLine(true, line))) };
	}

	/// <summary>
	///     The GameSenseGame that this event is associated with
	/// </summary>
	[JsonIgnore] public GameSenseGame GameSenseGame { get; }

	/// <summary>
	///     Name of the GameSenseGame.
	/// </summary>
	[JsonPropertyName("game")] public string GameName => GameSenseGame.Game;

	/// <summary>
	///     Name of the event.
	/// </summary>
	[JsonPropertyName("event")] public string EventName { get; init; }

	/// <summary>
	///     Icon ID of the event.
	///     See here for list of available icons
	///     <see href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/event-icons.md" />
	/// </summary>
	[JsonPropertyName("icon_id")] public int IconId { get; init; }

	/// <summary>
	/// </summary>
	[JsonPropertyName("handlers")] public IEnumerable<EventHandlers> Handlers { get; }

	/// <summary>
	///     Send an event to the GameSense server.
	/// </summary>
	/// <param name="dataLines">
	///     The data to send to the GameSense server. <br /><br />
	///     This method will only send the data that matches the lines that were registered with the GameSense API.
	///     Max length of dataLines is 3 or the number of lines registered with the GameSense API.
	/// </param>
	/// <returns>
	///     True if the event was sent successfully, false otherwise.
	/// </returns>
	public async Task<bool> SendEvent(params string[] dataLines)
	{
		var lineNames = Handlers
			.First()
			.DataArray
			.First()
			.Lines
			.Select(line => line.ContextFrameKey)
			.ToArray();

		// Create the dynamic object to send to the GameSense server
		// This is a way to create an object with dynamic properties akin to JavaScript objects.
		// TODO: Find a better way to do this? It's a bit hacky.
		dynamic expando = new ExpandoObject();
		for (var i = 0; i < Math.Min(dataLines.Length, lineNames.Length); i++)
		{
			((IDictionary<string, object>)expando).Add(lineNames[i], dataLines[i]);
		}

		// Create the event request
		// Reference: https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/sending-game-events.md#event-context-data
		var eventRequest = new
		{
			game = GameSenseGame.Game,
			@event = EventName,
			data = new
			{
				frame = expando
			}
		};

		var response = await _restClient.PostMessageAsync(Endpoints.SendEvent, eventRequest);

		return response is { IsSuccessful: true };
	}
}