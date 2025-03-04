using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class AdvertisingManager : MonoBehaviour
{
    [SerializeField] private GameObject _textOne;
    [SerializeField] private GameObject _textTwo;
    [SerializeField] private UnityEvent _onStartAdvertising;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 63)
        {
            _textTwo.SetActive(true);
        }
        if (_timer > 64)
        {
            _textTwo.SetActive(false);
            _textOne.SetActive(true);
        }
        if (_timer > 65)
        {
            StartAdvertising();
            _timer = 0;
        }
    }

    private void StartAdvertising()
    {
        _textOne.SetActive(false);
        _onStartAdvertising.Invoke();
        YandexGame.FullscreenShow();
    }
}
