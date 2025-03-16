using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    private GameObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            Debug.Log("Destroying duplicate instance");
            Destroy(this.gameObject);
        }
    }

}
