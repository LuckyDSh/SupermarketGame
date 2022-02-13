/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    #region Fields
    [SerializeField] private float rotation_speed;
    [SerializeField] private Transform rotation_point;
    #endregion

    #region Unity Methods
    void FixedUpdate()
    {
        transform.RotateAround(rotation_point.position, new Vector3(0, 1, 0), rotation_speed * Time.fixedDeltaTime);
    }
    #endregion
}
