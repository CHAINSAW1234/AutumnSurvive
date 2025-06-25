using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    private GamePlayController gamePlayController;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        gamePlayController = FindAnyObjectByType<GamePlayController>();
    }

    private void OnEnable()
    {
        text.text = "Score : " + ((int)gamePlayController.Score).ToString();
    }

}
