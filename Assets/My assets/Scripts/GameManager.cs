﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private bool haveKey;

    [SerializeField]
    private GameObject haveKeyText;

    [Header("WinCondition"), SerializeField]
    private float enemyNumber;


    [Header("GameOver")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Image gameOverBG;
    [SerializeField]
    private Color gameOverColorBG;

    [Header("Win")]
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private Image winBG;
    [SerializeField]
    private Color winColorBG;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Win();
        GameOver();
    }

    public void Win()
    {
        if (EnemyNumber<=0)
        {
            winPanel.SetActive(true);
            StartCoroutine(Fade(true));
            Time.timeScale = 0;
        }
    }


    public void GameOver()
    {
        if (PlayerController.Instance.Health <= 0)
        {
            gameOverPanel.SetActive(true);
            StartCoroutine(Fade(false));
            Time.timeScale = 0;
        }
    }

    public void ActivatedNeedAKey(bool activated)
    {
        haveKeyText.SetActive(activated);
    }

    #region coroutine

    IEnumerator Fade(bool winner)
    {
        if (winner)
        {
            while (winBG.color.a < 0.75f)
            {
                yield return null;

                winColorBG.a = Mathf.Lerp(winColorBG.a, 1, 0.04f);
                winBG.color = winColorBG;


            }
        }
        else
        {
            while (gameOverBG.color.a < 0.75f)
            {
                yield return null;

                gameOverColorBG.a = Mathf.Lerp(gameOverColorBG.a, 1, 0.04f);
                gameOverBG.color = gameOverColorBG;


            }
        }
    }

    #endregion

    #region Get&Set

    public bool HaveKey
    {
        get
        {
            return haveKey;
        }

        set
        {
            haveKey = value;
        }
    }

    public float EnemyNumber
    {
        get
        {
            return enemyNumber;
        }

        set
        {
            enemyNumber = value;
        }
    }

    #endregion
}
