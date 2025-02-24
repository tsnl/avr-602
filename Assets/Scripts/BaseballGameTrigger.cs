using UnityEngine;
using UnityEngine.Playables;

public class BaseballGameTrigger : MonoBehaviour
{

    //public Animator TargetAnimation;
    public GameObject target;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = target.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            //Debug.Log("Current animation state: " + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Play target animations when player enters game trigger
        if (anim != null && other.gameObject.CompareTag("Player"))
        {
            //Console.Log("Baseball Game Trigger triggered with: " + other.gameObject.name);
            //Debug.Log(other.name);

            // Ensure animation is playing:
            anim.Play("Base Layer.Target1", 0, 0);

            // Set playback speed to 1.0
            anim.SetFloat("PlaybackSpeed", 1.0f);
        }
        else
        {
            Debug.LogError("Animator is null, cannot play animation.");
        }
    }
}
