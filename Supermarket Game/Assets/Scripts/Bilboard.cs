/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Bilboard : MonoBehaviour
{
    #region Variables
    private Transform cam;
    #endregion

    #region Unity Methods

    private void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }

    #endregion
}
