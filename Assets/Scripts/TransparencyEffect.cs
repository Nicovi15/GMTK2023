using UnityEngine;

public class TransparencyEffect : MonoBehaviour, IProjectionEffect
{
    [SerializeField, Range(0f, 1f)] float highlightedOpacity = 0.4f;
    
    MeshRenderer[] _meshRenderers;

    private void Awake() => _meshRenderers = GetComponentsInChildren<MeshRenderer>();

    // private void Start()
    // {
    //     ShowHighlight();
    // }

    public void ShowHighlight()
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            foreach (var material in meshRenderer.materials)
            {
                material.SetFloat("_Surface", 1f);
                material.color = new Color( material.color.r, 
                                            material.color.g, 
                                            material.color.b, 
                                            highlightedOpacity);
            }
        }
    }
}
