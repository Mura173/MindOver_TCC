using UnityEngine;

public class ChangePositionOnTrigger : MonoBehaviour
{
    public Transform posStart;
    public Vector3 newPosStart;
    private Vector3 originalPosStart;

    private void Start()
    {
        originalPosStart = posStart.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            posStart.position = newPosStart;

            originalPosStart = newPosStart;

            Debug.Log("posStart mudou para: " + newPosStart);
        }
    }

    public void ResetPosStart()
    {
        posStart.position = originalPosStart;
        Debug.Log("posStart restaurado para: " + originalPosStart);
    }
}
