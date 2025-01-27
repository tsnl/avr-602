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

        public GameObject SpawnBall(GameObject template, Vector3 position, Quaternion rotation, Vector3 forward, Vector3 up, float force, float pitch)
        {
            var ball = Instantiate(template, position, rotation);
            Destroy(ball, 5.0f);
            ball.GetComponent<Rigidbody>().AddForce((forward + up * pitch).normalized * force, ForceMode.Force);
            return ball;
        }
    }
    public bool shotEnabled;
    public GameObject ballSpawnAnchor;
    public GameObject ballTemplate;
    public float shotDelaySec;
    public float shotForce;
    public float shotPitch;


    public bool ShotEnabled { get => shotEnabled; set => shotEnabled = value; }

    private Shot currentShot = null;
    private GameObject lastBall = null;

    // Start is called before the first frame update
    void Start()
    {
        shotEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shotEnabled)
        {
            return;
        }
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
                    // Destroy(lastBall);
                }
                lastBall = currentShot.SpawnBall(ballTemplate, ballSpawnAnchor.transform.position, ballSpawnAnchor.transform.rotation, ballSpawnAnchor.transform.forward, ballSpawnAnchor.transform.up, shotForce, shotPitch);
                currentShot = null;
            }
        }
    }
}
