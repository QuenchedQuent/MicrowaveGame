using Godot;
using Newtonsoft.Json;
using System;

public partial class ScoreManager : Node
{
	private const string SCORE_FILE = "user://COOKING_SCORES.dat";

	private int _score = 0;

	private int _upgrades = 0;

	private int _baseIncome = 1;

	private GameScore _savedScore = new();

	public GameScore SavedScore => _savedScore;

	public static int Score => Instance._score;

	public static int Upgrades => Instance._upgrades;

	public static int BaseIncome => Instance._baseIncome;

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
		SignalManager.Instance.OnSaveScore += OnSaveScore;
    }

    public void UpdateScore(int points)
	{
		_score += points;
		SignalManager.EmitOnScoreUpdateHUD();
	}

	public void UpdateUpgrade()
	{
		_upgrades += 1;
	}

	public static void ResetScoreAndUpgrades()
	{
		Instance._score = 0;
		Instance._upgrades = 0;
		SignalManager.EmitOnScoreUpdateHUD();
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

		if (_savedScore != null)
        {
			_score = SavedScore.Score;
			_upgrades = SavedScore.Upgrades;
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

	private void OnSaveScore()
	{
		SaveScore();
	}
}
