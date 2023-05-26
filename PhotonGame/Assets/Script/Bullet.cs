using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour
{
    private bool movedir ; 
    // this is for left or right direction of bullet 
    [SerializeField] private float moveSpeed ; 

    [SerializeField] private float DestroyTime ; 

    private void Awake() 
    {
        // this is for destroy bullet after some time 
        StartCoroutine("") ; 
    }


    IEnumerator DestroyByTime() 
    {
        yield return new WaitForSeconds(DestroyTime); 
        GetComponent<PhotonView>().RPC("DestroyObject",PhotonTargets.AllBuffered) ; 
    }
    [PunRPC]
    public void DestroyObject() 
    {
        Destroy(this.gameObject) ; 
    }

    private void Update() 
    {
        if(!movedir)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); 
        }
        else 
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
