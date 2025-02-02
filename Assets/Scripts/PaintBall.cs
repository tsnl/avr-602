using UnityEngine;

public class PaintBall : MonoBehaviour {
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    //private void OnEnable()
    //{
    //    transform.position = spawnPoint.position;
    //    transform.rotation = spawnPoint.rotation;
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
