using UnityEngine;

public class SocketController : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void ScaleDown()
    {

        transform.localScale = Vector3.zero * 0.5f; // Adjust the scale factor as needed
    }

    public void ScaleUp()
    {
        transform.localScale = originalScale;
    }
}
