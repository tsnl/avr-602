using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DifficultyData
{
  public int BaseballScoreThreshold { get; set; } = 10;
  public int ShootingScoreThreshold { get; set; } = 10;
}

public class SaveData
{
  public int LatestBaseballScore { get; set; }
  public int LatestShootingScore { get; set; }
  public int BaseballHighScore { get; set; }
  public int ShootingHighScore { get; set; }
  public bool BaseballScoreThresholdReached { get; set; }
  public bool ShootingScoreThresholdReached { get; set; }

}

public class ProgressionManager : MonoBehaviour
{
  private DifficultyData difficulty = new DifficultyData();
  private SaveData saveData;
  private bool baseballHighScoreChangedSinceLoad = false;
  private bool shootingHighScoreChangedSinceLoad = false;
  private bool baseballTrophyChangedSinceLoad = false;
  private bool shootingTrophyChangedSinceLoad = false;



  public UnityEvent BaseballScoreThresholdReached;
  public UnityEvent BaseballHighScoreChanged;
  public UnityEvent ShootingScoreThresholdReached;
  public UnityEvent ShootingHighScoreChanged;
  public UnityEvent BothThresholdReached;

  // Start is called before the first frame update
  void Start()
  {
    Load();
    Save();
    PublishEvents();
  }

  public void RegisterBaseballScore(int score)
  {
    saveData.LatestBaseballScore = score;
    Save();
    PublishEvents();
  }

  public void RegisterShootingScore(int score)
  {
    saveData.LatestShootingScore = score;
    Save();
    PublishEvents();
  }

  public void Reset()
  {
    saveData = new SaveData();
    Save();
  }

  private void PublishEvents()
  {
    PublishBaseballEvents();
    PublishShootingEvents();
    PublishConjunctionEvents();
  }

  private void PublishBaseballEvents()
  {
    if (saveData.LatestBaseballScore >= difficulty.BaseballScoreThreshold && !baseballTrophyChangedSinceLoad)
    {
      baseballTrophyChangedSinceLoad = true;
      BaseballScoreThresholdReached?.Invoke();
      saveData.BaseballScoreThresholdReached = true;
    }

    if (saveData.LatestBaseballScore > saveData.BaseballHighScore && !baseballHighScoreChangedSinceLoad)
    {
      baseballHighScoreChangedSinceLoad = true;
      saveData.BaseballHighScore = saveData.LatestBaseballScore;
      BaseballHighScoreChanged?.Invoke();
    }
  }

  private void PublishShootingEvents()
  {
    if (saveData.LatestShootingScore >= difficulty.ShootingScoreThreshold && !shootingTrophyChangedSinceLoad)
    {
      shootingTrophyChangedSinceLoad = true;
      ShootingScoreThresholdReached?.Invoke();
      saveData.ShootingScoreThresholdReached = true;
    }

    if (saveData.LatestShootingScore > saveData.ShootingHighScore && !shootingHighScoreChangedSinceLoad)
    {
      shootingHighScoreChangedSinceLoad = true;
      saveData.ShootingHighScore = saveData.LatestShootingScore;
      ShootingHighScoreChanged?.Invoke();
    }
  }

  private void PublishConjunctionEvents()
  {
    if (saveData.BaseballScoreThresholdReached && saveData.ShootingScoreThresholdReached)
    {
      BothThresholdReached?.Invoke();
    }
  }

  private void Save()
  {
    var filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/avr602/ArcadeSaveData.json";
    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
    var text = JsonConvert.SerializeObject(this.saveData);
    File.WriteAllText($"{filepath}.new", text);
    File.Replace($"{filepath}.new", filepath, $"{filepath}.bak");
  }


  private void Load()
  {
    var filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/avr602/ArcadeSaveData.json";
    if (!File.Exists(filepath))
    {
      saveData = new SaveData();
    }
    else
    {
      var text = File.ReadAllText(filepath);
      this.saveData = JsonConvert.DeserializeObject<SaveData>(text);
    }
  }

}