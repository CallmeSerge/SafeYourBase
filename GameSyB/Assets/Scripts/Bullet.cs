using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private ParticleSystem _vfxPrefab;

    private void Update()
    {
        transform.Translate(new Vector3(0, 1 * _bulletSpeed, 0 ) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "zombielite")
        {
            collision.gameObject.GetComponentInParent<ZombieLiteHealthSystem>().TakeDamage(_bulletDamage, collision);
            ParticleSystem vfx = Instantiate(_vfxPrefab, gameObject.transform.position, Quaternion.identity);
            vfx.Play();
        }
        else if (collision.gameObject.transform.CompareTag("woodbox"))
        {
            collision.gameObject.GetComponent<WoodBox>().TakeDamageWoodBox(_bulletDamage);
            ParticleSystem vfx = Instantiate(_vfxPrefab, gameObject.transform.position, Quaternion.identity);
            vfx.Play();
        }
        if (collision.gameObject != collision.gameObject.transform.CompareTag("automat"))
        {
            Destroy(gameObject);
        }
    }
}
