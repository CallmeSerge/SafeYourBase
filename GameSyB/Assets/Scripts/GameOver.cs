using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _returnButton;
    private void Start()
    {
        Invoke("ReturnButtonAwake", 2); 
    }
    public void ReturnInGame()
    {
        StartCoroutine(ReturnInGame());
        IEnumerator ReturnInGame()
        {
            _returnButton.transform.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Game");
        }
    }

    private void ReturnButtonAwake()
    {
        _returnButton.SetActive(true);
    }
}
