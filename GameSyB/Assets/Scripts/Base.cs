using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class Base : MonoBehaviour
{
    [SerializeField] private float _healthOfBase;
    [SerializeField] private UnityEvent _deadBase;
    [SerializeField] private Image _lineHeathBase;
    [SerializeField] private GameObject _imageBase;
    private bool _isDeadBase = false;
    private void OnTriggerEnter(Collider other)
    {
        if (_isDeadBase == false)
        {
            if (other.transform.CompareTag("zombielite") && other.GetComponentInParent<Zombie>()._isZombieMoving == true) 
            {
                _imageBase.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
                AudioEffects.audioSource.PlayOneShot(AudioEffects.destroyBase);
                CounterOfZombie();
                Destroy(other.gameObject);
            }
        }   
    }
  
    private void CounterOfZombie()
    {      
            _healthOfBase = _healthOfBase - 10;
            _lineHeathBase.fillAmount = _healthOfBase / 100;

            if (_healthOfBase <= 0)
            {
                Invoke("DestroyBase", 3);
                _isDeadBase = true;
            }       
    }

    private void DestroyBase()
    {
        _deadBase.Invoke();
    }
}
