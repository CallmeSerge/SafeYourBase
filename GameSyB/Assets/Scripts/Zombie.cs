using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _zombieSpeed;
    private Animator _animator;
    private Rigidbody _rb;
    private GameObject _player;
    public bool _isZombieMoving { get; private set; } = true;
    private List<Transform> _transforms;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _transforms = new List<Transform>(GetComponentsInChildren<Transform>());
        foreach(Transform transform in _transforms)
        {
            transform.tag = "zombielite";
        }  
    }
    private void Update()
    {
        if (_isZombieMoving == true)
        {
            gameObject.transform.Translate(new Vector3(0, 0, 1) * _zombieSpeed * Time.deltaTime);
        }
    }

    public void ChangeIsZombieMoving()
    {
        _isZombieMoving = false;
    }
    public void CallMethodDeathPlayer()
    {
        _player.GetComponent<PlayerMove>().DeathPlayer();
    }
    public void CallMethodZombieGoAgain()
    {
        gameObject.transform.eulerAngles = Vector3.zero;
        _zombieSpeed = 3;
        _animator.SetBool("isGoAgain", true);
    }
    public void ZombieMove()
    {
        _rb.velocity = new Vector3(0, 0, 1 * _zombieSpeed);
    }

    public virtual void ZombieAttack(Collision collision)
    {
        _player = collision.gameObject;
        _player.GetComponent<PlayerMove>().ChancheIsDeathing();
        _zombieSpeed = 0;
        transform.LookAt(_player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && _isZombieMoving == true)
        {
            GetComponent<ZombieLiteHealthSystem>().ChancheIsTouchingZombie();
            other.GetComponent<PlayerShooting>().IsDeathingChanche();
            other.gameObject.GetComponent<PlayerMove>().ChancheIsDeathing();
            _zombieSpeed = 0;
            transform.LookAt(other.transform.position);
            GetComponent<Animator>().SetTrigger("isAttacking");
            AudioEffects.audioSource.PlayOneShot(AudioEffects.zombieAttack);
            _player = other.gameObject;
        }
    }
}
