using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    #region Inspector variables

    [SerializeField] private Animator animator;

    #endregion Inspector variables

    #region private variables

    private bool value = true;

    #endregion private variables

    #region Unity functions

    private void Start()
    {
        if (animator == null)
        {
            animator = FindObjectOfType<Animator>();
        }
        animator.SetBool("isWalking", value);
    }

    #endregion Unity functions

    #region public funtions

    public void ChangeAnimatorState()
    {
        if (!value)
        {
            value = true;
        }
        else
        {
            value = false;
        }
        animator.SetBool("isWalking", value);
        Debug.Log("I'm clicked");
    }

    #endregion public funtions
}