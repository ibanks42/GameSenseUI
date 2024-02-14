using System.Text.Json;
using GameSenseApi.Internal;
using GameSenseApi.Utils;
using RestSharp;

namespace GameSenseApi;

public static class GameSenseClient
{
	/// <summary>
	///     The location of the coreProps.json file.
	///     Reference:
	///     <see
	///         href="https://github.com/SteelSeries/gamesense-sdk/blob/5b69e245833fe661ea0a30d459e2cbcccb3e81e7/doc/tutorials/audiovisualizer_tutorial.md#talking-to-gamesense" />
	/// </summary>
#if OS_WINDOWS
	private static string CorePropsFileLocation =>
		Environment.ExpandEnvironmentVariables(@"%ProgramData%\SteelSeries\SteelSeries Engine 3\coreProps.json");
#elif OS_MAC
	private static string CorePropsLocation => "/Library/Application Support/SteelSeries Engine 3/coreProps.json";
#else
#warning GameSense is only on Windows and macOS
	private static string CorePropsLocation => throw new PlatformNotSupportedException("GameSense is only supported on Windows and macOS.");
#endif

	/// <summary>
	///     The RestClient to use for sending requests.
	/// </summary>
	private static RestClient? _restClient;

	/// <summary>
	///     Registers a game with the GameSense server. This is required before sending events.
	/// </summary>
	/// <param name="name">
	///     The game name. This is the unique identifier for the game. It should be alphanumeric and have no spaces,
	///     underscores, or hyphens. It should be all uppercase and if not it will be converted to uppercase.
	/// </param>
	/// <param name="displayName">
	///     The game display name. This is the name that will be displayed in the SteelSeries Engine UI.
	/// </param>
	/// <param name="developer">
	///     The game developer. This is the name of the developer that will be displayed in the SteelSeries Engine UI.
	/// </param>
	/// <returns></returns>
	public static async Task<GameSenseGame?> RegisterGame(string name, string? displayName, string? developer)
	{
		// Ensure the rest client is initialized
		EnsureInitialized();

		// Create a new GameSenseGame object and send it to the server. This class contains the game name, display name, and developer as well as the methods to register events.
		var registerGame = new GameSenseGame(_restClient, name, displayName, developer);

		// Send the request to the server and return the GameSenseGame object if it was successful.
		var response = await _restClient.PostMessageAsync(Endpoints.RegisterGame, registerGame);

		// return null if registering the event failed
		return response is { IsSuccessful: false }
			? null
			: registerGame;
	}

	public static async Task<bool> UnregisterGame(string game)
	{
		// Ensure the rest client is initialized
		EnsureInitialized();

		// Create the request object. Reference: https://github.com/SteelSeries/gamesense-sdk/blob/83127ca35a108a3bab3fb3273e4e9c3c2a8ff9ac/doc/api/sending-game-events.md#registering-a-game
		var request = new { game };

		// Send the request to the server
		var response = await _restClient.PostMessageAsync(Endpoints.RemoveGame, request);

		// return true if the response is not null and request was successful
		return response is { IsSuccessful: true };
	}

	public static async Task<bool> UnregisterEvent(string game, string eventName)
	{
		// Ensure the rest client is initialized
		EnsureInitialized();

		// Create the request object.
		// Reference: https://github.com/SteelSeries/gamesense-sdk/blob/83127ca35a108a3bab3fb3273e4e9c3c2a8ff9ac/doc/api/writing-handlers-in-json.md#removing-an-event
		var request = new { game, @event = eventName };

		// Send the request to the server
		var response = await _restClient.PostMessageAsync(Endpoints.RemoveEvent, request);
		// return true if the response is not null and request was successful
		return response is { IsSuccessful: true };
	}

	private static void EnsureInitialized()
	{
		// If the rest client is already initialized, return
		if (_restClient != null) return;

		// Initialize the rest client with the address from the coreProps.json file
		var address = GetApiAddress();
		_restClient = new RestClient(address);
	}

	private static Uri GetApiAddress()
	{
		// Most likely SteelSeries Engine isn't running. Handle this differently?
		if (!File.Exists(CorePropsFileLocation))
		{
			throw new FileNotFoundException($"Could not find coreProps.json from {CorePropsFileLocation}");
		}

		// Read and deserialize core prop file
		var coreProp = JsonSerializer.Deserialize<CoreProp>(File.ReadAllText(CorePropsFileLocation));

		// coreProp or address is null, throw exception
		if (coreProp?.Address == null)
			throw new InvalidDataException(
				$"Failed to read data from coreProps.json file from {CorePropsFileLocation}.");

		// Failed to parse address, throw exception
		if (!Uri.TryCreate($"http://{coreProp.Address}", UriKind.RelativeOrAbsolute, out var address))
			throw new UriFormatException("The coreProp.json file contained an invalid address.");

		// Return the address
		return address;
	}
}