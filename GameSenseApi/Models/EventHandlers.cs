using System.Text.Json.Serialization;

namespace GameSenseAPI.Models;

/// <summary>
///     EventHandlers class is used to create a new event for the GameSense API. The structure can be seen in the official
///     documentation.
///     <see
///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/json-handlers-screen.md#full-schema-definition" />
/// </summary>
/// <param name="lines"></param>
public class EventHandlers(IEnumerable<EventLine> lines)
{
  /// <summary>
  ///     Represents the device type that will be used to display the event. Currently only 'screened' is supported. Support
  ///     for other device types may or may not be added in the future.
  /// </summary>
  [JsonPropertyName("device-type")] public string DeviceType => "screened";

  /// <summary>
  ///     Represents the mode of the device that will be used to display the event. Currently only 'screen' is supported.
  ///     Support for other modes may or may not be added in the future.
  /// </summary>
  [JsonPropertyName("mode")] public string Mode => "screen";

  /// <summary>
  ///     Currently only one zone is supported. This is a string that represents which screen zone to show events on. Support
  ///     for other zones may or may not be added in the future.
  /// </summary>
  [JsonPropertyName("zone")] public string Zone => "one";

  /// <summary>
  ///     This is the datas array. Documentation for this can be found in the official documentation.
  ///     <see
  ///         href="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/json-handlers-screen.md#describing-the-screen-notification" />
  /// </summary>
  [JsonPropertyName("datas")] public IEnumerable<DataArray> DataArray { get; } = new[] { new DataArray(lines) };
}