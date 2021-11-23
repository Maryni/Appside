using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool setActions = false;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode scenemode) => OnLoadCallback(scene, scenemode);
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        SetAllActionsWhenLevelLoaded();
        progresSaver.OpenStartPanelOnLoadLevel();
        progresSaver.SetLastLevelAfterLoadNextLevel();
        Debug.Log($"I'm load in scene {SceneManager.GetActiveScene().buildIndex}");
    }

    #endregion Unity functions

    #region private functions

    private void SetAllActionsWhenLevelLoaded()
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
        SetStartValues();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SetActionsToObjects();
        }
    }

    private void SetActionsToObjects()
    {
        if (!setActions)
        {
            drugDrop.SetActionToStartDrug(() => animatorController.ChangeAnimatorState());
            drugDrop.SetActionToEndDrug(() => animatorController.ChangeAnimatorState());
            drugDrop.SetActionToDragging(() => movement.Move());
            collisionChecker.SetActionToOnCollisionEvent(() => particles.SetActive(true));
            collisionChecker.SetActionToOnCollisionEvent(() => countObjectsPlayerDestroy++);
            collisionChecker.SetActionToOnCollisionEvent(() => progresSaver.SetAllCountObjectsInUI(countObjectsPlayerDestroy));
            collisionChecker.SetActionToOnCollisionEvent(() => progresSaver.CheckProgression());
            collisionChecker.SetActionToOnCollisionEvent(() => objectsSaver.CheckObjects());//not a best optimize choice
            setActions = true;
        }
    }

    private void SetStartValues()
    {
        if (objectsSaver == null)
        {
            objectsSaver = FindObjectOfType<ObjectsSaver>();
        }
        countObjectsPlayerDestroy = 0;
        progresSaver.SetDestroyableCountInUI(objectsSaver.GetCountAllObjects());
        progresSaver.SetAllCountObjectsInUI(countObjectsPlayerDestroy);
    }

    #endregion private functions

    #region public functions

    public void LoadNextLevel() // used in UI
    {
        progresSaver.LoadNextLevel();
    }

    #endregion public functions
}