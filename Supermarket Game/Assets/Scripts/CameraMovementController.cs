/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }
    private float XOffset;
    private float ZOffset;

    private bool is_moving;
    private Vector3 vector_buffer;
    //[SerializeField] private float sideWaysSpeed;
    #endregion

    #region UnityMethods
    void Start()
    {
        XOffset = transform.position.x - _target.position.x;
        ZOffset = transform.position.z - _target.position.z;
    }

    void Update()
    {
        //transform.position += new Vector3(_target.position.x + XSpeed,
        //    transform.position.y, _target.position.z + ZSpeed);

        if (is_moving)
            Move(vector_buffer);
    }
    #endregion

    public void Move(Vector3 vector)
    {
        vector = new Vector3(vector.x + XOffset, vector.y, vector.z + ZOffset);

        vector_buffer = vector;
        transform.position = vector - transform.position;

        if (Vector3.Distance(vector, transform.position) <= 0.01f)
            is_moving = false;
    }
}
