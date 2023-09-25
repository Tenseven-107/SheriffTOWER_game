using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    [SerializeField] public GameObject PlaceOnDrop;
    [SerializeField] string ContainerTag = "TowerContainer";

    float bufferTime = 0.85f;
    float last;
    BoxCollider2D coll;

    Transform container;
    PositionToGridpos posToGrid;
    CheckGridPosition gridChecker;



    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        Vector2 size = coll.size;
        coll.size = new Vector2(size.x * 1.5f, size.y * 1.5f);

        GameObject CheckObject = GameObject.FindGameObjectWithTag("TowerContainer");
        gridChecker = CheckObject.GetComponent<CheckGridPosition>();

        container = GameObject.FindWithTag(ContainerTag).transform;
        posToGrid = gameObject.AddComponent<PositionToGridpos>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (Time.time - last < bufferTime / 2)
                {
                    return;
                }
                last = Time.time;

                PlayerPocket pocket = collisionObject.GetComponent<PlayerPocket>();
                pocket.AddItem(gameObject);

                gameObject.SetActive(false);
            }
        }
    }


    public void Drop()
    {
        if (PlaceOnDrop != null)
        {
            if (gridChecker.CheckIfAvailable(posToGrid.PositionToGrid(transform.position)))
            {
                Instantiate(PlaceOnDrop, posToGrid.PositionToGrid(transform.position), Quaternion.Euler(0, 0, 0), container);

                Destroy(gameObject);
            }
            else
            {
                coll.enabled = false;
                StartCoroutine(BufferTime());
            }
        }
        else
        {
            coll.enabled = false;
            StartCoroutine(BufferTime());
        }
    }

    IEnumerator BufferTime()
    {
        yield return new WaitForSeconds(bufferTime);
        coll.enabled = true;
        yield break;
    }
}
