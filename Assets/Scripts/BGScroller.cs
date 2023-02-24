using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BGScroller : MonoBehaviour
{
    [Range(-5f,5f)]
    [SerializeField] private float scrollSpeed;

  
    


    private void FixedUpdate()
    {
        transform.position += new Vector3(0, scrollSpeed * Time.deltaTime);

        if (transform.position.y <= -35)
        {
            transform.position = new Vector3(transform.position.x, 28, 2);
        }

    }
}
