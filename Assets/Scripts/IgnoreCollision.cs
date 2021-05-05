﻿using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        }
    }
}
