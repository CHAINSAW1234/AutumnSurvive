using UnityEngine;
using System.Collections;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
	GamePlayController gamePlayController;
	TextMeshProUGUI text;

	void Start()
	{
        gamePlayController = FindAnyObjectByType<GamePlayController>();
		text = GetComponentInChildren<TextMeshProUGUI>();
    }

	void Update()
	{
		text.text = "Score : " + (int)gamePlayController.Score;
    }
}
