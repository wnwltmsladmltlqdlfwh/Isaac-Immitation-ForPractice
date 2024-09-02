using UnityEngine;

public class EndDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            var endP = collision.gameObject.GetComponent<PlayerController>().PlayerEndMotion();
            _ = StartCoroutine(endP);
        }
    }
}
