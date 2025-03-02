using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterSceneManager : MonoBehaviour
{
  public GameObject ProgressionManager;
  public Scenes Scenes;

  public void Start()
  {
    // Nothing to do here.
  }

  public void Update()
  {
    var pm = ProgressionManager.GetComponent<ProgressionManager>();
    if (string.IsNullOrEmpty(pm.LatestSaveData.CurrentScene))
    {
      // Default scene on startup:
      Scenes.ChangeScene(ArcadeScene.Exterior);
    }
    else
    {
      // Load the latest scene:
      Scenes.ChangeScene(pm.LatestSaveData.CurrentScene);
    }
  }
}