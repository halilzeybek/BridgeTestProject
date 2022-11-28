using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonobehaviourSingleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody rb;
    float horizontal, vertical;
    private Vector3 direction;

    Vector3 lastPosition;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");     
    }

    private void FixedUpdate()
    {
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float angleToRotate = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angleToRotate, 0f);

            rb.MovePosition(transform.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
       
    }

    int surrondedCubes = 0;
    private void OnTriggerEnter(Collider other)
    {
        // When player is surronded by cubes and cant move
        if(other.tag == "Cube")
        {
            surrondedCubes++;
              
            if(surrondedCubes >= 4)
            {
                GameManager.Instance.Lose();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Cube")
           surrondedCubes--;
    }


}
