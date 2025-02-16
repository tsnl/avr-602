using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class target : MonoBehaviour
{
    [SerializeField] ScoreBoard scoreBoard;

    private int score = 0;
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private Material material;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        originalColor = material.color;
    }

    // Clean up
    private void OnDestroy()
    {
        Destroy(material);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Contains("Ball"))
        {
            Debug.Log("ball hits target");

            // Play audio
            audioSource.Play();

            // Increment score
            score++;
            scoreBoard.Set(score.ToString());

            // Change target material color
            material.color = Color.cyan;
            Invoke(nameof(ResetMaterial), 1f);
        }
    }

    // Reset material color to original
    private void ResetMaterial()
    {
        material.color = originalColor;
    }
}
