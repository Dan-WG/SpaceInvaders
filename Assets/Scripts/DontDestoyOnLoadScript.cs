using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoyOnLoadScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static DontDestoyOnLoadScript instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);        
        }

        
    }

 
}
