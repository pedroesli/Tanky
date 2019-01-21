using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float life = 100;

    void Update()
    {
        if (life <= 0)
        {
            // DEAD X_X
            Destroy(gameObject);
        }
    }
}
