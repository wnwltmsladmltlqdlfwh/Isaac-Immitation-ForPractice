using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosedRange;
    float damage = 10f;
    float passedTime;

    public void Init()
    {
        passedTime = 2.5f;

        _ = StartCoroutine(OnExplodeBomb());
    }

    IEnumerator OnExplodeBomb()
    {
        while(passedTime > 0)
        {
            passedTime -= Time.deltaTime * 1f;
            yield return null;
        }

        Collider2D[] colArray = Physics2D.OverlapCircleAll(this.transform.position, explosedRange);

        foreach (Collider2D col in colArray)
        {
            if (col.gameObject.GetComponent<PlayerController>())
            {
                col.gameObject.GetComponent<PlayerController>().OnDamaged();
            }
            else if (col.gameObject.GetComponent<EnemyController>())
            {
                col.gameObject.GetComponent<EnemyController>().OnDamage(damage);
            }
            else if (col.gameObject.GetComponent<PickUpItem>())
            {
                Vector3 forceDir = (col.transform.position - this.transform.position).normalized;

                col.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * 700);
            }
        }

        Vector3 effectPos = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);

        ObjectManager.Instance.BoomExplodeEffect(effectPos);

        PoolingManager.Instance.Push(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, explosedRange);
    }
}
