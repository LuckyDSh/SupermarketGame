/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class ClientPoint : MonoBehaviour
{
    [Range(1, 3)] public int type;
    public Shelf shelf_refference;
    public bool is_occupied;

    private void Start()
    {
        is_occupied = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            shelf_refference.PlaceProductOnShelf(other.GetComponent<Player>().ReleaseProductFromPlayer(type), type);
        }
    }
}
