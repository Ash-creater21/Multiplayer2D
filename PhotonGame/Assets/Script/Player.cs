using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class Player : Photon.MonoBehaviour
{
    public new PhotonView photonView ; 
    private Rigidbody2D rb ; 
    public GameObject PlayerCamera ; 
    // public Text PlayerName ;

    // to detect the ground 
    [SerializeField] private bool IsGrounded ; 
    [SerializeField] private Transform groundCheck ; 
    [SerializeField] private float checkRadius ; 
    [SerializeField] private LayerMask GroundLayer ; 
    [SerializeField] private Text playerNameText ; 
    // animation 

    [SerializeField] private Animator anim ; 

    
    public float MoveSpeed ; 
    public float jumpForce; 
    private float MoveInput ; 

    private bool facingRight = true ; 
    private bool isjumping = false ; 
    
    private Transform firepoint ; 
    private GameObject bulletprefab ; 
    private void Start() 
    {
        // anim = GetComponent<Animator>() ; 
        rb = GetComponent<Rigidbody2D>(); 
        
    }
    private void Awake() 
    {
        // if the player is local player 
        if(photonView.isMine)
        {
            PlayerCamera.SetActive(true); 
            playerNameText.text = PhotonNetwork.playerName ;  
        }
    else 
     {
        
         enabled = false;
         playerNameText.text = photonView.owner.name; 
         playerNameText.color = Color.white ; 
     }
    }
    private void CheckInput() 
    {
        MoveInput = Input.GetAxis("Horizontal") ; 
        if(MoveInput!=0)
        {
            anim.SetBool("isrunning",true);
        }
        else 
        {
            anim.SetBool("isrunning",false);
        }
    }
    private void ApplyMovement()
    {
        rb.velocity = new Vector2(MoveInput*MoveSpeed,rb.velocity.y);
        if(facingRight!=true)
        {
            if(MoveInput>=0.2)
            {
                photonView.RPC("flip",PhotonTargets.AllBuffered);
                // flip(); 
                facingRight=true; 
            }
        }
        if(facingRight)
        {
            if(MoveInput<=-0.2)
            {
                // flip() ; 
                photonView.RPC("flip",PhotonTargets.AllBuffered);
            }
        }
        
        
    }
    private void Update() 
    {
        CheckInput(); 
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position,checkRadius,GroundLayer) ; 
       
         if(IsGrounded==true)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = Vector2.up * jumpForce ; 
                anim.SetBool("isjumping",true) ; 
            }
            anim.SetBool("isjumping",false) ; 
        }
        
       
    }
    private void FixedUpdate()
    {
        ApplyMovement() ; 

    }

    [PunRPC]
    private void flip()
    {
        facingRight = !facingRight ; 
        Vector3 Scaler = transform.localScale; 
        Scaler.x *=-1 ; // scale = 0.8 => 0.8*(-1) = -0.8 
        transform.localScale = Scaler ; 
        
    }

    private void Shoot() 
    {
        GameObject bullet = PhotonNetwork.Instantiate(bulletprefab.name,firepoint.position,Quaternion.identity,0); 
    }
    

}
