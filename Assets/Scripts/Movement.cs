using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private Vector3 direction;
    [SerializeField] private DrugDrop drugDrop;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rig;

    private void OnValidate()
    {
        if (player == null)
        {
            player = gameObject;
        }
    }

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        animator.SetBool("isWalking", true);
        rig = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GetPosition();
    }

    private void GetPosition()
    {
        startPos = drugDrop.GetStartPosition();
        endPos = drugDrop.GetEndPosition();
        Move();
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            direction = new Vector3(endPos.x - startPos.x, 0, endPos.y - startPos.y).normalized;
            rig.velocity = direction;
            direction = Vector2.zero;
        }
    }
}