using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeSceneInteriorSceneManager : MonoBehaviour
{
    public GameObject progressionManager;
    public GameObject baseballTrophy;
    public GameObject shootingTrophy;
    public GameObject escapeGameObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBaseballTrophy()
    {
        baseballTrophy.SetActive(true);
    }

    public void OnShootingTrophy()
    {
        shootingTrophy.SetActive(true);
    }

    public void onBothTrophy()
    {
        escapeGameObject.SetActive(true);
    }
}
