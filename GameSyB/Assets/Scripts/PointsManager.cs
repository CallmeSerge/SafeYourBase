using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPoints;
    private int _points = 0;
    public void PointManager()
    {
        _points += 1;
        _textPoints.text = _points.ToString();
        if (_points > YandexGame.savesData.points)
        {
            YandexGame.savesData.points = _points;
            YandexGame.NewLeaderboardScores("SyBLeaderboard", YandexGame.savesData.points);
            YandexGame.SaveProgress();
        }
    }
}
