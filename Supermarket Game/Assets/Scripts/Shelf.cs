/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject Products_type1;
    [SerializeField] private GameObject Products_type2;
    [SerializeField] private GameObject Products_type3;

    [SerializeField] private List<Ppoint> Points_for_type1;
    [SerializeField] private List<Ppoint> Points_for_type2;
    [SerializeField] private List<Ppoint> Points_for_type3;

    [Header("Products List | For Development")]
    [Space]
    public List<GameObject> Products_type1_list;
    public List<GameObject> Products_type2_list;
    public List<GameObject> Products_type3_list;
    public int price_for_type1;
    public int price_for_type2;
    public int price_for_type3;
    private int COUNTER;
    private GameObject product_buffer;

    public static int MINIMUM_BALANCE;
    private int index_buffer;
    #endregion

    #region Unity Methods
    void Start()
    {
        #region Products Inicialisation

        foreach (var point in Points_for_type1)
        {
            Products_type1_list.Add(Instantiate(Products_type1, point.transform.position, Quaternion.identity));
            point.is_free = false;
            point.product_attached = Products_type1_list[Products_type1_list.Count - 1];
        }

        foreach (var point in Points_for_type2)
        {
            Products_type2_list.Add(Instantiate(Products_type2, point.transform.position, Quaternion.identity));
            point.is_free = false;
            point.product_attached = Products_type2_list[Products_type2_list.Count - 1];
        }

        foreach (var point in Points_for_type3)
        {
            Products_type3_list.Add(Instantiate(Products_type3, point.transform.position, Quaternion.identity));
            point.is_free = false;
            point.product_attached = Products_type3_list[Products_type3_list.Count - 1];
        }

        #endregion

        price_for_type1 = Products_type1_list[0].GetComponent<Product>().price;
        price_for_type2 = Products_type2_list[0].GetComponent<Product>().price;
        price_for_type3 = Products_type3_list[0].GetComponent<Product>().price;

        MINIMUM_BALANCE = Mathf.Max(new int[] { price_for_type1, price_for_type2, price_for_type3 });
    }
    #endregion

    #region PlaceProductOnShelf
    public void PlaceProductOnShelf(Product product, int type)
    {
        if (product == null)
            return;

        if (type == 1)
        {
            foreach (var point in Points_for_type1)
            {
                if (point.is_free)
                {
                    product.transform.position = point.transform.position;
                    Products_type1_list.Add(product.gameObject);

                    point.is_free = false;
                    point.product_attached = product.gameObject;
                    return;
                }
            }

            return; // For optimization purposes
        }
        else if (type == 2)
        {
            foreach (var point in Points_for_type2)
            {
                if (point.is_free)
                {
                    product.transform.position = point.transform.position;
                    Products_type2_list.Add(product.gameObject);

                    point.is_free = false;
                    point.product_attached = product.gameObject;
                    return;
                }
            }

            return; // For optimization purposes
        }
        else if (type == 3)
        {
            foreach (var point in Points_for_type3)
            {
                if (point.is_free)
                {
                    product.transform.position = point.transform.position;
                    Products_type3_list.Add(product.gameObject);

                    point.is_free = false;
                    point.product_attached = product.gameObject;
                    return;
                }
            }

            return; // For optimization purposes
        }
    }
    #endregion

    #region ReleaseProduct
    public void ReleaseProduct(Customer customer, int type)
    {
        if (type == 1)
        {
            COUNTER = Products_type1_list.Count;
            if (COUNTER > 0)
            {
                if (customer.balance >= price_for_type1)
                {
                    index_buffer = Random.Range(0, COUNTER - 1);
                    product_buffer = Products_type1_list[index_buffer];

                    Points_for_type1[Points_for_type1.FindIndex(p => p.product_attached == product_buffer)].is_free = true;

                    Products_type1_list.RemoveAt(index_buffer);
                    customer.balance -= price_for_type1;
                    customer.AddProduct(product_buffer.GetComponent<Product>());
                }

                return;
            }
            else
            {
                // If Shelf of product type is empty 
                // Start Destroying the Shelf

            }
        }
        else if (type == 2)
        {
            COUNTER = Products_type2_list.Count;
            if (COUNTER > 0)
            {
                if (customer.balance >= price_for_type2)
                {
                    index_buffer = Random.Range(0, COUNTER - 1);
                    product_buffer = Products_type2_list[index_buffer];
                    Points_for_type2[Points_for_type2.FindIndex(p => p.product_attached == product_buffer)].is_free = true;

                    Products_type2_list.RemoveAt(index_buffer);
                    customer.balance -= price_for_type2;
                    customer.AddProduct(product_buffer.GetComponent<Product>());
                }

                return;
            }
            else
            {
                // If Shelf of product type is empty 
                // Start Destroying the Shelf
            }
        }
        else if (type == 3)
        {
            COUNTER = Products_type3_list.Count;
            if (COUNTER > 0)
            {
                if (customer.balance >= price_for_type3)
                {
                    index_buffer = Random.Range(0, COUNTER - 1);
                    product_buffer = Products_type3_list[index_buffer];
                    Points_for_type3[Points_for_type3.FindIndex(p => p.product_attached == product_buffer)].is_free = true;

                    Products_type3_list.RemoveAt(index_buffer);
                    customer.balance -= price_for_type3;
                    customer.AddProduct(product_buffer.GetComponent<Product>());
                }

                return;
            }
            else
            {
                // If Shelf of product type is empty 
                // Start Destroying the Shelf
            }
        }
        else
        {
            type = 1;
            ReleaseProduct(customer, type);
        }
    }
    #endregion 
}
