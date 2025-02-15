using System.Collections;
using UnityEngine;

public class GunTower : Tower
{
    [Header("Gun")]
    [SerializeField] float damage = 10;
    [SerializeField] float damageDuration = 0.5f; 

    void Start()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageDuration);

            Agent target = FindTarget();

            if (target != null)
                target.Damage(damage);
        }
    }
}
