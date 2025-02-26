using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _returnButton;
    private void Start()
    {
        Invoke("ReturButtonAwake", 2); 
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

    private void ReturButtonAwake()
    {
        _returnButton.SetActive(true);
    }
}
