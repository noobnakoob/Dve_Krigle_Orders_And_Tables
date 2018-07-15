using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour {

    public static TableManager instance;
    public Transform table_Parent;
    public GameObject large_Prefab;
    public GameObject medium_Prefab;
    public GameObject small_Prefab;
    public GameObject large_Button;
    public GameObject medium_Button;
    public GameObject small_Button;
    public GameObject edit_Button;
    public Sprite large_On, large_Off, medium_On, medium_Off, small_On, small_Off, edit_On, edit_Off;
    public GameObject grid1, grid2;

    bool is_Large;
    bool is_Medium;
    bool is_Small;
    public bool is_Edit;
    GameObject prefab;

    bool is_Instantiated;
    bool is_Available = true;

    // Use this for initialization
    void Start () {

        instance = this;		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (is_Edit)
        {
            if (!is_Instantiated)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.back, 100);
                    if (hit)
                    {
                        if (hit.collider.tag == "Grid")
                        {
                            GameObject go = Instantiate(prefab, new Vector3(pos.x, pos.y, 10), Quaternion.identity) as GameObject;
                            go.transform.SetParent(table_Parent, true);
                            go.name = "Table" + DataClass.current.table_ID.ToString();
                            go.GetComponent<DragAndDrop>().table_ID = DataClass.current.table_ID;
                            is_Instantiated = true;
                        }
                    }
                }
            }
            else
            {
                SetNewTable();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.back, 100);
                if (hit)
                {
                    if (hit.collider.name.Contains("Table"))
                    {
                        GUIManager.instance.UpdateTable(hit.collider.GetComponent<DragAndDrop>().table_ID);

                    }
                }
            }
        }
    }    

    void SetNewTable()
    {
        if (is_Instantiated)
        {
            is_Instantiated = false;
            DataClass.current.tables_List.Add(new TableClass(DataClass.current.table_ID));
            GameObject temp = GameObject.Find("Table" + DataClass.current.table_ID.ToString());
            DataClass.current.table_Positions.Add(new TablePositionClass(DataClass.current.table_ID, 
                temp.transform.localPosition, 
                temp.transform.rotation,
                (is_Large) ? TableType.Large : ((is_Medium) ? TableType.Medium : TableType.Small)));
            DataClass.current.table_ID = (DataClass.current.table_ID + 1) % long.MaxValue;
        }
    }

    public void OnLargeTableClicked()
    {
        if (!is_Large)
        {
            large_Button.GetComponent<Image>().sprite = large_On;
            is_Large = true;
            prefab = large_Prefab;
        }
        else
        {
            large_Button.GetComponent<Image>().sprite = large_Off;
            is_Large = false;
        }
        medium_Button.GetComponent<Image>().sprite = medium_Off;
        small_Button.GetComponent<Image>().sprite = small_Off;
        is_Medium = false;
        is_Small = false;
    }

    public void OnMediumTableClicked()
    {
        if (!is_Medium)
        {
            medium_Button.GetComponent<Image>().sprite = medium_On;
            is_Medium = true;
            prefab = medium_Prefab;
        }
        else
        {
            medium_Button.GetComponent<Image>().sprite = medium_Off;
            is_Medium = false;
        }
        large_Button.GetComponent<Image>().sprite = large_Off;
        small_Button.GetComponent<Image>().sprite = small_Off;
        is_Large = false;
        is_Small = false;
    }

    public void OnSmallTableClicked()
    {
        if (!is_Small)
        {
            small_Button.GetComponent<Image>().sprite = small_On;
            is_Small = true;
            prefab = small_Prefab;
        }
        else
        {
            small_Button.GetComponent<Image>().sprite = small_Off;
            is_Small = false;
        }
        medium_Button.GetComponent<Image>().sprite = medium_Off;
        large_Button.GetComponent<Image>().sprite = large_Off;
        is_Medium = false;
        is_Large = false;
    }

    public void OnEditButtonClicked()
    {
        if(!is_Edit)
        {
            is_Edit = true;
            edit_Button.GetComponent<Image>().sprite = edit_On;
            large_Button.GetComponent<Button>().interactable = true;
            medium_Button.GetComponent<Button>().interactable = true;
            small_Button.GetComponent<Button>().interactable = true;
            grid1.SetActive(true);
            grid2.SetActive(true);
        }
        else
        {
            is_Edit = false;
            edit_Button.GetComponent<Image>().sprite = edit_Off;
            large_Button.GetComponent<Button>().interactable = false;
            medium_Button.GetComponent<Button>().interactable = false;
            small_Button.GetComponent<Button>().interactable = false;
            small_Button.GetComponent<Image>().sprite = small_Off;
            medium_Button.GetComponent<Image>().sprite = medium_Off;
            large_Button.GetComponent<Image>().sprite = large_Off;
            grid1.SetActive(false);
            grid2.SetActive(false);
        }
    }

    public void SetIsInstantiated(bool val)
    {
        is_Instantiated = val;
    }

    public bool IsAvailable
    {
        get { return is_Available; }
        set { is_Available = value; }
    }
}
