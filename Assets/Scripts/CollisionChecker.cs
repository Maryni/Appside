using UnityEngine;
using UnityEngine.Events;

public class CollisionChecker : MonoBehaviour
{
    #region private variables

    private UnityAction eventOnCollision;

    #endregion private variables

    #region public functions

    public void SetActionToOnCollisionEvent(UnityAction action)
    {
        eventOnCollision += action;
    }

    #endregion public functions

    #region Unity functions

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider>())
        {
            collision.gameObject.SetActive(false);
            eventOnCollision?.Invoke();
        }
    }

    #endregion Unity functions
}