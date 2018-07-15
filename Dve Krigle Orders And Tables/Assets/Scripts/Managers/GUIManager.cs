using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour {

    public static GUIManager instance;
    [Header("Table")]
    public InputField order_Field;
    public Transform order_Holder;
    public TextMeshProUGUI table_Sum;
    public GameObject order_Prefab;

    [Header("Check Sum")]
    public GameObject check_Sum_Panel;
    public Transform check_Holder;
    public TextMeshProUGUI text_Sum;
    public Button delete_Check;
    public GameObject check_Sum_Prefab;

    public GameObject large_Table;
    public GameObject medium_Table;
    public GameObject small_Table;
    public Transform table_Parent;

    TableClass current_Table;
    GameObject table_Prefab;

    // Use this for initialization
    void Start() {

        instance = this;
        LoadData.LoadFromFile();
        DataClass.current.selected_Table = 0;
        GetTables();
        UpdateTableOrders();
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateTable(long tableID)
    {
        order_Field.text = "";
        DataClass.current.selected_Table = tableID;                
        UpdateTableOrders();
    }

    public void MakeNewOrder()
    {
        int value = int.Parse(order_Field.text);
        order_Field.text = "";
        current_Table.orders_List.Add(new OrderClass(current_Table.number_Of_Orders + 1, value));
        current_Table.orders_List.Sort((o1, o2) => o2.order_ID.CompareTo(o1.order_ID));
        current_Table.table_Sum += value;
        current_Table.number_Of_Orders++;

        UpdateTableOrders();
    }

    void CancelOrder(OrderClass order)
    {
        current_Table.table_Sum -= order.order_Value;
        current_Table.orders_List.Remove(order);
        current_Table.orders_List.Sort((o1, o2) => o2.order_ID.CompareTo(o1.order_ID));

        UpdateTableOrders();
    }

    void UpdateTableOrders()
    {
        if (DataClass.current.selected_Table > 0)
        {
            current_Table = DataClass.current.tables_List.Find(t => t.table_ID == DataClass.current.selected_Table);
            order_Field.onEndEdit.RemoveAllListeners();
            order_Field.onEndEdit.AddListener(delegate
            {
                MakeNewOrder();
            });

            table_Sum.text = current_Table.table_Sum.ToString();

            if (order_Holder.childCount > 0)
                for (int i = order_Holder.childCount - 1; i >= 0; i--)
                    Destroy(order_Holder.GetChild(i).gameObject);

            for (int i = 0; i < current_Table.orders_List.Count; i++)
            {
                GameObject go = Instantiate(order_Prefab, order_Holder, false) as GameObject;
                OrderClass order = current_Table.orders_List[i];
                go.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = current_Table.orders_List[i].order_Value.ToString();
                go.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    CancelOrder(order);
                });
            }
        }
        else
        {
            order_Field.onEndEdit.RemoveAllListeners();
            table_Sum.text = 0.ToString();
            if (order_Holder.childCount > 0)
                for (int i = order_Holder.childCount - 1; i >= 0; i--)
                    Destroy(order_Holder.GetChild(i).gameObject);
        }
        SaveData.SaveToFile();
    }

    public void OnCheckClicked()
    {
        if (DataClass.current.selected_Table > 0)
        {
            check_Sum_Panel.SetActive(true);

            if (check_Holder.childCount > 0)
                for (int i = check_Holder.childCount - 1; i >= 0; i--)
                    Destroy(check_Holder.GetChild(i).gameObject);

            if (DataClass.current.selected_Table > 0)
            {
                for (int i = 0; i < current_Table.orders_List.Count; i++)
                {
                    GameObject go = Instantiate(check_Sum_Prefab, check_Holder, false);
                    go.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = current_Table.orders_List[i].order_Value.ToString();
                }

                text_Sum.text = current_Table.table_Sum.ToString();

                delete_Check.onClick.RemoveAllListeners();
                delete_Check.onClick.AddListener(delegate
                {
                    DeleteTableOrders();
                });
            }
        }
    }

    void DeleteTableOrders()
    {
        DataClass.current.tables_List.Remove(DataClass.current.tables_List.Find(t => t.table_ID == DataClass.current.selected_Table));
        DataClass.current.selected_Table = 0;
        UpdateTableOrders();
        SaveData.SaveToFile();
        CloseCheckSumPanel();
    }

    public void CloseCheckSumPanel()
    {
        check_Sum_Panel.SetActive(false);
    }

    public void GetTables()
    {
        if (DataClass.current.table_Positions.Count > 0)
        {
            for(int i = 0; i < DataClass.current.table_Positions.Count; i++)
            {
                if (DataClass.current.table_Positions[i].table_Type == TableType.Large)
                    table_Prefab = large_Table;
                else if (DataClass.current.table_Positions[i].table_Type == TableType.Medium)
                    table_Prefab = medium_Table;
                else if (DataClass.current.table_Positions[i].table_Type == TableType.Small)
                    table_Prefab = small_Table;

                GameObject go = Instantiate(table_Prefab, table_Parent, false) as GameObject;
                Vector3 pos = new Vector3(
                    DataClass.current.table_Positions[i].pos_x,
                    DataClass.current.table_Positions[i].pos_y,
                    DataClass.current.table_Positions[i].pos_z);

                Quaternion rot = new Quaternion(
                   DataClass.current.table_Positions[i].quat_x,
                   DataClass.current.table_Positions[i].quat_y,
                   DataClass.current.table_Positions[i].quat_z,
                   DataClass.current.table_Positions[i].quat_w);

                go.transform.localPosition = pos;
                go.transform.rotation = rot;
                go.name = "Table" + DataClass.current.table_Positions[i].table_ID.ToString();
                go.GetComponent<DragAndDrop>().table_ID = DataClass.current.table_Positions[i].table_ID;
            }
        }
    }

    void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveData.SaveToFile();
    }
}