using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Inspector variables

#pragma warning disable
    [SerializeField] private float modSpeed;
#pragma warning restore

    #endregion Inspector variables

    #region private variables

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 direction;
    private GameObject player;
    private Rigidbody rig;
    private Coroutine jumpCoroutine;
    private bool inJump;
    private DrugDrop drugDrop;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (player == null)
        {
            player = gameObject;
        }
        if (rig == null)
        {
            rig = player.GetComponent<Rigidbody>();
        }
        if (drugDrop == null)
        {
            drugDrop = FindObjectOfType<DrugDrop>();
        }
    }

    private void Start()
    {
        startPos = drugDrop.GetStartPosition();
    }

    #endregion Unity functions

    #region public functions

    public void Move()
    {
        GetPosition();
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            direction = new Vector3(endPos.x - startPos.x, 0, endPos.y - startPos.y).normalized;
            RotateOnDirection();
            rig.velocity = direction * modSpeed;
            direction = Vector2.zero;
            if (drugDrop.GetCallback())
            {
                Jump();
            }
        }
    }

    #endregion public functions

    #region private functions

    private void GetPosition()
    {
        endPos = drugDrop.GetEndPosition();
    }

    private void RotateOnDirection()
    {
        player.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Jump()
    {
        inJump = true;
        rig.velocity = Vector3.zero;
        rig.AddForce(Vector3.up * 20f, ForceMode.Impulse);
        if (inJump)
        {
            if (Input.touchCount == 2)
            {
                rig.AddForce(Vector3.down * 4f, ForceMode.Impulse);
                inJump = false;
            }
        }
        if (jumpCoroutine == null)
        {
            jumpCoroutine = StartCoroutine(StopJump());
        }
    }

    private IEnumerator StopJump()
    {
        yield return new WaitForSeconds(1.5f);
        rig.velocity = Vector3.zero;
        StopCoroutine(StopJump());
        jumpCoroutine = null;
    }

    #endregion private functions
}