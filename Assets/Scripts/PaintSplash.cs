using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplash : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private GameObject parent;
    private Vector3 posiitonOffset;
    private Material material;
    private int frameCouter = 0;
    private const int fadeFrames = 500;

    private void Start()
    {
        material = meshRenderer.material;
    }

    // Update splash after animator update
    void LateUpdate()
    {
        if (parent != null)
        {
            // Follow parent object
            this.transform.position = parent.transform.position + posiitonOffset;

            // Fade out
            material.color = new Color(1f, 1f, 1f, 1f - frameCouter / (float)fadeFrames);
            frameCouter++;
            if (frameCouter > fadeFrames)
            {
                Destroy(material);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(material);
            Destroy(this.gameObject);
        }
    }

    // ToDo: Track rotation too?
    public void SetParent(GameObject parent, Vector3 positionOffset = default)
    {
        this.parent = parent;
        this.posiitonOffset = positionOffset;
    }
}
