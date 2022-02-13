/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CameraBorder : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform cam;
    [SerializeField] private float travel_distance;
    [SerializeField] private float smoothSpeed;
    private bool is_moving;
    private bool is_destination_set;

    private Vector3 desired_position_buffer;
    private Vector3 smoothed_position_buffer;

    [Tooltip("Right/Left/Back/Front")]
    [SerializeField] private string side;
    #endregion

    #region Unity Methods
    void Start()
    {
        is_moving = false;
        is_destination_set = false;
        desired_position_buffer = cam.position;

        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        if (is_moving)
        {
            Move(side);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // We set the direction to Z (positive) for Camera to move
            is_moving = true;
            is_destination_set = false;
        }
    }
    #endregion

    #region MoveSides

    private void Move(string side)
    {
        // Movement
        // Move camera towards desired position 
        // Check for distance (if desired position is close -> stop movement)
        // If left/right -> Move X position
        // If front/back -> Move Z position

        //if (!is_destination_set)
        //{
            desired_position_buffer = cam.position;

            if (side == "Front")
            {
                // Move Z Positive

                //desired_position_buffer = new Vector3(cam.position.x, cam.position.y, cam.position.z + travel_distance);
                desired_position_buffer += Vector3.forward * travel_distance;
                is_destination_set = true;
            }
            else if (side == "Back")
            {
                // Move Z Negative

                //desired_position_buffer = new Vector3(cam.position.x, cam.position.y, cam.position.z - travel_distance);
                desired_position_buffer -= Vector3.forward * travel_distance;
                is_destination_set = true;
            }
            else if (side == "Right")
            {
                // Move X Positive

                //desired_position_buffer = new Vector3(cam.position.x + travel_distance, cam.position.y, cam.position.z);
                desired_position_buffer += Vector3.right * travel_distance;
                is_destination_set = true;
            }
            else if (side == "Left")
            {
                // Move X Negative

                //desired_position_buffer = new Vector3(cam.position.x - travel_distance, cam.position.y, cam.position.z);
                desired_position_buffer -= Vector3.right * travel_distance;
                is_destination_set = true;
            }
        //}

        smoothed_position_buffer = Vector3.Lerp(cam.position, desired_position_buffer, smoothSpeed);
        cam.position = smoothed_position_buffer;

        if (Vector3.Distance(cam.position, desired_position_buffer) <= 0.005)
        {
            is_moving = false;
            is_destination_set = false;
        }
    }

    #endregion
}
