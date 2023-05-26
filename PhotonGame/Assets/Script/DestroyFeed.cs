using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFeed : MonoBehaviour
{
    [SerializeField] private float DestroyTime ; 

    private void OnEnable() 
    {
        Destroy(gameObject,DestroyTime);
    }
}
