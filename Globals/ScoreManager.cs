using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ScoreManager : Node
{
	private const string SCORE_FILE = "user://COOKING_SCORES.dat";

	private int _score = 0;

	private int _upgrades = 0;

	private GameScore _savedScore = new();

	public GameScore SavedScore => _savedScore;

	public static int Score => Instance._score;

	public static int Upgrades => Instance._upgrades;

	public static ScoreManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
		LoadScoreAndUpgrades();
		ConnectSignals();
	}

	private void ConnectSignals()
	{
		SignalManager.Instance.OnScoreUpdated += OnScoreUpdated;
		SignalManager.Instance.OnUpgradeUpdated += OnUpgradeUpdated;
    }

    public void UpdateScore(int points)
	{
		_score += points;
	}

	public void UpdateUpgrade()
	{
		_upgrades += 1;
	}

	public static void ResetScore()
	{
		Instance._score = 0;
	}

	public static void ResetUpgrades()
	{
		Instance._upgrades = 0;
	}

	private void SaveScore()
	{
		var gameScore = new GameScore
		{
			DateSaved = DateTime.Now,
			Score = _score,
			Upgrades = _upgrades
		};

		using var file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Write);

		if (file != null)
		{
			var jsonStr = JsonConvert.SerializeObject(gameScore);
			file.StoreString(jsonStr);
		}
	}

	private void LoadScoreAndUpgrades()
	{
		using var file = FileAccess.Open(SCORE_FILE, FileAccess.ModeFlags.Read);
		if (file != null)
		{
			var jsonData = file.GetAsText();
			if (!string.IsNullOrEmpty(jsonData))
			{
				_savedScore = JsonConvert.DeserializeObject<GameScore>(jsonData) ?? new();
			}
		}
	}

	private void OnUpgradeUpdated()
	{
		UpdateUpgrade();
	}
	
    private void OnScoreUpdated(int points)
    {
		UpdateScore(points);
    }
}
