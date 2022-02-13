/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.25f;
     private Vector3 offset;
    #endregion

    #region UnityMethods

    private void Start()
    {
        offset = new Vector3(transform.position.x - target.position.x, 
            transform.position.y - target.position.y, transform.position.z - target.position.z);

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void FixedUpdate()
    {
        Vector3 desired_position = target.position + offset;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smoothSpeed);
        transform.position = smoothed_position;

        //transform.LookAt(target);
    }
    #endregion
}
