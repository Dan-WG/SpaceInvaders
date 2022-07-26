using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y > 4.56)
        {
            Destroy(gameObject);
            Debug.Log("Bullet is gone");
        }
    }


}
