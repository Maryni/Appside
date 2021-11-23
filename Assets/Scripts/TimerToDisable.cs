using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToDisable : MonoBehaviour
{
    #region Inspector variables

#pragma warning disable
    [SerializeField] private float timeForDisable;
#pragma warning restore

    #endregion Inspector variables

    #region private variables

    private Coroutine coroutineTimer;

    #endregion private variables

    #region Unity functions

    private void OnEnable()
    {
        StartTimerCoroutine();
    }

    #endregion Unity functions

    #region private functions

    private void StartTimerCoroutine()
    {
        if (coroutineTimer != null)
        {
            StopCoroutineTimer();
        }
        coroutineTimer = StartCoroutine(DisableByTime(timeForDisable));
    }

    private IEnumerator DisableByTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        StopCoroutineTimer();
    }

    private void StopCoroutineTimer()
    {
        gameObject.SetActive(false);
        StopCoroutine(coroutineTimer);
        coroutineTimer = null;
    }

    #endregion private functions
}