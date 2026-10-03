using UnityEngine;

public class TriggerHighlight : MonoBehaviour
{
    public Color highlightColor = Color.yellow;

    private Renderer objectRenderer;
    private Material originalMaterial;
    private Material highlightMaterial;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        originalMaterial = objectRenderer.material;

        highlightMaterial = new Material(originalMaterial);
        highlightMaterial.EnableKeyword("_EMISSION");
        highlightMaterial.SetColor("_EmissionColor", highlightColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectRenderer.material = highlightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectRenderer.material = originalMaterial;
        }
    }
}
