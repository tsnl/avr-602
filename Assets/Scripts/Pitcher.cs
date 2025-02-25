using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Pitcher : MonoBehaviour
{
    private class Shot
    {
        public float delay = 1.0f;

        public GameObject SpawnBall(GameObject template, Vector3 position, Quaternion rotation, Vector3 forward, Vector3 up, float force, float pitch, float speedMultiplier)
        {
            var ball = Instantiate(template, position, rotation);
            Destroy(ball, 5.0f);
            var rb = ball.GetComponent<Rigidbody>();
            rb.AddForce((forward + up * pitch).normalized * force * speedMultiplier, ForceMode.Impulse);
            rb.angularVelocity = new Vector3(100.0f, 0.0f, 0.0f);
            return ball;
        }
    }


    public bool shotEnabled;
    public GameObject ballSpawnAnchor;
    public GameObject ballTemplate;
    public float shotDelaySec;
    public float shotForce;
    public float shotPitch;
    [SerializeField] private PlayableDirector playableDirector;
    public float difficultyMultiplierPerShot = 1.01F;


    public bool ShotEnabled { get => shotEnabled; set => shotEnabled = value; }

    private Shot currentShot = null;
    private GameObject lastBall = null;
    private float difficultyMultiplier = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        shotEnabled = false;

        // Play on Awake is enabled for animation to start, but pause soon after.
        Invoke(nameof(PauseAnimation), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!shotEnabled)
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
        }*/
    }

    public void NotifyScoreChanged(int score)
    {
        Debug.Log("Pitcher: NotifyScoreChanged: " + score);
        difficultyMultiplier = Mathf.Pow(difficultyMultiplierPerShot, score);
    }

    /// <summary>
    /// This function is called by signal receiver of Pitcher when the throw animation emits a signal in Timeline.
    /// </summary>
    public void Throw()
    {
        if (shotEnabled)
        {
            Shot newShot = new();
            newShot.SpawnBall(ballTemplate, ballSpawnAnchor.transform.position, ballSpawnAnchor.transform.rotation, ballSpawnAnchor.transform.forward, ballSpawnAnchor.transform.up, shotForce, shotPitch, difficultyMultiplier);
        }
    }

    /// <summary>
    /// Play pitcher animation (called by Bat XR Grab Interactable)
    /// </summary>
    public void PlayAnimation()
    {
        playableDirector.Play();
    }

    /// <summary>
    /// Pause pitcher animation
    /// </summary>
    public void PauseAnimation()
    {
        playableDirector.Pause();
    }
}
