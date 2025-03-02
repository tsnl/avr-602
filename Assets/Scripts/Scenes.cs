using System;
using System.Collections;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public enum ArcadeScene
{
    Router,
    // Interior,
    InteriorV2,
    Exterior,
    Baseball,
    Shooting,
    GameOver,
}

public class Scenes : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string name)
    {
        ArcadeScene scene;
        if (Enum.TryParse(name, out scene))
        {
            ChangeScene(scene);
        }
        else
        {
            // Try the original scene name
            SceneManager.LoadScene(name);
        }
    }

    public void ChangeScene(ArcadeScene scene)
    {
        SceneManager.LoadScene(
            scene switch
            {
                ArcadeScene.Router => "RouterScene",
                // ArcadeScene.Interior => "ArcadeSceneInterior",
                ArcadeScene.InteriorV2 => "ArcadeSceneInteriorV2",
                ArcadeScene.Exterior => "ArcadeSceneExterior",
                ArcadeScene.Baseball => "BaseballScene",
                ArcadeScene.Shooting => "ShootingScene",
                ArcadeScene.GameOver => "GameOver",
                _ => throw new System.NotImplementedException(),
            }
        );
    }

    public void DebugLog(string s)
    {
        Debug.Log(s);
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame called-- Application.Quit is ignored in the editor!");
        Application.Quit();
    }
}
