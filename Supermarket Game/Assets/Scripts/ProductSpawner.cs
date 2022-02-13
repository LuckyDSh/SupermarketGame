/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject product_to_spawn;
    [SerializeField] private int number_of_products;
    public List<Product> Products;

    [HideInInspector] public Product product_buffer;
    #endregion

    #region Unity Methods
    void Start()
    {
        StartCoroutine(InitializeProducts());
    }

    void FixedUpdate()
    {
        CheckForCapacityOverTime();
    }
    #endregion

    private IEnumerator CheckForCapacityOverTime()
    {
        if (Products.Count <= 0)
        {
            StartCoroutine(InitializeProducts());
        }

        yield return new WaitForSeconds(2f);
    }

    private IEnumerator InitializeProducts()
    {
        Products = new List<Product>();

        for (int i = 0; i < number_of_products; i++)
        {
            Products.Add(Instantiate(product_to_spawn, transform.position, transform.rotation).GetComponent<Product>());
        }

        yield return new WaitForSeconds(2f);

        foreach (var item in Products)
        {
            item.rb.constraints = RigidbodyConstraints.None;
        }
    }
}
