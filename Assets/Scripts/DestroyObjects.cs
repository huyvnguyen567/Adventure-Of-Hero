using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.5f;
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
    
}
