using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 360f;

    // Update is called once per frame
    void Update()
    {
        //spin on the Z for 2d
        transform.Rotate(0,0, speedOfSpin * Time.deltaTime);
    }
}
