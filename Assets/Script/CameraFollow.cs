using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    // Update is called once per frame
    void Update() {
        float x = transform.position.x, y = transform.position.y;

        float t = target.transform.position.y;
        if (t < 2.05 && t  > - 2.04) 
            y = t;

        t = target.position.x;
        if (t < 3.073 && t > -3.073)
            x = t;
                                                    
        transform.position = new Vector3(x, y, -1);

    } // Update
}
