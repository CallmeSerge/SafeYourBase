using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private ZombieAndWoodBoxBuilder _zombieAndWoodBoxBuilder;
    [SerializeField] private Image _imagePause;
    [SerializeField] private AudioSource _audioSource;
    private float _timer;
    private float _maxTimerForOneNewWaveZombie = 60;
    private float _maxTimerForTwoNewWaveZombie = 180;
    private float _maxTimerForThreeNewWaveZombie = 300;
    private bool _maxTimer = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (_maxTimer == false)
        {
            _timer += Time.deltaTime;

            if (_timer > _maxTimerForOneNewWaveZombie)
            {
                _zombieAndWoodBoxBuilder.ChooseOneWave();
            }

            if (_timer > _maxTimerForTwoNewWaveZombie)
            {
                _zombieAndWoodBoxBuilder.ChooseTwoWave();
            }

            if (_timer > _maxTimerForThreeNewWaveZombie)
            {
                _zombieAndWoodBoxBuilder.ChooseThreeWave();
            }
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            _imagePause.gameObject.SetActive(true);
            _audioSource.enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            _imagePause.gameObject.SetActive(false);
            _audioSource.enabled = true;
            Time.timeScale = 1;
        }
    }
}
