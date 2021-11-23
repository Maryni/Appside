using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressSaver : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private int score;
    [SerializeField] private Text textLevel;
    [SerializeField] private Text countObjectDestroyable;
    [SerializeField] private Text countObjectWasDestroyed;
    [SerializeField] private GameObject panelNextLevel;

    #endregion Inspector variables

    #region private variables

    private List<int> levels = new List<int>() { 0, 1, 2 };
    private int lastLevel;
    private bool standartLevelLoading = true;

    #endregion private variables

    #region Unity functions

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        SetStartValues();
    }

    #endregion Unity functions

    #region public functions

    public void LoadNextLevel()
    {
        AddLevel();
        if (standartLevelLoading)
        {
            if (lastLevel == 0 || lastLevel == 1)
            {
                SceneManager.LoadSceneAsync(lastLevel++);
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                standartLevelLoading = false;
            }
        }
        else
        {
            List<int> temp = levels;
            temp.Remove(lastLevel);
            SceneManager.LoadSceneAsync(temp[Random.Range(0, temp.Count)]);
        }
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        panelNextLevel.SetActive(false);
    }

    public void AddLevel()
    {
        int level = int.Parse(textLevel.text);
        level++;
        textLevel.text = level.ToString();
    }

    public void CheckProgression()
    {
        if (countObjectDestroyable.text == countObjectWasDestroyed.text)
        {
            panelNextLevel.SetActive(true);
        }
    }

    public void SetAllCountObjectsInUI(int count)
    {
        countObjectWasDestroyed.text = count.ToString();
    }

    public void SetDestroyableCountInUI(int count)
    {
        countObjectDestroyable.text = count.ToString();
    }

    #endregion public functions

    #region private functions

    private void SetStartValues()
    {
        SetStartLevel();
    }

    private void SetStartLevel()
    {
        textLevel.text = "1";
    }

    #endregion private functions
}