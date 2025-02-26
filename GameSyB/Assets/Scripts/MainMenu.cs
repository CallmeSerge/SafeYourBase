using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _buttonStart;
    [SerializeField] private GameObject _imageLoading;
    [SerializeField] private AudioSource _audioSource;

    public void GoToGame()
    {
        StartCoroutine(GoToSceneOne());
        IEnumerator GoToSceneOne()
        {
            _buttonStart.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
            _imageLoading.SetActive(true);
            _imageLoading.GetComponent<Image>().DOFade(1, 1.5f);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Game");
        }
    }

    /*private void Start()
    {
        StartCoroutine(PunchStartButton());
        IEnumerator PunchStartButton()
        {
            yield return new WaitForSeconds(2);
            while (true)
            {
                yield return new WaitForSeconds(3);
                if (_isLoadingOneScene == false)
                {
                    _buttonStart.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.5f);
                }
            }
        }
    }*/
}
