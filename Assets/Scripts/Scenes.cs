using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum ArcadeScene
{
    Interior,
    InteriorV2,
    Exterior,
    Baseball,
    Shooting,
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
        Enum.TryParse(name, out scene);
        ChangeScene(scene);
    }

    public void ChangeScene(ArcadeScene scene)
    {
        SceneManager.LoadScene(
            scene switch
            {
                ArcadeScene.Interior => "ArcadeSceneInterior",
                ArcadeScene.InteriorV2 => "ArcadeSceneInteriorV2",
                ArcadeScene.Exterior => "ArcadeSceneExterior",
                ArcadeScene.Baseball => "BaseballScene",
                ArcadeScene.Shooting => "ShootingScene",
                _ => throw new System.NotImplementedException(),
            }
        );
    }

    public void DebugLog(string s)
    {
        Debug.Log(s);
    }
}
