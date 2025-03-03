using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public enum DifficultyLevel
{
  Default,
  Demo,
}

public class DifficultyData
{
  // public DifficultyLevel Level { get; set; } = DifficultyLevel.Default;
  public DifficultyLevel Level { get; set; } = DifficultyLevel.Demo;
  public int BaseballScoreThreshold => Level switch
  {
    DifficultyLevel.Default => 10,
    DifficultyLevel.Demo => 3,
    _ => 10,
  };
  public int ShootingScoreThreshold => Level switch
  {
    DifficultyLevel.Default => 30,
    DifficultyLevel.Demo => 10,
    _ => 10
  };
}

public class SaveData
{
  public string CurrentScene { get; set; }
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

  public bool CreateSaveDataIfMissing = true;
  public UnityEvent BaseballScoreThresholdReached;
  public UnityEvent BaseballHighScoreChanged;
  public UnityEvent ShootingScoreThresholdReached;
  public UnityEvent ShootingHighScoreChanged;
  public UnityEvent BothThresholdReached;

  public SaveData LatestSaveData => saveData;

  // Start is called before the first frame update
  void Start()
  {
    Load();

    if (CreateSaveDataIfMissing)
    {
      // Clobber default state with the current scene name.
      UpdateCurrentScene();
    }

    // Immediately save again in case we loaded the default object
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

  public void SaveGame()
  {
    UpdateCurrentScene();
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

  private void UpdateCurrentScene()
  {
    saveData.CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
  }

  private void Save()
  {
    var filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/avr602/ArcadeSaveData.json";
    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
    var text = JsonConvert.SerializeObject(this.saveData);
    File.WriteAllText($"{filepath}.new", text);
    if (File.Exists(filepath))
    {
      File.Replace($"{filepath}.new", filepath, $"{filepath}.bak");
    }
    else
    {
      File.Move($"{filepath}.new", filepath);
    }
  }


  private void Load()
  {
    var filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/avr602/ArcadeSaveData.json";
    try
    {
      var text = File.ReadAllText(filepath);
      saveData = JsonConvert.DeserializeObject<SaveData>(text);
    }
    catch (Exception e)
    {
      if (e is FileNotFoundException || e is DirectoryNotFoundException)
      {
        saveData = new SaveData();
      }
      else
      {
        throw;
      }
    }
  }
}
