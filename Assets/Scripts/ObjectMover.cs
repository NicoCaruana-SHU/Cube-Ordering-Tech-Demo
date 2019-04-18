using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed_ = 2;   // Can be used to modify how quickly the blocks take their ordered position.

    private Vector3 startPosition_;     // Holds the position randomly generated on spawn, used for lerping in the Move function;
    private Vector3 destination_;
    private bool moving_;
    private float amountMoved_;
    private ParticleSystem ps_;

    private void Awake()
    {
        // Initialising the member variables in Awake as this class is instantiated at runtime, and start occurs too late.
        ps_ = GetComponent<ParticleSystem>();

        startPosition_ = transform.position;
        destination_ = Vector3.zero;
        moving_ = false;
        amountMoved_ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving_)
        {
            if (amountMoved_ < 1.0f)
            {
                Move();
            }
            else
            {
                moving_ = false;
                if (ps_ != null)
                {
                    ps_.Play(); // Once the object is in its final position, play the particle effect if it has one attached.
                }
            }
        }
    }

    public void SetDestination(Vector3 pos)
    {
        destination_ = pos;
    }

    private void Move()
    {
        amountMoved_ += movementSpeed_ * Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition_, destination_, amountMoved_);
    }

    public void StartMoving()
    {
        moving_ = true;
    }
}
