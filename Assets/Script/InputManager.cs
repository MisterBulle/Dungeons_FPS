using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerControlls_PI playerControll;
    //private PlayerControlls_PI onFoot;
    public PlayerControlls_PI.OnFootActions onFoot;

    //Call function
    private PlayerMotor PM;

    private PlayerLook PL;

    //Gun en public car il est pas dans Player
    public Gun gun;
    public WeaponSwitching weaponSwitching;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerControll = new PlayerControlls_PI();
        //Récupération du onfoot du playerInput de l'input manager
        onFoot = playerControll.OnFoot;
        PM = GetComponent<PlayerMotor>();
        PL = GetComponent<PlayerLook>();
        //gun = GetComponent<Gun>();

        //Event de jump, quand on appuie sur le bouton de jump, on appelle la fonction jump du player motor
        //Event : 3 états : started, performed, canceled
        onFoot.Jump.performed += ctx => PM.Jump();

        //Pour le sprint
        onFoot.Sprint.performed += ctx => PM.Sprint(true);
        onFoot.Sprint.canceled += ctx => PM.Sprint(false);

        //Pour le dash
        onFoot.Dash.performed += ctx => PM.StartDash(onFoot.Movement.ReadValue<Vector2>());

        //WEAPON SWITCHING
        onFoot.WeaponSwitchUp.performed += ctx => weaponSwitching.WeaponSwitchUp_Function();
        onFoot.WeaponSwitchDown.performed += ctx => weaponSwitching.WeaponSwitchDown_Function();

        //WEAPON KEY
        onFoot.Weapon_0.performed += ctx => weaponSwitching.Weapon_Number_Key(0);
        onFoot.Weapon_1.performed += ctx => weaponSwitching.Weapon_Number_Key(1);
        onFoot.Weapon_2.performed += ctx => weaponSwitching.Weapon_Number_Key(2);

        //Reload
        onFoot.Reload.performed += ctx => ReloadActiveWeapon();

        //Throw Grenade
        onFoot.LaunchGrenade.performed += ctx => GetComponent<ThrowGrenade>().ThrowGrenadeFunction();

    }

    // Update is called once per frame
    void Update()
    {
        // Find the currently active weapon by checking which child is active
        Gun activeGun = null;
        
        foreach (Transform weapon in weaponSwitching.transform)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                activeGun = weapon.GetComponent<Gun>();
                break;
            }
        }
        
        // Only shoot if we have a gun and it's active
        if (activeGun != null && onFoot.Shoot.IsPressed())
        {
            activeGun.Shoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PM.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        PL.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private Gun GetActiveGun()
    {
        foreach (Transform weapon in weaponSwitching.transform)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                return weapon.GetComponent<Gun>();
            }
        }
        return null;
    }

    private void ReloadActiveWeapon()
    {

        Gun activeGun = GetActiveGun();
        if(activeGun != null)
        {
            activeGun.StartCoroutine(activeGun.Reload());           
        }

    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
