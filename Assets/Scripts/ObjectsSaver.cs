using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSaver : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private List<GameObject> destroyableObjects;
    [SerializeField] private List<GameObject> allObjects; //include objects which was been hide ("destroyed")
    private BoxCollider[] temp;

    #endregion Inspector variables

    #region public functions

    public int GetCountActiveObjects()
    {
        return destroyableObjects.Count;
    }

    public int GetCountAllObjects()
    {
        return allObjects.Count;
    }

    public void CheckObjects()
    {
        for (int i = 0; i < destroyableObjects.Count; i++)
        {
            if (!destroyableObjects[i].activeInHierarchy)
            {
                destroyableObjects.RemoveAt(i);
            }
        }
    }

    #endregion public functions

    #region private functions

    [ContextMenu("SetAllObjectsToLists")]
    private void SetAllObjectsToLists()
    {
        temp = FindObjectsOfType<BoxCollider>();
        for (int i = 0; i < temp.Length; i++)
        {
            allObjects.Add(temp[i].gameObject);
            if (temp[i].gameObject.activeInHierarchy)
            {
                destroyableObjects.Add(temp[i].gameObject);
            }
        }
    }

    #endregion private functions
}