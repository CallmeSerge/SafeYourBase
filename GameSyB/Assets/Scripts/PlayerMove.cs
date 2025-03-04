using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using YG;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private UnityEvent _gameOver;
    [SerializeField] private Button _leftButton, _rightButton;
    private CapsuleCollider _capsulaCollider;
    private Animator _animator;
    private bool _isDeathing = false;
    private float _go = 0;

    private void Awake()
    {
        if (YandexGame.EnvironmentData.isDesktop == true)
        {
            _leftButton.gameObject.SetActive(false);
            _rightButton.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsulaCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
            if (_isDeathing == false)
            {
               //float Go = _joystick.Horizontal;
               if (YandexGame.EnvironmentData.isDesktop == true)
            {
                _go = Input.GetAxis("Horizontal");
            }
            _rb.velocity = new Vector3(-_go * _speed, 0, 0);
                if (_go > 0)
                {
                    //_rb.velocity = new Vector3(-1 * _speed, 0, 0);
                    _animator.SetBool("isWalkingRight", true);
                    _animator.SetBool("isWalkingLeft", false);

            }
            else if (_go < 0)
                {
                    //_rb.velocity = new Vector3(1 * _speed, 0, 0);
                    _animator.SetBool("isWalkingLeft", true);
                    _animator.SetBool("isWalkingRight", false);

            }
            if (_go == 0)
                {
                    _animator.SetBool("isWalkingRight", false);
                    _animator.SetBool("isWalkingLeft", false);
                }
            }

    }

    public void PushLeftButton()
    {
        _go = -1;
    }
    public void PushRightButton()
    {
        _go = 1;

    }
    public void ButtonUp()
    {
        _go = 0;
    }

    public void DeathPlayer()
    {
        _rb.isKinematic = true;
        _animator.SetTrigger("isDeath");
        _capsulaCollider.enabled = false;
        Invoke("CallGameOver", 4);
    }

    public void ChancheIsDeathing()
    {
        _animator.SetBool("isAutomatShooting", false);
        _animator.SetBool("isWalkingRight", false);
        _animator.SetBool("isWalkingLeft", false);
        _isDeathing = true;
    }

    private void CallGameOver()
    {
        _gameOver.Invoke();
    }
}
