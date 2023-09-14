using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    float dropBufferTime = 0.8f;
    BoxCollider2D coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        Vector2 size = coll.size;
        coll.size = new Vector2(size.x * 1.5f, size.y * 1.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                PlayerPocket pocket = collisionObject.GetComponent<PlayerPocket>();
                pocket.AddItem(gameObject);

                gameObject.SetActive(false);
            }
        }
    }


    public void DropBuffer()
    {
        coll.enabled = false;
        StartCoroutine(BufferTime());
    }

    IEnumerator BufferTime()
    {
        yield return new WaitForSeconds(dropBufferTime);
        coll.enabled = true;
        yield break;
    }
}
