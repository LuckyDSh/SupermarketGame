/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class IgnoreRotation : MonoBehaviour
{
    private Vector3 InitialRotation;

    private void Start()
    {
        InitialRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(InitialRotation);
    }
}
