using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region private variables

    private GameObject player;
    private GameObject particles;
    private Movement movement;
    private AnimatorController animatorController;
    private ObjectsSaver objectsSaver;
    private CollisionChecker collisionChecker;
    private ProgressSaver progresSaver;
    private DrugDrop drugDrop;
    private int countObjectsPlayerDestroy = 0;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        if (movement == null)
        {
            movement = FindObjectOfType<Movement>();
        }
        if (player == null)
        {
            player = movement.gameObject;
        }
        if (animatorController == null)
        {
            animatorController = GetComponent<AnimatorController>();
        }
        if (objectsSaver == null)
        {
            objectsSaver = FindObjectOfType<ObjectsSaver>();
        }
        if (collisionChecker == null)
        {
            collisionChecker = FindObjectOfType<CollisionChecker>();
        }
        if (drugDrop == null)
        {
            drugDrop = FindObjectOfType<DrugDrop>();
        }
        if (particles == null)
        {
            particles = FindObjectOfType<ParticleSystem>().gameObject;
            particles.SetActive(false);
        }
        if (progresSaver == null)
        {
            progresSaver = GetComponent<ProgressSaver>();
        }

        SetActionsToObjects();
        SetStartValues();
    }

    #endregion Unity functions

    #region private functions

    private void SetActionsToObjects()
    {
        drugDrop.SetActionToStartDrug(() => animatorController.ChangeAnimatorState());
        drugDrop.SetActionToEndDrug(() => animatorController.ChangeAnimatorState());
        drugDrop.SetActionToDragging(() => movement.Move());
        collisionChecker.SetActionToOnCollisionEvent(() => particles.SetActive(true));
        collisionChecker.SetActionToOnCollisionEvent(() => countObjectsPlayerDestroy++);
        collisionChecker.SetActionToOnCollisionEvent(() => progresSaver.SetAllCountObjectsInUI(countObjectsPlayerDestroy));
        collisionChecker.SetActionToOnCollisionEvent(() => progresSaver.CheckProgression());
    }

    private void SetStartValues()
    {
        progresSaver.SetDestroyableCountInUI(objectsSaver.GetCountAllObjects());
        progresSaver.SetAllCountObjectsInUI(countObjectsPlayerDestroy);
    }

    #endregion private functions

    #region public functions

    public void LoadNextLevel()
    {
        progresSaver.LoadNextLevel();
    }

    #endregion public functions
}