using UnityEngine;

public class PaintBall : MonoBehaviour {
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, 3f);
    }
}
