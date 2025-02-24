using System;
using System.Collections;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.SceneManagement;
using Palmmedia.ReportGenerator.Core.Common;
using System.IO;
using UnityEngine.Events;

public class DifficultyData
{
  public int BaseballScoreThreshold { get; set; } = 3;
  public int ShootingScoreThreshold { get; set; } = 10;
}

public class SaveData
{
  public int BaseballHighScore { get; set; }
  public int ShootingHighScore { get; set; }
  public bool BaseballScoreThresholdReached { get; set; }
  public bool ShootingScoreThresholdReached { get; set; }

}

public class ProgressionManager : MonoBehaviour
{
  private DifficultyData difficulty = new DifficultyData();
  private SaveData saveData;

  public UnityEvent BaseballScoreThresholdReached;
  public UnityEvent BaseballHighScoreChanged;
  public UnityEvent ShootingScoreThresholdReached;
  public UnityEvent ShootingHighScoreChanged;

  // Start is called before the first frame update
  void Start()
  {
    Load();
  }

  public void RegisterBaseballScore(int score)
  {
    if (score >= difficulty.BaseballScoreThreshold)
    {
      BaseballScoreThresholdReached?.Invoke();
      saveData.BaseballScoreThresholdReached = true;
    }

    if (score > saveData.BaseballHighScore)
    {
      saveData.BaseballHighScore = score;
      BaseballHighScoreChanged?.Invoke();
    }

    Save();
  }

  public void RegisterShootingScore(int score)
  {
    if (score >= difficulty.ShootingScoreThreshold)
    {
      ShootingScoreThresholdReached?.Invoke();
      saveData.ShootingScoreThresholdReached = true;
    }

    if (score > saveData.ShootingHighScore)
    {
      saveData.ShootingHighScore = score;
      ShootingHighScoreChanged?.Invoke();
    }

    Save();
  }

  public void Reset()
  {
    saveData = new SaveData();
    Save();
  }

  private void Save()
  {
    var filepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/avr602/ArcadeSaveData.json";
    Directory.CreateDirectory(Path.GetDirectoryName(filepath));
    var text = JsonConvert.SerializeObject(this.saveData);
    File.WriteAllText($"${filepath}.new", text);
    File.Replace($"${filepath}.new", filepath, null);
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