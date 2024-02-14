using System;
using System.Text.Json;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using GameSenseApi;
using GameSenseUI.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace GameSenseUI.Views;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private GameSenseGame? Game { get; set; }
	private GameSenseEvent? Event { get; set; }

	private async void Create_OnClick(object? _, RoutedEventArgs e)
	{
		// Remove the game and event if they already exist
		await GameSenseClient.UnregisterGame("PIHOLE");
		await GameSenseClient.UnregisterEvent("PIHOLE", "STATS");

		// Register the game and event
		Game = await GameSenseClient.RegisterGame("PIHOLE", "Pihole GS Stats", "Isaiah Banks");

		if (Game == null) return;

		// Register the event
		Event = await Game.BindEvent("STATS", 0, "title", "ads", "percentage");
		
		await MessageBoxManager.GetMessageBoxStandard("Success", "The event was created successfully.").ShowAsync();
	}

	private async void Send_OnClick(object? _, RoutedEventArgs e)
	{
		if (Event == null)
		{
			await MessageBoxManager
				.GetMessageBoxStandard("Error",
					"You have not created the event yet.",
					ButtonEnum.Ok,
					MsBox.Avalonia.Enums.Icon.Error).ShowAsync();
			return;
		}

		// Load the pihole data from the static json asset. In a real application, this would be loaded from the pihole server.
		var piholeData =
			JsonSerializer.Deserialize<PiholeResponseModel>(
				AssetLoader.Open(new Uri("avares://GameSenseUI/Assets/pihole.json")))!;

		// Send the event
		var response = await Event.SendEvent("-Pihole Today-",
			$"Blocked: {piholeData.AdsBlockedToday}",
			$"% blocked: {piholeData.AdsPercentageToday}");

		// TODO: Do something on the UI to show it worked?
		if (response)
		{
			await MessageBoxManager.GetMessageBoxStandard("Success", "The event was sent successfully.").ShowAsync();
		}
	}
}