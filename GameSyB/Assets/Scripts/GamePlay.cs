using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private ZombieAndWoodBoxBuilder _zombieAndWoodBoxBuilder;
    [SerializeField] private Image _imagePause;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _musicButtonOff;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Image _imageGameOver;
    private float _timer;
    private float _maxTimerForOneNewWaveZombie = 60;
    private float _maxTimerForTwoNewWaveZombie = 180;
    private float _maxTimerForThreeNewWaveZombie = 300;
    private bool _maxTimer = false;
    private bool _isMusicOff = false;

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
        StartCoroutine (GameOverBegin());
        IEnumerator GameOverBegin()
        {
            _imageGameOver.gameObject.SetActive(true);
            Tween imageFade = _imageGameOver.DOFade(1,1);
            imageFade.SetLink(gameObject);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AfterYandexPause()
    {
        _pauseButton.gameObject.SetActive(true);
    }
    public void OnTimer()
    {
        _pauseButton.gameObject.SetActive(false);
        //Invoke("AfterYandexPause", 2);
    }
    public void GamePause()
    {
        if (Time.timeScale == 1)
        {
            _imagePause.gameObject.SetActive(true);
            if (_isMusicOff == false)
            {
                _audioSource.enabled = false;
            }
            _musicButton.interactable = false;  
            _musicButtonOff.interactable = false;
            Time.timeScale = 0;
        }
        else
        {
            _imagePause.gameObject.SetActive(false);
            if (_isMusicOff == false)
            {
                _audioSource.enabled = true;
            }
            _musicButton.interactable = true;
            _musicButtonOff.interactable = true;
            Time.timeScale = 1;
        }
    }

    public void ButtonMusic()
    {
        if (_audioSource.enabled == true)
        {
            _audioSource.enabled = false;
            _musicButton.gameObject.SetActive(false);
            _musicButtonOff.gameObject.SetActive(true);
            _isMusicOff = true;

        }
        else
        {
            _audioSource.enabled = true;
            _musicButton.gameObject.SetActive(true);
            _musicButtonOff.gameObject.SetActive(false);
            _isMusicOff = false;
        }
    }

  
}
