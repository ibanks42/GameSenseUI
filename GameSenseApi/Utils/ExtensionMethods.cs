using RestSharp;

namespace GameSenseApi.Utils;

public static class ExtensionMethods
{
	/// <summary>
	///     Sends a message to the GameSense server.
	/// </summary>
	/// <param name="client">
	///     The RestClient to use for sending requests.
	/// </param>
	/// <param name="path">
	///     The path to send the message to.
	/// </param>
	/// <param name="body">
	///     The body of the message.
	/// </param>
	/// <typeparam name="T">
	///     The type of the body.
	/// </typeparam>
	/// <returns></returns>
	public static async Task<RestResponse?> PostMessageAsync<T>(this RestClient? client, string path, T body)
		where T : class
	{
		// If the client is null return.
		// TODO: Should we throw an exception here?
		if (client == null)
			return null;

		// Create a RestRequest and have RestSharp serialize it for us
		var request = new RestRequest(path, Method.Post);
		request.AddJsonBody(body);

		// Send to the API and return the response
		var response = await client.ExecutePostAsync(request);

		// Return the response
		return response;
	}
}