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
        GameObject newBall = Instantiate(paintball);
        newBall.transform.position = spawnPoint.position;
        newBall.transform.rotation = spawnPoint.rotation;
    }
}
