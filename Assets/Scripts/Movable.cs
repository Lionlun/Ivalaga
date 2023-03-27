using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    Vector3 startDownMovement = new Vector3(0f, -10f, 0f);
    public float ObjectSpeed { get; set; }

    private void Awake()
    {
        
    }


    public virtual IEnumerator EnemyMovementPattern()
    {
        rb.AddForce(startDownMovement, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
    }
}
