using UnityEngine;

public class Arrow : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _father;
    [SerializeField] Vector3 rotationStep = new Vector3(0f, 90f, 0f);

    public void Interact()
    {
        Rotate();
    }

    private void Rotate()
    {
        _father.rotation *= Quaternion.Euler(rotationStep);
    }
}
