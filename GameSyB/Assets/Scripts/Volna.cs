using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volna : MonoBehaviour
{
    private float _damageVolna = 1000;
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("woodbox"))
        {
            other.GetComponent<WoodBox>().TakeDamageWoodBox(_damageVolna);
        }
        if (other.transform.CompareTag("zombielite"))
        {
            other.GetComponentInParent<ZombieLiteHealthSystem>().TakeDamage(_damageVolna, other, gameObject);
        }
    }
}
