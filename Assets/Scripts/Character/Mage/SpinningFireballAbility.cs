using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningFireballAbility : Ability
{
    public float spinSpeed;
    public float spinDir;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, spinDir), spinSpeed * Time.deltaTime);
    }
}
