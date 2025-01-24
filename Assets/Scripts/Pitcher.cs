using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pitcher : MonoBehaviour
{
    private class Shot
    {
        public float delay = 1.0f;

        public GameObject SpawnBall(GameObject template, Vector3 position, Quaternion rotation, Vector3 forward)
        {
            var ball = Instantiate(template, position, rotation);
            ball.GetComponent<Rigidbody>().AddForce(forward * 1e3f, ForceMode.Force);
            return ball;
        }
    }

    public GameObject ballSpawnAnchor;
    public GameObject ballTemplate;
    public float shotDelaySec;

    private Shot currentShot = null;
    private GameObject lastBall = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentShot == null)
        {
            currentShot = new Shot() { delay = shotDelaySec };
        }
        else
        {
            currentShot.delay = Mathf.Max(0.0f, currentShot.delay - Time.deltaTime);
            if (currentShot.delay == 0.0f)
            {
                if (lastBall != null)
                {
                    Destroy(lastBall);
                }
                lastBall = currentShot.SpawnBall(ballTemplate, ballSpawnAnchor.transform.position, ballSpawnAnchor.transform.rotation, ballSpawnAnchor.transform.forward);
                currentShot = null;
            }
        }
    }
}
