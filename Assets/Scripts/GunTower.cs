using System.Collections;
using UnityEngine;

public class GunTower : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] float damageDuration = 0.5f;
    [SerializeField] float range = 3;

    void Start()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageDuration);

            Agent target = TowerDefenseUtil.FindClosest(transform.position, range);

            if (target != null)
                target.Damage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);        
    }
}
