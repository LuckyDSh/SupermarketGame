/*
*	TickLuck
*	All rights reserved
*/

using UnityEngine;

public class Product : MonoBehaviour
{
    public string product_name;
    public int price;
    public float size_scale;
    public Vector3 default_scale;
    public Vector3 refference_scale;

    [HideInInspector] public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
