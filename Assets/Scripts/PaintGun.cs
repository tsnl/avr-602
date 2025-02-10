using UnityEngine;

public class PaintGun : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject paintball;
    public GameObject icecube;
    public GameObject splash;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        Debug.Log("RegularShoot");

        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward, out hit, Mathf.Infinity))
        {
            // Display paint splash
            GameObject newSplash = Instantiate(splash);
            newSplash.transform.position = hit.transform.position;
            //newSplash.transform.rotation = hit.transform.rotation;
            Destroy(newSplash, 0.6f);
        }

        GameObject newBall = Instantiate(paintball);
        newBall.transform.position = spawnPoint.position;
        newBall.transform.rotation = spawnPoint.rotation;

    }
    public void AltShoot()
    {
        Debug.Log("AltShoot");

        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward, out hit, Mathf.Infinity))
        {
            // Distroy target
            DestroyImmediate(hit.collider.gameObject, true);
        }

        GameObject newBall = Instantiate(icecube);
        newBall.transform.position = spawnPoint.position;
        newBall.transform.rotation = spawnPoint.rotation;
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
