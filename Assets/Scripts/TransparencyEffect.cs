using UnityEngine;

public class TransparencyEffect : MonoBehaviour, IProjectionEffect
{
    [SerializeField, Range(0f, 1f)] float highlightedOpacity = 0.7f;
    
    MeshRenderer[] _meshRenderers;
    
    private static readonly int Surface = Shader.PropertyToID("_Surface");

    private void Awake() => _meshRenderers = GetComponentsInChildren<MeshRenderer>();

    public void ShowHighlight()
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            foreach (var material in meshRenderer.materials)
            {
                material.SetFloat(Surface, 1f);
                material.color = new Color( material.color.r, 
                                            material.color.g, 
                                            material.color.b, 
                                            highlightedOpacity);
            }
        }
    }
}
