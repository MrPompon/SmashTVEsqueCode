using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controller : Controller
{
    public enum ControllMode
    {
        TOPDOWN,
        SIDESCROLL,
    }
    public enum ControllModeTopDown
    {
        Keyboard, 
        MouseKeyboard,
    }
    public System.Action OnAnyInput;

    public Weapon gun;
    public Weapon kickGun;
    public Weapon testWeapon;

    public System.Action<float> OnScaleX;
    public System.Action<bool> OnCrouch;
    public string strafeInput;
    
    public ControllMode controllMode = ControllMode.TOPDOWN;
    public ControllModeTopDown controllModeTopDown =ControllModeTopDown.MouseKeyboard;
    [SerializeField]private float rotationAmount = 90f;
    [SerializeField]private float rotationCooldown = 0.5f;
    private float rotationProgress = 0;
    private bool isRotating = false;
    private bool isCrouching = false;

    private Vector2 input;
    private float strafe;
    //more freeform movement
    //rotamount 22.5
    //rotcooldown 0.125

    //other setting 
    //rotamount 90
    //rotcooldown 0.25
    void Start()
    {
        GameObject spawnLocation = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (spawnLocation != null)
        {
            rigid2D.gameObject.transform.position= spawnLocation.transform.position;
        }
        gun.OnFire += HandleGunFire;
        if(kickGun!=null)
        kickGun.OnFire += HandleKick;

        if (testWeapon != null)
            testWeapon.OnFire += HandleTestWeapon;

        if(statHandler!=null)
        statHandler.OnDeath += OnDeath;
    }
    private void OnDestroy()
    {
        gun.OnFire -= HandleGunFire;
        if (kickGun != null)
            kickGun.OnFire -= HandleKick;

        if (testWeapon != null)
            testWeapon.OnFire -= HandleTestWeapon;

        if (statHandler != null)
            statHandler.OnDeath -= OnDeath;
    }
    //private void Update()
    //{
        
    //}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            controllModeTopDown++;
            if((int)controllModeTopDown>= System.Enum.GetValues(typeof(ControllModeTopDown)).Length)
            {
                controllModeTopDown = 0;
            }
        }
        HandleRotation();
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

      
        if (controllMode == ControllMode.SIDESCROLL)
        {
            HandleInputSideScroll(input);
        }
        else if(controllMode == ControllMode.TOPDOWN)
        {
            float strafe = 0;
            switch (controllModeTopDown)
            {
                case ControllModeTopDown.Keyboard:
                    strafe= Input.GetAxis(strafeInput);
                    HandleInputTopDown(input, strafe);
                    break;
                case ControllModeTopDown.MouseKeyboard:
                    strafe = input.x;
                    HandleInputMouseKeyboard(input, strafe);
                    break;
            }

           
        }
    }
    void HandleGunFire()
    {
        animator.SetTrigger("Fire");
    }
    void HandleKick()
    {
        animator.SetTrigger("Kick");
    }
    void HandleTestWeapon()
    {
        animator.SetTrigger("Fire");
    }
    void HandleRotation()
    {
        if (!isRotating)
        {
            return;
        }
        rotationProgress += Time.deltaTime;
        if(rotationProgress>= rotationCooldown)
        {
            rotationProgress = 0;
            isRotating = false;
        }
    }
    void HandleInputSideScroll(Vector2 input)
    {
        if (rigid2D.transform.localScale.x == -1)
        {
            input.x *= -1; // invert the input since player is rotated
        }
        if (input != Vector2.zero)
        {
            OnAnyInput?.Invoke();
            if (!isRotating && Mathf.Abs(input.y) < 0.1f && input.x != 0)
            {
                animator.SetBool("Moving", false);
                if (input.x > 0)
                {
                    if (!isCrouching)
                    {
                    animator.SetBool("Crouching", true);
                    isCrouching = true;
                    OnCrouch?.Invoke(true);
                    }
                }
                else if (input.x < 0)
                {
                    ScaleX();
                    if (isCrouching)
                    {
                    animator.SetBool("Crouching", false);
                    isCrouching = false;
                    OnCrouch?.Invoke(false);
                    }
                }
            
                
            }
            else if (input.y != 0 && !isCrouching)
            {
                animator.SetBool("Moving", true);
                float movementSpeed = statHandler.baseStats.movementSpeed;
                if (input.y < 0) //half backwards speed 
                {
                    movementSpeed /= 2;
                }
                rigid2D.MovePosition(new Vector2(rigid2D.position.x, rigid2D.position.y) + (Vector2)rigid2D.transform.right *(rigid2D.transform.localScale.x* input.y) * movementSpeed);
            }
        }
        else
        {
            if (isCrouching)
            {
            isCrouching = false;
            animator.SetBool("Crouching", false);
            OnCrouch?.Invoke(false);
            }
            animator.SetBool("Moving", false);
        }
        if (!isRotating && Mathf.Abs(input.y) < 0.1f && input.x < 0)
        {
            if (isCrouching)
            {
                animator.SetBool("Crouching", false);
                isCrouching = false;
                OnCrouch?.Invoke(false);
            }
        }
    }
    void HandleInputTopDown(Vector2 input, float strafe)
    {
        if (input != Vector2.zero || strafe!=0)
        {
            OnAnyInput?.Invoke();
            if (!isRotating && Mathf.Abs(input.y) < 0.1f && input.x != 0) //handle rotation
            {
                if (input.x < -0.2f)
                {
                    RotateLeft();
                }
                else if (input.x > 0.2f)
                {
                    RotateRight();
                }
            }
            else if (input.y != 0) // handle forward/back
            {
                animator.SetBool("Moving", true);
                float movementSpeed = statHandler.baseStats.movementSpeed;
                if(input.y < 0) //half backwards speed 
                {
                    movementSpeed /= 2;
                }
                //rigid2D.MovePosition(new Vector2(rigid2D.position.x, rigid2D.position.y) + (Vector2)rigid2D.transform.up * -input.y * movementSpeed);
                rigid2D.AddForce((Vector2)rigid2D.transform.up * -input.y * movementSpeed);
            }
            else if (strafe != 0) //handle strafing
            {
                animator.SetBool("Moving", true);
                float movementSpeed = statHandler.baseStats.movementSpeed/2;
        
                //rigid2D.MovePosition(new Vector2(rigid2D.position.x, rigid2D.position.y) + (Vector2)rigid2D.transform.up * -input.y * movementSpeed);
                rigid2D.AddForce((Vector2)rigid2D.transform.right * strafe * movementSpeed);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    void HandleInputMouseKeyboard(Vector2 input, float strafe)
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));

        //main.Rotate(Vector3.forward, -rotationAmount);
        Vector3 dir = mousepos - main.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90;
        main.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        isRotating = true;
        //if (input != Vector2.zero)
        //{
        //    OnAnyInput?.Invoke();
        //    if (!isRotating && Mathf.Abs(input.y) < 0.1f && input.x != 0) //handle rotation
        //    {
        //        if (input.x < -0.2f)
        //        {
        //            RotateLeft();
        //        }
        //        else if (input.x > 0.2f)
        //        {
        //            RotateRight();
        //        }
        //    }
        if (input.y != 0) // handle forward/back
        {
            animator.SetBool("Moving", true);
            float movementSpeed = statHandler.baseStats.movementSpeed;
            if (input.y < 0) //half backwards speed 
            {
                movementSpeed /= 2;
            }
            //rigid2D.MovePosition(new Vector2(rigid2D.position.x, rigid2D.position.y) + (Vector2)rigid2D.transform.up * -input.y * movementSpeed);
            rigid2D.AddForce((Vector2)rigid2D.transform.up * -input.y * movementSpeed);
        }
        else if (strafe != 0) //handle strafing
        {
            animator.SetBool("Moving", true);
            float movementSpeed = statHandler.baseStats.movementSpeed / 2;

            //rigid2D.MovePosition(new Vector2(rigid2D.position.x, rigid2D.position.y) + (Vector2)rigid2D.transform.up * -input.y * movementSpeed);
            rigid2D.AddForce((Vector2)rigid2D.transform.right * strafe * movementSpeed);
        }
    
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void ScaleX()
    {
        rigid2D.transform.localScale = new Vector3(rigid2D.transform.localScale.x * -1, rigid2D.transform.localScale.y,rigid2D.transform.localScale.z);
        isRotating = true;
        OnScaleX?.Invoke(rigid2D.transform.localScale.x);
    }
    void RotateLeft()
    {
        main.Rotate(Vector3.forward, rotationAmount);
        isRotating = true;
        //rigid2D.MoveRotation(90);
    }
    void RotateRight()
    {
        main.Rotate(Vector3.forward, -rotationAmount);
        isRotating = true;
    }
    void OnDeath()
    {
        GameManager.gameManager.StartCoroutine(LoadMenu(1.5f));
    }
    IEnumerator LoadMenu(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GameManager.gameManager.GetComponent<Game_SceneManager>().LoadMenu();  
    }
}
