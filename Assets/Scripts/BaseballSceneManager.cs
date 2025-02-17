using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballSceneManager : MonoBehaviour
{
    private BatState batState;

    public GameObject batGameObject;
    public GameObject batGazeInteractibleGameObject;
    public GameObject batGazeInteractibleMeshGameObject;

    // Start is called before the first frame update
    void Start()
    {
        batState = BatState.Spawn;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnBatGrabbed()
    {
        Debug.Log("Bat grabbed");
        batState = BatState.Grabbed;

        // Unconditionally disable the bat mesh renderer when bat is grabbed.
        batGazeInteractibleMeshGameObject.GetComponent<MeshRenderer>().enabled = false;

        // Ensure not enabled to hide the annoying reticle.
        batGazeInteractibleGameObject.GetComponent<TS.GazeInteraction.GazeInteractable>().Enable(false);
    }

    public void OnBatReleased()
    {
        Debug.Log("Bat released");
        batState = BatState.Spawn;

        // Ensure not enabled to hide the annoying reticle.
        batGazeInteractibleGameObject.GetComponent<TS.GazeInteraction.GazeInteractable>().Enable(true);
    }

    public void OnBatGazeEnter()
    {
        if (batState == BatState.Spawn)
        {
            Debug.Log("Bat gaze enter");
            batGazeInteractibleMeshGameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OnBatGazeLeave()
    {
        // Unconditionally disable the bat mesh renderer when gaze leaves or when bat is grabbed.
        batGazeInteractibleMeshGameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

enum BatState
{
    Spawn,
    Grabbed,
}