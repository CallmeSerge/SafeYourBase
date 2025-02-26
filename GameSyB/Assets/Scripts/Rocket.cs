using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speedRocket;
    [SerializeField] private GameObject _volnaPrefab;
    [SerializeField] private float _rocketDamage;
    [SerializeField] private ParticleSystem _rocketBoomPrefab;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _rb.velocity = new Vector3(0,0,-1 * _speedRocket);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioEffects.audioSource.PlayOneShot(AudioEffects.rocketboom);
        if (collision.gameObject.tag != "volna")
        {
            ParticleSystem rocketBoom = Instantiate(_rocketBoomPrefab, gameObject.transform.position, Quaternion.identity);
            rocketBoom.Play();
        }

        if (collision.transform.CompareTag("zombielite"))
        {
            Instantiate(_volnaPrefab, gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponentInParent<ZombieLiteHealthSystem>().TakeDamage(_rocketDamage, collision);
        }
        if (collision.transform.CompareTag("woodbox"))
        {
            Instantiate(_volnaPrefab, gameObject.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<WoodBox>().TakeDamageWoodBox(_rocketDamage);
        }

        Destroy(gameObject);
    }
}
