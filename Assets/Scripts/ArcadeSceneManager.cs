using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public enum ArcadeScene
{
    Interior,
    Exterior,
    Baseball,
    Shooting,
}

public class ArcadeSceneManager : MonoBehaviour
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
                ArcadeScene.Exterior => "ArcadeSceneExterior",
                ArcadeScene.Baseball => "BaseballScene",
                ArcadeScene.Shooting => "ShootingScene",
                _ => throw new System.NotImplementedException(),
            }
        );
    }
}
