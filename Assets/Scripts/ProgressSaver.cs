using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressSaver : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Text textLevel;
    [SerializeField] private Text countObjectDestroyable;
    [SerializeField] private Text countObjectWasDestroyed;
    [SerializeField] private GameObject panelNextLevel;
    [SerializeField] private GameObject panelToPlay;

    #endregion Inspector variables

    #region private variables

    private List<int> levels = new List<int>();
    private List<int> tempLevels = new List<int>();
    private int lastLevel;
    private bool standartLevelLoading = true;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        SetValueToListSceneIndex();
        SetStartValues();
    }

    #endregion Unity functions

    #region public functions

    [ContextMenu("LoadNext Level")] //used for tests
    public void LoadNextLevel()
    {
        AddLevel();
        if (standartLevelLoading)
        {
            if (lastLevel == 1)
            {
                SceneManager.LoadScene(lastLevel + 1);
            }
        }
        else
        {
            SetLevelsToTempLevels();
            tempLevels.RemoveAt(lastLevel - 1);
            SceneManager.LoadScene(tempLevels[Random.Range(0, tempLevels.Count)]);
        }
        panelNextLevel.SetActive(false);
    }

    public void AddLevel()
    {
        textLevel.text = (int.Parse(textLevel.text) + 1).ToString();
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

    public void SetLastLevelAfterLoadNextLevel()
    {
        lastLevel = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            standartLevelLoading = false;
            print("standartLevelLoading = false");
        }
    }

    public void OpenStartPanelOnLoadLevel()
    {
        panelToPlay.SetActive(true);
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

    private void SetValueToListSceneIndex()
    {
        var temp = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < temp; i++)
        {
            levels.Add(i);
        }
        levels.RemoveAt(0); // remove "boot" level
    }

    private void SetLevelsToTempLevels()
    {
        tempLevels = new List<int>();
        for (int i = 0; i < levels.Count; i++)
        {
            tempLevels.Add(levels[i]);
        }
    }

    #endregion private functions
}