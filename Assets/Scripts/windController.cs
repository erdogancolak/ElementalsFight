using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyController.instance.GetComponent<EnemyController>().Jump();
        }

    }
}
