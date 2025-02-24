using UnityEngine;

public class GameTrigger : MonoBehaviour
{

    public GameObject target1, target2, target3;
    private Animator anim1, anim2, anim3;

    // Start is called before the first frame update
    void Start()
    {
        anim1 = target1.GetComponent<Animator>();
        anim2 = target2.GetComponent<Animator>();
        anim3 = target3.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug only
        //if (anim1 != null)
        //{
        //    AnimatorStateInfo stateInfo = anim1.GetCurrentAnimatorStateInfo(0);
        //    Debug.Log("Current animation state: " + anim1.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        // Play target animations when player enters game trigger
        if (anim1 != null && anim2 != null && anim3 != null && other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Playing animation: Base Layer.ShootingTarget1");

            // Play animation 1
            anim1.Play("Base Layer.ShootingTarget1", 0, 0);
            // Set playback speed to 1.0
            anim1.SetFloat("PlaybackSpeed", 1.0f);

            // Play animation 2
            anim2.Play("Base Layer.ShootingTarget1", 0, 0);
            // Set playback speed to 1.0
            anim2.SetFloat("PlaybackSpeed", 1.0f);

            // Play animation 3
            anim3.Play("Base Layer.ShootingTarget1", 0, 0);
            // Set playback speed to 1.0
            anim3.SetFloat("PlaybackSpeed", 1.0f);
        }
        else
        {
            Debug.LogError("Animator is null, cannot play animation.");
        }
    }
}
