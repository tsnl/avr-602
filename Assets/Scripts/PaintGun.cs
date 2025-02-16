using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PaintGun : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject paintball;
    [SerializeField] private GameObject icecube;
    [SerializeField] private GameObject splash;
    [SerializeField] private List<AudioClip> audioClips = new();
    [SerializeField] private ScoreBoard scoreBoard;

    private static int score = 0;
    private bool altShoot;

    // Start is called before the first frame update
    void Start()
    {
        altShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot()
    {

        if (!altShoot) // regular shoot
        {
            Debug.Log("RegularShoot");

            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward, out hit, Mathf.Infinity))
            {
                // Display paint splash
                PaintSplash newSplash = Instantiate(splash).GetComponent<PaintSplash>();
                GameObject hitParent = hit.collider.gameObject;
                newSplash.SetParent(hitParent, hit.point - hitParent.transform.position);
                newSplash.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);

                // Play splash sound
                if (audioClips.Count > 0 && audioClips[0] != null) AudioSource.PlayClipAtPoint(audioClips[0], hit.point);

                // 
                if (hit.collider.gameObject.name.Contains("target"))
                {
                    score++;
                    scoreBoard.Set(score.ToString());
                }
            }

            GameObject newBall = Instantiate(paintball);
            newBall.transform.position = spawnPoint.position;
            newBall.transform.rotation = spawnPoint.rotation;
        }
        else // AltShoot
        {
            Debug.Log("AltShoot");

            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward, out hit, Mathf.Infinity))
            {
                // Distroy target
                if (hit.collider.gameObject.tag == "Destroyable")
                {
                    DestroyImmediate(hit.collider.gameObject, true);
                }

                // Play ice sound
                if (audioClips.Count > 1 && audioClips[1] != null) AudioSource.PlayClipAtPoint(audioClips[1], hit.point);
            }

            GameObject newBall = Instantiate(icecube);
            newBall.transform.position = spawnPoint.position;
            newBall.transform.rotation = spawnPoint.rotation;

        }


    }

    public void PrimaryTriggerPressed()
    {
        altShoot = !altShoot;
    }

    public void DebugPrimaryButton()
    {
        Debug.Log("Primary button pressed");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward);
    }
}
