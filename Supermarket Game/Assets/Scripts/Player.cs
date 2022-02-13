/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Fields
    [SerializeField] private Box box;
    public List<Product> Products_type1;
    public List<Product> Products_type2;
    public List<Product> Products_type3;

    private Product product_buffer;
    private int COUNTER;

    [SerializeField] private GameObject GO_UI;
    [SerializeField] private Text player_balance_txt;
    [SerializeField] private Text animation_txt;
    public static bool is_game_over;
    [SerializeField] private float margin;
    public static float MARGIN_AMOUNT;
    public static int PLAYER_BALANCE;
    public static bool is_balance_modified;

    [Header("Release Timer")]
    [Space]
    [SerializeField] private float time;
    [SerializeField] private float prime;
    [SerializeField] private float time_buffer;
    private bool is_timer_on;
    private bool is_time_to_get_soldier;
    private static int value_buffer;
    #endregion

    #region Unity Methods
    void Start()
    {
        Products_type1 = new List<Product>();
        Products_type2 = new List<Product>();
        Products_type3 = new List<Product>();

        MARGIN_AMOUNT = margin;
        PLAYER_BALANCE = 100;
        player_balance_txt.text = PLAYER_BALANCE.ToString();

        is_game_over = false;
        is_timer_on = false;
        is_time_to_get_soldier = false;
        time_buffer = time;

        GO_UI.SetActive(false);
        animation_txt.gameObject.SetActive(false);
    }

    private void Update()
    {
        #region Release With Timer
        //if (is_timer_on)
        //{
        //    if (time_buffer > 0)
        //    {
        //        time_buffer -= Time.deltaTime * prime;
        //    }
        //    if (time_buffer <= 0)
        //    {
        //        is_time_to_get_soldier = true;
        //        ResetTimer();
        //    }
        //}
        #endregion

        if (is_balance_modified)
        {
            // Apply changes to UI 
            // Start animation
            player_balance_txt.text = PLAYER_BALANCE.ToString();
            Animation_txt(value_buffer);
            is_balance_modified = false;
        }

        if (is_game_over)
            GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"/* || other.tag == "BOSS"*/)
        {
            // GameOver();
            Destroy(other.gameObject);
        }
    }

    #region Release With Timer
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Release")
    //    {
    //        is_timer_on = true;

    //        #region Get Soldier
    //        if (is_time_to_get_soldier)
    //        {
    //            other.transform.parent.TryGetComponent<Tower>(out tower_buffer);

    //            if (tower_buffer != null)
    //                tower_buffer.ReleaseWorker("Element");
    //            else
    //            {
    //                other.transform.parent.TryGetComponent<Bunker>(out bunker_buffer);

    //                if (bunker_buffer != null)
    //                    bunker_buffer.Release("Element");
    //            }

    //            is_time_to_get_soldier = false;
    //        }
    //        #endregion
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Release")
    //        ResetTimer();
    //}
    #endregion
    #endregion

    #region Release With Timer
    //private void ResetTimer()
    //{
    //    is_timer_on = false;
    //    time_buffer = time;
    //}
    #endregion

    public void ModifyMargin()
    {
        MARGIN_AMOUNT = margin;
    }

    public static void ModifyBalance(int value)
    {
        PLAYER_BALANCE += value;
        value_buffer = value;

        if (PLAYER_BALANCE < 0)
            PLAYER_BALANCE = 0;

        // play animation 
        // modify text UI
        is_balance_modified = true;
    }

    private IEnumerable Animation_txt(int value)
    {
        animation_txt.text = "+" + value.ToString();
        animation_txt.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        animation_txt.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(GameOver_enum());
    }

    private IEnumerator GameOver_enum()
    {
        yield return new WaitForSeconds(1f);
        GO_UI.SetActive(true);
    }

    public void PurchaseProductFromStorage(Product product, int type)
    {
        product.transform.position = box.BoxPoint.position;
        product.transform.parent = box.transform;

        // For now we use Size-Proportion x - 0.5 | y - 0.3 | z - 0.5
        product.transform.localScale =
                     new Vector3(product.transform.localScale.x * 0.5f,
                     product.transform.localScale.y * 0.3f,
                     product.transform.localScale.z * 0.5f);

        //product.rb.constraints = RigidbodyConstraints.FreezeRotation;

        Debug.Log("Type " + type.ToString() + " is purchased");

        if (type == 1)
            Products_type1.Add(product);
        else if (type == 2)
            Products_type2.Add(product);
        else if (type == 3)
            Products_type3.Add(product);
    }

    public Product ReleaseProductFromPlayer(int type)
    {
        // We should release a Right type Product to Right type Collection in Shelf

        if (type == 1)
        {
            COUNTER = Products_type1.Count;

            if (COUNTER > 0)
            {
                product_buffer = Products_type1[COUNTER - 1];

                // For now we use Size-Proportion x - 0.7 | y - 0.3 | z - 0.7
                //product_buffer.transform.localScale =
                //    new Vector3(product_buffer.transform.localScale.x / 0.5f,
                //    product_buffer.transform.localScale.y / 0.3f,
                //    product_buffer.transform.localScale.z / 0.5f);

                product_buffer.transform.SetParent(null);
                product_buffer.transform.localScale = product_buffer.default_scale;
                product_buffer.rb.constraints = RigidbodyConstraints.FreezeAll;
                product_buffer.transform.rotation = Quaternion.Euler(Vector3.zero);
                Products_type1.Remove(product_buffer);

                return product_buffer;
            }
        }
        else if (type == 2)
        {
            COUNTER = Products_type2.Count;

            if (COUNTER > 0)
            {
                product_buffer = Products_type2[COUNTER - 1];

                // For now we use Size-Proportion x - 0.7 | y - 0.3 | z - 0.7
                //product_buffer.transform.localScale =
                //    new Vector3(product_buffer.transform.localScale.x / 0.5f,
                //    product_buffer.transform.localScale.y / 0.3f,
                //    product_buffer.transform.localScale.z / 0.5f);

                product_buffer.transform.SetParent(null);
                product_buffer.transform.localScale = product_buffer.default_scale;
                product_buffer.transform.rotation = Quaternion.Euler(Vector3.zero);
                product_buffer.rb.constraints = RigidbodyConstraints.FreezeAll;
                Products_type2.Remove(product_buffer);

                return product_buffer;
            }
        }
        else if (type == 3)
        {
            COUNTER = Products_type3.Count;

            if (COUNTER > 0)
            {
                product_buffer = Products_type3[COUNTER - 1];

                // For now we use Size-Proportion x - 0.7 | y - 0.3 | z - 0.7
                //product_buffer.transform.localScale =
                //    new Vector3(product_buffer.transform.localScale.x / 0.5f,
                //    product_buffer.transform.localScale.y / 0.3f,
                //    product_buffer.transform.localScale.z / 0.5f);

                product_buffer.transform.SetParent(null);
                product_buffer.transform.localScale = product_buffer.default_scale;
                product_buffer.transform.rotation = Quaternion.Euler(Vector3.zero);
                product_buffer.rb.constraints = RigidbodyConstraints.FreezeAll;
                Products_type3.Remove(product_buffer);

                return product_buffer;
            }
        }

        // Default state:
        // When Product is not found
        return null;
    }
}
