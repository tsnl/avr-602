using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeSceneExteriorSceneManager : MonoBehaviour
{
    public GameObject progressionManager;

    // Start is called before the first frame update
    void Start()
    {
        progressionManager.GetComponent<ProgressionManager>().Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
