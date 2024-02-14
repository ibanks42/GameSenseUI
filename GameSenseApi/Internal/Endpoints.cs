namespace GameSenseApi.Internal;

internal static class Endpoints
{
	/// <summary>
	///     Address to register a game with the GameSense server.
	///     <see
	///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/sending-game-events.md#registering-a-game" />
	/// </summary>
	public const string RegisterGame = "game_metadata";

	/// <summary>
	///     Address to remove a game from the GameSense server.
	///     <see
	///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/writing-handlers-in-json.md#removing-a-game" />
	/// </summary>
	public const string RemoveGame = "remove_game";

	/// <summary>
	///     Address to bind an event to a game.
	///     <see
	///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/writing-handlers-in-json.md#binding-an-event" />
	/// </summary>
	public const string BindEvent = "bind_game_event";

	/// <summary>
	///     Address to remove an event from a game.
	///     <see
	///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/writing-handlers-in-json.md#removing-an-event" />
	/// </summary>
	public const string RemoveEvent = "remove_game_event";

	/// <summary>
	///     Address to send an event to the GameSense server.
	///     <see href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/sending-game-events.md#game-events" />
	/// </summary>
	public const string SendEvent = "game_event";
}