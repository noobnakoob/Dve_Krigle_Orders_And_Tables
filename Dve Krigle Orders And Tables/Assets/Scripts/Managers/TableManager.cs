using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour {

    public static TableManager instance;
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
    bool is_Edit;
    GameObject prefab;

    bool is_Instantiated;
    bool is_Available = true;
    Vector3 newPos;

    // Use this for initialization
    void Start () {

        instance = this;		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!is_Instantiated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.back, 100);
                if (hit)
                {
                    if (hit.collider.tag == "Cell")
                    {
                        newPos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, 10);
                        newPos = new Vector3(newPos.x - newPos.x % 0.08f + 0.04f, newPos.y - newPos.y % 0.08f + 0.04f, newPos.z);
                        GameObject go = Instantiate(prefab, newPos, Quaternion.identity) as GameObject;
                        go.GetComponent<DragAndDrop>().table_ID = DataClass.current.table_ID;
                    }
                }
            }
        }
        else
        {
            is_Instantiated = false;
            DataClass.current.tables_List.Add(new TableClass(DataClass.current.table_ID,
                (is_Large) ? TableType.Large : ((is_Medium) ? TableType.Medium : TableType.Small), newPos));
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
