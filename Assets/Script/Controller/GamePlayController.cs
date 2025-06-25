using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections.Generic;

public class GamePlayController : MonoBehaviour
{
	private float score = 0;

	public float Score { get => score; }

	private float sceneStartTime;

	[SerializeField]
	GeneratorController[] monsterGenerators;

	private const float scoreSpeed = 100;
    private static readonly float[] GeneratorOpenScore = { 10000f, 30000f };

    void Start()
	{
		score = 0;
        sceneStartTime = Time.time;
    }

	// Update is called once per frame
	void Update()
	{
		float magnification = (Time.time - sceneStartTime) / 40f;
		magnification = Mathf.Max(1f, magnification);
        score += Time.deltaTime * magnification * scoreSpeed;


        for (int i = 0; i < GeneratorOpenScore.Length; i++)
        {
            if (monsterGenerators[i].gameObject.activeInHierarchy == false && score >= GeneratorOpenScore[i])
            {
                monsterGenerators[i].gameObject.SetActive(true);
            }
        }
    }

	public void AddScore(int score)
	{
		this.score += score;
	}

	public int GetExp()
	{
		return ((int)Mathf.Floor(score)) / ((int)scoreSpeed * 2);
	}

    public string GetGamePlayTime()
    {
        float minute = (Time.time - sceneStartTime) / 60f;
        float second = (Time.time - sceneStartTime) % 60f;

        return string.Format("{0:D2} : {1:D2}", Mathf.FloorToInt(minute), Mathf.FloorToInt(second));
    }
}