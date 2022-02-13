/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Storage : MonoBehaviour
{
    #region Fields
    [SerializeField] private ProductSpawner SpawnPoint;
    [SerializeField] private int type;
    [SerializeField] private float time_of_give_away;
    private float time_of_give_away_buffer;
    private bool is_timer_on;
    private Player player;
    private int last_index;
    #endregion

    #region Unity Methods
    void Start()
    {
        is_timer_on = false;
        time_of_give_away_buffer = time_of_give_away;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (is_timer_on)
        {
            if (time_of_give_away_buffer > 0)
            {
                time_of_give_away_buffer -= Time.deltaTime;
            }
            else
            {
                time_of_give_away_buffer = time_of_give_away;
                last_index = SpawnPoint.Products.Count - 1;
                player.PurchaseProductFromStorage(SpawnPoint.Products[last_index], type);
                SpawnPoint.Products.RemoveAt(last_index);
            }
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            is_timer_on = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            is_timer_on = false;
        }
    }
}
