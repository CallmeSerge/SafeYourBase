using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localizator : MonoBehaviour
{
    [SerializeField] private Button _buttonRussian;
    [SerializeField] private Button _buttonEnglish;

    private void Awake()
    {
        if (PlayerPrefs.GetString("ChooseLanguage") == string.Empty)
        {
            _buttonRussian.gameObject.SetActive(true);
            _buttonEnglish.gameObject.SetActive(false);
            PlayerPrefs.SetString("ChooseLanguage", "ru");
        }
        else if (PlayerPrefs.GetString("ChooseLanguage") == "ru")
        {
            _buttonRussian.gameObject.SetActive(true);
            _buttonEnglish.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetString("ChooseLanguage") == "en")
        {
            _buttonRussian.gameObject.SetActive(false);
            _buttonEnglish.gameObject.SetActive(true);
        }
    }
    public void ChooseLanguage()
    {
        if (_buttonRussian.gameObject.activeSelf == true)
        {
            _buttonRussian.gameObject.SetActive(false);
            _buttonEnglish.gameObject.SetActive(true);
            PlayerPrefs.SetString("ChooseLanguage", "en");
        }
        else if (_buttonRussian.gameObject.activeSelf == false)
        {
            _buttonRussian.gameObject.SetActive(true);
            _buttonEnglish.gameObject.SetActive(false);
            PlayerPrefs.SetString("ChooseLanguage", "ru");
        }
    }
}
