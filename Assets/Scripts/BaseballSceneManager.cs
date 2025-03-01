using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballSceneManager : MonoBehaviour
{
    private BatState batState;
    private Material batNormalMaterial;

    public GameObject batGameObject;
    public GameObject batMeshGameObject;
    public Material batHighlightMaterial;
    public GameObject batGazeInteractibleGameObject;
    public GameObject scoreThresholdReachedAudioSourceGameObject;

    // Start is called before the first frame update
    void Start()
    {
        batState = BatState.Spawned;

        if (batNormalMaterial != null && batMeshGameObject != null)
        {
            batNormalMaterial = batMeshGameObject.GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBatGrabbed()
    {
        Debug.Log("Bat grabbed");
        batState = BatState.Grabbed;

        // Ensure not enabled to hide the annoying reticle.
        batGazeInteractibleGameObject.GetComponent<TS.GazeInteraction.GazeInteractable>().Enable(false);
    }

    public void OnBatReleased()
    {
        Debug.Log("Bat released");
        batState = BatState.Spawned;

        // Ensure not enabled to hide the annoying reticle.
        batGazeInteractibleGameObject.GetComponent<TS.GazeInteraction.GazeInteractable>().Enable(true);
    }

    public void OnBatGazeEnter()
    {
        if (batState == BatState.Spawned)
        {
            Debug.Log("Bat gaze enter");
            batGameObject.GetComponent<MeshRenderer>().material = batHighlightMaterial;
        }
    }

    public void OnBatGazeLeave()
    {
        // Unconditionally disable the bat highlight when the bat is not gazed.
        batGameObject.GetComponent<MeshRenderer>().material = batNormalMaterial;
    }
}

enum BatState
{
    Spawned,
    Grabbed,
}