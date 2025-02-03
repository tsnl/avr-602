using UnityEngine;

public class PaintGun : MonoBehaviour {

    public Transform spawnPoint;
    public GameObject paintball;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward, out hit, Mathf.Infinity))
        {
            Destroy(hit.collider.gameObject);
        }

        GameObject newBall = Instantiate(paintball);
        newBall.transform.position = spawnPoint.position;
        newBall.transform.rotation = spawnPoint.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + new Vector3(0f, 0.2f, 0.15f), transform.forward);
    }
}
