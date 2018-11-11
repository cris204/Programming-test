using System.Collections;
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

    [Header("GameOver")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Image gameOverBG;
    [SerializeField]
    private Color gameOvercolorBG;

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



    public void GameOver()
    {

            gameOverPanel.SetActive(true);
            StartCoroutine(Fade());
            Time.timeScale = 0;
        
    }

    public void ActivatedNeedAKey(bool activated)
    {
        haveKeyText.SetActive(activated);
    }

    #region coroutine

    IEnumerator Fade()
    {
        while (gameOverBG.color.a < 0.99f)
        {
            yield return null;

            gameOvercolorBG.a = Mathf.Lerp(gameOvercolorBG.a, 1, 0.04f);
            gameOverBG.color = gameOvercolorBG;


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

    #endregion
}
