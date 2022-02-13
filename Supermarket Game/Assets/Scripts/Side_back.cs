/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Side_back : MonoBehaviour
{
    #region Fields
    private Collider this_collider;
    #endregion

    #region Unity Methods
    void Start()
    {
        this_collider = GetComponent<Collider>();
        this_collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this_collider.isTrigger = false;
        }

        if(other.tag == "Enemy")
        {
            this_collider.isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            this_collider.isTrigger = true;
        }
    }
    #endregion
}
