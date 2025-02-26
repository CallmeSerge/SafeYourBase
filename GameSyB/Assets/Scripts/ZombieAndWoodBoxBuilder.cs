using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ZombieAndWoodBoxBuilder : MonoBehaviour
{
    [SerializeField] private GameObject [] _zombiePrefab;
    [SerializeField] private GameObject[] _tegsForLiteZombieAwake;
    [SerializeField] private float _waitForNewZombieAwake;
    [SerializeField] private float _maxTimerForWoodBoxAwake;
    [SerializeField] private GameObject _woodBoxPrefab;
    [SerializeField] private int _maxCountZombieWave;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private ParticleSystem _boxBoomPrefab;
    private List<GameObject> _tegs;
    [SerializeField] private float _timer;
    private bool _isZombieWaveAttaking = false;
    private bool _zvykiZombie = true;

    private void Start()
    {
        StartCoroutine(ZombieLiteAwake());
    }
    private void Update()
    {
       _timer += Time.deltaTime;
    }

    public void ChooseOneWave()
    {
        _waitForNewZombieAwake = 2.5f;
    }

    public void ChooseTwoWave()
    {
        _waitForNewZombieAwake = 2;
    }

    public void ChooseThreeWave()
    {
        _waitForNewZombieAwake = 1.5f;

    }

    IEnumerator ZombieLiteAwake()
    {
            _tegs = new List<GameObject>(_tegsForLiteZombieAwake);
            yield return new WaitForSeconds(_waitForNewZombieAwake);
            int vibraniiTeg = Random.Range(0, _tegs.Count);
            int vibraniiZombie = Random.Range(0, _zombiePrefab.Length);
            Instantiate(_zombiePrefab[vibraniiZombie], _tegs[vibraniiTeg].transform.position, Quaternion.identity);
            if (_timer > _maxTimerForWoodBoxAwake)
            {
                _tegs.RemoveAt(vibraniiTeg);
                GameObject woodBox = Instantiate(_woodBoxPrefab, _tegs[Random.Range(0, _tegs.Count)].transform.position, Quaternion.identity);
                _timer = 0;
            }
            StartCoroutine(ZombieLiteAwake()); 
    }
}
