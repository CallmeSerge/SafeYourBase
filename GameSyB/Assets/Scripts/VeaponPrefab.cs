using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class VeaponPrefab : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.velocity = new Vector3(0, 0, 1 * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
