using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZombieLiteHealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _hP;
    [SerializeField] private Image _healthBar;
    [SerializeField] private int _forceStrength;
    [SerializeField] private int _forceStrengthRocket;
    [SerializeField] private int _forceStrengthForVolna;
    private List<Rigidbody> _rigidbodies;
    private Animator _animator;
    private bool _isTouchingZombie = false;
    private bool _isZombieAlreadyDead = false;
    private PointsManager _pointManager;

    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    public void Construct(PointsManager pointManager)
    {
        _pointManager = pointManager;
    }
    public void ChancheIsTouchingZombie()
    {
        _isTouchingZombie = true;
    }
    public void TakeDamage(float bulletDamage, Collision other)
    {
        _maxHP -= bulletDamage;
        _healthBar.fillAmount = _maxHP / _hP;
        if (_maxHP < 0 )
        {
            _maxHP = 0;
        }
        if (_maxHP == 0)
        {
            if (_isZombieAlreadyDead == false)
            {
                if (_isTouchingZombie == false)
                {
                    _isZombieAlreadyDead = true;
                    DeathZombieLite(other);
                }
            }       
        }
    }
    public void TakeDamage(float bulletDamage, Collider other, GameObject volna)
    {
        _maxHP -= bulletDamage;
        _healthBar.fillAmount = _maxHP / _hP;
        if (_maxHP < 0)
        {
            _maxHP = 0;
        }
        if (_maxHP == 0)
        {
            if (_isZombieAlreadyDead == false)
            {
                if (_isTouchingZombie == false)
                {
                    _isZombieAlreadyDead = true;
                    DeathZombieLite(other, volna);
                }
            } 
        }
    }

    private void DeathZombieLite(Collision otherCollision) 
    {
        AudioEffects.audioSource.PlayOneShot(AudioEffects.zombieDead);
        GetComponent<Zombie>().ChangeIsZombieMoving();
        _animator.enabled = false;
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
        if (otherCollision.transform.CompareTag("rocket"))
        {
            otherCollision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.back * _forceStrengthRocket, otherCollision.transform.position, ForceMode.Impulse);
        }
        else
        {
            otherCollision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.back * _forceStrength, otherCollision.transform.position, ForceMode.Impulse);
        }
        _pointManager.PointManager();
        Invoke("DestroyZombieLite", 7);
    }

    private void DeathZombieLite(Collider otherCollider, GameObject volna)
    {
        AudioEffects.audioSource.PlayOneShot(AudioEffects.zombieDead);
        GetComponent<Zombie>().ChangeIsZombieMoving();
        _animator.enabled = false;
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
        Vector3 directionVolna = otherCollider.transform.position - volna.transform.position;
        if(otherCollider.TryGetComponent<Rigidbody>(out Rigidbody otherColliderRB))
        {
                otherColliderRB.AddForceAtPosition(directionVolna * _forceStrengthForVolna, otherCollider.transform.position, ForceMode.Impulse);
        }
        _pointManager.PointManager();
        Invoke("DestroyZombieLite", 7);
    }

    private void DestroyZombieLite()
    {
       Destroy(gameObject); 
    }
}
