using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public SpriteRenderer itemSprite;
    public Animator animator;

    public virtual void Init()
    {
        if(animator == null)
        {
            if (!this.GetComponent<Animator>()) { return; }

            animator = GetComponent<Animator>();
        }
        if(itemSprite == null)
        {
            itemSprite = GetComponent<SpriteRenderer>();
        }
    }

    public virtual void GainPerformance()
    {
        PoolingManager.Instance.Push(this);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision != null && collision.gameObject.CompareTag("Player"))
        {
            GainPerformance();
        }
    }
}
