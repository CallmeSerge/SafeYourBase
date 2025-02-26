using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _automat;
    [SerializeField] private GameObject _bazuka;
    [SerializeField] private GameObject _tegsForBulletPistol;
    [SerializeField] private GameObject _tegsForBulletAutomat;
    [SerializeField] private GameObject _tegsForBulletBazuka;
    [SerializeField] private GameObject _pistolBulletPrefab;
    [SerializeField] private GameObject _bazukaBulletPrefab;
    [SerializeField] private float _maxTimeForAutomatRecharge;
    [SerializeField] private TextMeshProUGUI _textCountOfBullet;
    [SerializeField] private GameObject _bulletUI;
    [SerializeField] private GameObject _fireButtonForPistolAndBazuka;
    [SerializeField] private GameObject _fireButtonForAutomat;
    private Animator _animator;
    private float _timerForAutomatRecharge;
    private int _puliForAutomatCount;
    private bool _isDeathing = false;
    private bool _isStartAutomatShooting = false;
    private bool _isBazukaAlredyShooting = false;
    private bool _isPistolShooting = true;
    private bool _isAutomatShooting = false;
    private bool _isBazukaShooting = false;

    private void Awake()
    {  
        if(YandexGame.EnvironmentData.isDesktop == true)
        {
            _fireButtonForPistolAndBazuka.SetActive(false);
        }
        _automat.SetActive(false);
        _bazuka.SetActive(false);
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
            if (_isPistolShooting == true)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    PistolShooting();
                }
            }
            else if (_isAutomatShooting == true)
            {
                _pistol.SetActive(false);
                _automat.SetActive(true);
                _timerForAutomatRecharge += Time.deltaTime;

                if (Input.GetKey(KeyCode.K) || _isStartAutomatShooting == true)
                {
                    Automatshooting();      
                }
                else
                {
                    _animator.SetBool("isAutomatShooting", false);
                }
            }
            else if (_isBazukaShooting == true)
            {
                _pistol.SetActive(false);
                _automat.SetActive(true);
                _bazuka.SetActive(true);

                if (Input.GetKeyDown(KeyCode.K))
                {
                    BazukaShooting();
                }
            }
    }

    public void PistolShooting()
    {
        if (_isDeathing == false)
        {
            _animator.SetTrigger("isPistolShooting");
            GameObject newBullet = Instantiate(_pistolBulletPrefab, _tegsForBulletPistol.transform.position, Quaternion.identity);
            AudioEffects.audioSource.PlayOneShot(AudioEffects.pistolShoot);
            newBullet.transform.eulerAngles = new Vector3(90, 0, 180);
        }
    }

    public void IsDeathingChanche()
    {
        _isDeathing = true;
    }

    public void IsAutomatShootingDown()
    {
        _isStartAutomatShooting = true;
    }
    public void IsAutomatShootingUp()
    {
        _isStartAutomatShooting = false;
        _animator.SetBool("isAutomatShooting", false);
    }
    public void Automatshooting()
    {
        if (_isDeathing == false)
        {
            if (_puliForAutomatCount > 0)
            {
                if (_timerForAutomatRecharge > _maxTimeForAutomatRecharge)
                {
                    _animator.SetBool("isAutomatShooting", true);
                    GameObject newAutomatBullet = Instantiate(_pistolBulletPrefab, _tegsForBulletAutomat.transform.position, Quaternion.identity);
                    AudioEffects.audioSource.PlayOneShot(AudioEffects.pistolShoot);
                    _puliForAutomatCount -= 1;
                    _textCountOfBullet.text = _puliForAutomatCount.ToString();
                    newAutomatBullet.transform.eulerAngles = new Vector3(90, 0, 180);
                    _timerForAutomatRecharge = 0;
                }

                if (_puliForAutomatCount == 0)
                {
                    ChanchePistol();
                    _animator.SetBool("isAutomatShooting", false);
                }
            }
        } 
    }

    public void ChanchePistol()
    {
        _bulletUI.SetActive(false);
        _fireButtonForPistolAndBazuka.SetActive(true);
        _fireButtonForAutomat.SetActive(false);
        _textCountOfBullet.text = null;
        _animator.SetLayerWeight(2, 0);
        _animator.SetLayerWeight(1, 1);
        _animator.SetLayerWeight(3, 0);
        _animator.SetLayerWeight(0, 1);
        _animator.SetLayerWeight(4, 0);
        _bazuka.SetActive(false);
        _automat.SetActive(false);
        _pistol.SetActive(true);
        _isAutomatShooting = false;
        _isPistolShooting = true;
        _isBazukaShooting = false;
        _isStartAutomatShooting = false;
    }

    public void PistolOrBazukaShootingButton()
    {
        if (_isBazukaShooting == false)
        {
            PistolShooting();
        }
        else
        {
            BazukaShooting();
        }
    }

    private void BazukaShooting()
    {
        if (_isDeathing == false)
        {
            if (_isBazukaAlredyShooting == false)
            {
                _animator.SetTrigger("isBazukaShotting");
                AudioEffects.audioSource.PlayOneShot(AudioEffects.bazukaShoot);
                GameObject rocket = Instantiate(_bazukaBulletPrefab, _tegsForBulletBazuka.transform.position, Quaternion.identity);
                rocket.transform.eulerAngles = new Vector3(0, 180, 0);
                _isBazukaAlredyShooting = true;
            }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("automat"))
        {
            AudioEffects.audioSource.PlayOneShot(AudioEffects.perezaryadka);
            _bulletUI.SetActive(true);
            _fireButtonForPistolAndBazuka.SetActive(false);
            if (YandexGame.EnvironmentData.isDesktop == false)
            {
                _fireButtonForAutomat.SetActive(true);
            }
            _animator.SetLayerWeight(0,0);
            _animator.SetLayerWeight(1,0);
            _animator.SetLayerWeight(2, 1);
            _animator.SetLayerWeight(3, 1);
            _animator.SetLayerWeight(4, 0);
            _puliForAutomatCount += 50;
            _textCountOfBullet.text = _puliForAutomatCount.ToString();
            _isPistolShooting = false;
            _isAutomatShooting = true;
        }
        else if (other.transform.CompareTag("bazuka"))
        {
            AudioEffects.audioSource.PlayOneShot(AudioEffects.perezaryadka);
            _bulletUI.SetActive(false); 
            _isBazukaAlredyShooting = false;
            _fireButtonForPistolAndBazuka.SetActive(true);
            _fireButtonForAutomat.SetActive(false);
            _animator.SetLayerWeight(0, 0);
            _animator.SetLayerWeight(1, 0);
            _animator.SetLayerWeight(2, 1);
            _animator.SetLayerWeight(4, 1);
            _animator.SetLayerWeight(3, 0);
            _isAutomatShooting = false;
            _isPistolShooting = false;
            _isBazukaShooting = true;
            _isStartAutomatShooting = false;
        }
    }
}
