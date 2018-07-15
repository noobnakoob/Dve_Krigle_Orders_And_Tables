using UnityEngine;

public class DragAndDrop : MonoBehaviour {

    Rigidbody2D body;
    SpriteRenderer rend;
    public long table_ID;

    bool is_Drag;
    bool rotated;
    public bool table_Set;

    Color brown = new Color(0.36f, 0.16f, 0.03f, 1f);

    // Use this for initialization
    void Start() {

        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        rend.color = brown;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetMouseButtonUp(0))
        {
            if (!TableManager.instance.IsAvailable && !table_Set)
                Destroy(this.gameObject);
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
        if (TableManager.instance.is_Edit)
        {
            is_Drag = true;
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
    }

    void OnMouseDown()
    {
        if (TableManager.instance.is_Edit)
           table_Set = false;
    }

    void OnMouseUp()
    {
        if (TableManager.instance.is_Edit)
        {
            is_Drag = false;
            table_Set = true;
            TablePositionClass cur_Table = DataClass.current.table_Positions.Find(t => t.table_ID == table_ID);
            cur_Table.pos_x = this.transform.localPosition.x;
            cur_Table.pos_y = this.transform.localPosition.y;
            cur_Table.pos_z = this.transform.localPosition.z;

            cur_Table.quat_x = this.transform.rotation.x;
            cur_Table.quat_y = this.transform.rotation.y;
            cur_Table.quat_z = this.transform.rotation.z;
            cur_Table.quat_w = this.transform.rotation.w;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!table_Set && TableManager.instance.is_Edit)
        {
            if (other.gameObject.name.Contains("Table") ||
                other.gameObject.tag == "Wall")
            {
                TableManager.instance.IsAvailable = false;
                rend.color = Color.red;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!table_Set && TableManager.instance.is_Edit)
        {
            if (other.gameObject.name.Contains("Table") ||
                other.gameObject.tag == "Wall")
            {
                TableManager.instance.IsAvailable = true;
                rend.color = brown;
            }
        }
    }
}
