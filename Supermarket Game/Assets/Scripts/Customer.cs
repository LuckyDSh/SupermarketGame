/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    #region Fields
    [SerializeField] private Box box;
    [SerializeField] public int balance;
    [SerializeField] private ClientPoint[] ClientPoints;
    [SerializeField] private GameObject[] BuyAreaPoints;

    [SerializeField] private GameObject angry_UI;

    public List<Product> Products;
    private NavMeshAgent agent;
    private int random;
    private int type_buffer;

    [Header("For Development...")]
    [Space]
    [SerializeField] private ClientPoint current_client_point_reference;
    #endregion

    #region Unity Methods
    void Start()
    {
        angry_UI.SetActive(false);
        box.gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        MoveToShelf();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shelf_active_area")
        {
            if (!current_client_point_reference.is_occupied)
            {
                // if box is empty and shelf point 
                // get the product
                // dicrease Customer balance
                //while (balance > Shelf.MINIMUM_BALANCE)
                //{

                // Local Problem
                current_client_point_reference.shelf_refference.ReleaseProduct(this, type_buffer);
                current_client_point_reference.is_occupied = true;
                //}

                // When we are done go to Buy Area
                MoveToBuyArea();
                current_client_point_reference.is_occupied = false;
            }
            else
            {
                StartCoroutine(CustomerIsAngry());
            }

        }
        else if (other.tag == "buy_active_area")
        {
            // if the box is not empty and buy_area
            // release products and increase Player balance
            foreach (var item in Products)
            {
                Player.ModifyBalance(Mathf.CeilToInt(item.price * Player.MARGIN_AMOUNT));
                Destroy(item.gameObject);
                DisableCustomer();
            }
        }
    }

    public IEnumerator CustomerIsAngry()
    {
        angry_UI.SetActive(true);

        yield return new WaitForSeconds(1f); // transition time

        AudioManager.instance.Play("AngryCustomer");
    }

    private void DisableCustomer()
    {
        box.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void MoveToShelf()
    {
        random = Random.Range(0, ClientPoints.Length);
        type_buffer = ClientPoints[random].type;
        current_client_point_reference = ClientPoints[random];
        agent.SetDestination(current_client_point_reference.transform.position);
    }

    public void MoveToBuyArea()
    {
        random = Random.Range(0, BuyAreaPoints.Length);
        agent.SetDestination(BuyAreaPoints[random].transform.position);
    }

    public void AddProduct(Product product)
    {
        // Dicreasing Balance
        // Check for balance
        // Check for Shelf Capacity
        // All Operations done in Shelf Script -> ReleaseProduct()

        // Activate box
        // Place the product in the customer box
        // Scale the Product 
        box.gameObject.SetActive(true);
        product.transform.position = box.BoxPoint.position;
        product.transform.parent = box.transform;
        product.transform.localScale = product.refference_scale;
        product.rb.constraints = RigidbodyConstraints.FreezeRotation;
        Products.Add(product);
    }
}
