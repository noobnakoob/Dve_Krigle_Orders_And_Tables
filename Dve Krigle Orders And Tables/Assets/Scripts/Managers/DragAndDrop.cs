using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    SpriteRenderer rend;
    Rigidbody2D body;
    public long table_ID;

    bool is_Drag;
    bool rotated;

    // Use this for initialization
    void Start() {

        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetMouseButtonUp(0))
        {
            if (!TableManager.instance.IsAvailable)
            {
                TableManager.instance.SetIsInstantiated(false);
                Destroy(this.gameObject);
            }
            else
                TableManager.instance.SetIsInstantiated(true);
        }

        if (is_Drag)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!rotated)
                {
                    body.MoveRotation(90);
                    rotated = true;
                }
                else
                {
                    body.MoveRotation(0);
                    rotated = false;
                }
            }
        }
    }

    void OnMouseDrag()
    {
        is_Drag = true;
        rend.enabled = false;
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        if (Mathf.Abs(body.position.x - newPos.x) > 0.08f && Mathf.Abs(body.position.y - newPos.y) > 0.08f)
        { 
            body.MovePosition(new Vector2(newPos.x, newPos.y));
        }
        else if (Mathf.Abs(body.position.x - newPos.x) > 0.08f)
        {
            body.MovePosition(new Vector2(newPos.x, body.position.y));
        }
        else if (Mathf.Abs(body.position.y - newPos.y) > 0.08f)
        {
            body.MovePosition(new Vector2(body.position.x, newPos.y));
        }
    }

    void OnMouseUp()
    {
        rend.enabled = true;
        is_Drag = false;
    }


}
