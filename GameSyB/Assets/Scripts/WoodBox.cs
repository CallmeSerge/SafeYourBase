using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WoodBox : MonoBehaviour
{
    [SerializeField] private float _boxSpeed;
    [SerializeField] private float _boxHeath;
    [SerializeField] private GameObject[] _veaponOrZombie;
    [SerializeField] private ParticleSystem _boxBoomPrefab;
    [SerializeField] private GameObject _volnaPrefab;
    private Rigidbody _rbBox;
    private void Start()
    {
        _rbBox = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _rbBox.velocity = new Vector3(0,0,1 * _boxSpeed);
    }

    public void TakeDamageWoodBox(float damage)
    {
        _boxHeath -= damage;
        if (_boxHeath <= 0)
        {
            GameObject veapon = Instantiate(_veaponOrZombie[Random.Range(0, _veaponOrZombie.Length)], gameObject.transform.position, Quaternion.identity);
            if (veapon.transform.CompareTag("automat"))
            {
                veapon.transform.eulerAngles = new Vector3(0,90,0);
            }
            else if(veapon.transform.CompareTag("bazuka"))
            {
                veapon.transform.eulerAngles = new Vector3(-90, 0, 0);
            }
            ParticleSystem boxBoom =  Instantiate(_boxBoomPrefab, gameObject.transform.position, Quaternion.identity);
            Instantiate(_volnaPrefab, gameObject.transform.position, Quaternion.identity);
            AudioEffects.audioSource.PlayOneShot(AudioEffects.woodBoxDestroy);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("base"))
        {
            Destroy(gameObject);
        }
    }
}
