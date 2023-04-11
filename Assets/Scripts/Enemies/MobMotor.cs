using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMotor : MonoBehaviour
{
    public float enemySpeed;
    private Transform enemyTransform;
    void Start()
    {
        enemyTransform = transform; 
    }

    // Update is called once per frame
   

    public void MoveLeft()
    {
        enemyTransform.position += Vector3.left * enemySpeed;
    }
    public void MoveRight()
    {
        enemyTransform.position += Vector3.right * enemySpeed;
    }

    public void MoveDown()
    {
        enemyTransform.position += Vector3.down * enemySpeed;
    }

    public void MoveUp()
    {
        enemyTransform.position += Vector3.up * enemySpeed;
    }
}
