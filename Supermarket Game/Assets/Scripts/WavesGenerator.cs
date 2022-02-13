/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField] private List<GameObject> Waves;

    [Header("Waves Timer")]
    [Space]
    [SerializeField] private float wave_prime;
    [SerializeField] private float wave_time;
    [SerializeField] private Slider wave_timer_slider;
    [SerializeField] private Gradient wave_gradient;
    [SerializeField] private Image wave_fill;
    public static bool wave_isOn;
    private int COUNTER;
    private GameObject buffer;

    //[Header("Boss Timer and Boss Spawn")]
    //[Space]
    //[SerializeField] private GameObject BOSS;
    //[SerializeField] private GameObject BOSS_EFFECTS;
    //[SerializeField] private Transform main_camera;
    //[SerializeField] private Transform camera_parent;
    //[SerializeField] private Transform camera_position;
    //[SerializeField] private Transform CAMERA_POSITION_FOR_BOSS_SPAWN;
    //[SerializeField] private float cam_speed;
    [SerializeField] private float boss_prime;

    // We move Camera by Z 

    private Vector3 main_camera_initial_position;
    [SerializeField] private bool is_camera_moving;
    private Vector3 cam_target_buffer;

    // Depends on number of waves: boss_time = waves_number * waves_time + 30f
    [SerializeField] private float boss_time;

    [SerializeField] private Slider boss_timer_slider;
    [SerializeField] private Gradient boss_gradient;
    [SerializeField] private Image boss_fill;
    [SerializeField] private GameObject Boss_Spawned_UI;
    private float boss_time_buffer;
    public static bool boss_isOn;

    // used in: Bunker(28) | Barricade(33) | Weapon(79)
    public static bool BOSS_IS_SPAWNED;

    // Subscribe in Soldier (optional)
    public delegate void CrowdHome();
    public static event CrowdHome home;

    // Subscribe in Weapon
    public delegate void ActivateWeapon();
    public static event ActivateWeapon weapon;
    #endregion

    #region Unity Methods
    void Start()
    {
        //main_camera_initial_position = main_camera.position;
        is_camera_moving = false;

        wave_isOn = false;
        boss_isOn = false;
        BOSS_IS_SPAWNED = false;
        // Waves Timer goes from Max to Min
        wave_timer_slider.maxValue = wave_time;
        wave_timer_slider.value = wave_time;
        wave_fill.color = wave_gradient.Evaluate(1f);

        // Boss Timer goes from Min to Max
        boss_timer_slider.maxValue = boss_time;
        boss_timer_slider.value = 0f;
        boss_fill.color = boss_gradient.Evaluate(0f);
        boss_time_buffer = 0f;
        //BOSS.SetActive(false);
        Boss_Spawned_UI.SetActive(false);

        foreach (var item in Waves)
        {
            item.SetActive(false);
        }

        COUNTER = Waves.Count;
        buffer = Waves[COUNTER - 1];
        buffer.SetActive(true);
        Waves.Remove(buffer);
    }

    void Update()
    {
        //if (!BOSS_IS_SPAWNED)
        //    StartCoroutine(ActivateEnemies());

        if (wave_isOn)
            WAVE_TIMER();

        if (boss_isOn)
            BOSS_TIMER();
    }

    //private void FixedUpdate()
    //{
    //    if (is_camera_moving)
    //    {
    //        MoveCameraToTargetPosition(cam_target_buffer);
    //    }
    //}
    #endregion

    private void WAVE_TIMER()
    {
        #region WAVE TIMER
        if (wave_time > 0)
        {
            wave_time -= Time.deltaTime * wave_prime;
        }

        wave_timer_slider.value = wave_time;
        wave_fill.color = wave_gradient.Evaluate(wave_timer_slider.normalizedValue);//
        if (wave_timer_slider.value <= 0f)
        {
            wave_timer_slider.value = wave_timer_slider.maxValue;
            wave_time = wave_timer_slider.maxValue;
            wave_fill.color = wave_gradient.Evaluate(1f);
            COUNTER = Waves.Count;

            if (COUNTER <= 0)
            {
                wave_isOn = false;
                return;
            }

            // When Timer is on max Value -> Generate Wave
            buffer = Waves[COUNTER - 1];
            buffer.SetActive(true);
            Waves.Remove(buffer);
        }
        #endregion
    }

    #region BOSS_TIMER

    private void BOSS_TIMER()
    {
    #region BOSS TIMER
        if (boss_time_buffer <= boss_timer_slider.maxValue)
        {
            boss_time_buffer += Time.deltaTime * boss_prime;
        }

        boss_timer_slider.value = boss_time_buffer;
        boss_fill.color = boss_gradient.Evaluate(boss_timer_slider.normalizedValue);

        if (boss_timer_slider.value >= boss_timer_slider.maxValue)
        {
            Debug.Log("Boss Spawned Triggered");

            boss_timer_slider.value = boss_timer_slider.maxValue;
            boss_time = boss_timer_slider.maxValue;
            boss_fill.color = boss_gradient.Evaluate(1f);

            // SPAWN BOSS
            Boss_Spawned_UI.SetActive(true);
            BOSS_IS_SPAWNED = true;
            boss_isOn = false;

            if (home != null)
                home();
            if (weapon != null)
                weapon();

            //StartCoroutine(BOSS_SPAWN_ROUTINE());
        }
    #endregion
    }

    #endregion

    #region BOSS_SPAWN_ROUTINE
#if false
    private IEnumerator BOSS_SPAWN_ROUTINE()
    {
        main_camera.SetParent(null);
        yield return new WaitForSeconds(3f); // transition

        cam_target_buffer = CAMERA_POSITION_FOR_BOSS_SPAWN.position;
        is_camera_moving = true;
        BOSS.SetActive(true);
        //Enemy.is_moving = false;

        yield return new WaitForSeconds(5f); // transition

        BOSS_EFFECTS.SetActive(true);
        AudioManager.instance.Play("Boss_Spawn");
        yield return new WaitForSeconds(3f);

        cam_target_buffer = main_camera_initial_position;
        is_camera_moving = true;

        yield return new WaitForSeconds(3f); // transition

        BOSS_EFFECTS.SetActive(false);
        //Enemy.is_moving = true;

        yield return new WaitForSeconds(2f); // transition

        // Back the Camera to the Player
        main_camera.SetParent(camera_parent);
        main_camera.position = camera_parent.transform.position;
    }
#endif
    #endregion

    #region MoveCameraToTargetPosition
#if false
    private void MoveCameraToTargetPosition(Vector3 target)
    {
        if (Vector3.Distance(target, main_camera.position) >= 0.05f)
            main_camera.position += (target - main_camera.position) * Time.fixedDeltaTime * cam_speed;
        else
            is_camera_moving = false;
    }
#endif
    #endregion

#if false
    private IEnumerator ActivateEnemies()
    {
        yield return new WaitForSeconds(.6f);

        //if (GAME_CONTROLLER.IS_PLAYING)
        //    Enemy.is_moving = true;
    }
#endif

}
