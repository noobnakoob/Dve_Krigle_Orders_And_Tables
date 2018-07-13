using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableType { Small, Medium, Large }

[System.Serializable]
public class TableClass {

    public int table_ID;
    public TableType table_Type;
    public List<OrderClass> orders_List;
    public string table_Notice;
    public int table_Sum;
    public int number_Of_Orders;

    public TableClass(int id, TableType type)
    {
        table_ID = id;
        table_Type = type;
        orders_List = new List<OrderClass>();
        table_Notice = "";
        table_Sum = 0;
        number_Of_Orders = 0;
    }
}