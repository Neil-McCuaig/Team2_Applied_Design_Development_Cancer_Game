using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 1.5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
