using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region Public variables
    public float movementSpeed;
    public float dashSpeed=100f;
    public float dashCooldown=0.5f;
    public float dashResetTimer=0.5f;
    public LayerMask shootLayer;
    #endregion

    private Rigidbody playerRB;

    private Vector2 moveVector;
    private GM gm;
    private bool canDash;
    private MeshCollider sphereCheck;
    private bool dashAgain;


    #region setup
    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GM>();
        sphereCheck = GetComponentInChildren<MeshCollider>();
    }

    void Start()
    {
        gm.HealthUpdate();
        canDash = true;
        sphereCheck.gameObject.SetActive(false);
        gm.TakeDamage(25f) ;
    }

    void Update()
    {
        ReadInputs();
    }
    private void FixedUpdate()
    {
        PlayerMov();
    }

   
    public void OnMovement(InputAction.CallbackContext MoveContext)
    {
        moveVector = MoveContext.ReadValue<Vector2>();
       
    }

    private void ReadInputs()
    {
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        if (kb.spaceKey.wasPressedThisFrame)
        {
          if (canDash == true)   Dash();
        }
        Mouse m1 = InputSystem.GetDevice<Mouse>();
        if (m1.leftButton.wasPressedThisFrame) Shoot();
        

    }
    #endregion

    #region Dash
    private void Dash()
    {
        Debug.Log("Dash");
        //playerRB.AddRelativeForce(new Vector3(0, 0, 100), ForceMode.Impulse);
        playerRB.AddForce(new Vector3(moveVector.x*100,0,moveVector.y*100), ForceMode.Impulse);

        StartCoroutine("DashCooldown");
        StartCoroutine("DashDuration");
        StartCoroutine("ReDashCheck");
    }

    public IEnumerator DashCooldown()
    {
        canDash = false;
       // sphereCheck.gameObject.SetActive(true);
        yield return new WaitForSeconds(dashCooldown);
       // sphereCheck.gameObject.SetActive(false);
        canDash = true;

        //Need to set this stuff true later

    }
    public IEnumerator ReDashCheck()
    {
        yield return new WaitForSeconds(0.1f);
        sphereCheck.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        sphereCheck.gameObject.SetActive(false);


    }
    private IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(dashResetTimer);
        playerRB.velocity = new Vector3(0, 0, 0);
    }

    public void ReDash()
    {
        sphereCheck.gameObject.SetActive(false);
        StopCoroutine("DashCooldown");
        StartCoroutine("ReDashAuto");
        canDash = false;

    }

    private IEnumerator ReDashAuto ()
    {
        float baseMS = movementSpeed;
        movementSpeed = 0f;
        yield return new WaitForSeconds(0.5f);
        Dash();
        movementSpeed = baseMS;
    }

    #endregion
    private void PlayerMov()
    {
        //  xInput = Input.GetAxis("Horizontal");
        // zInput = Input.GetAxis("Vertical");

       // Debug.Log(xzMove);

        if (playerRB.velocity.magnitude <= 10)
        {
            playerRB.AddForce(new Vector3(moveVector.x * movementSpeed, 0, moveVector.y * movementSpeed) * Time.fixedDeltaTime, ForceMode.Force) ;
            // playerRB.velocity = new Vector3(xzMove.x * movementSpeed, 0, xzMove.y * movementSpeed)*Time.fixedDeltaTime;
           
        }


        if (moveVector == Vector2.zero)
        {
          //  Debug.Log("Standing still");
          //  playerRB.velocity = Vector3.Lerp(playerRB.velocity, new Vector3(0, playerRB.velocity.y, 0), 5f);
        }

     

    }

    #region Shooting

    private void Shoot()
    {
        RaycastHit rayInfo;
        // bool rayHit;
        //rayHit = Physics.Raycast(this.transform.position, this.transform.rotation.eulerAngles, 10f);


        Physics.Raycast(this.transform.position, transform.forward, out rayInfo, 20f, shootLayer);
      
        if (rayInfo.collider)
        {
            if (rayInfo.collider.gameObject.CompareTag("Enemy"))
            {
                rayInfo.collider.gameObject.SetActive(false);
                Debug.Log("Okeh");
            }
        }
        

        Debug.Log(rayInfo.collider);
       

        //if (rayHit) Debug.Log("Ree");
        Debug.DrawRay(this.transform.position, transform.forward, Color.blue, 100f);
    }
    #endregion
}
