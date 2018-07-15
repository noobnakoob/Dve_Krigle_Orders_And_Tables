using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TableClass {

    public long table_ID;
    public List<OrderClass> orders_List;
    public string table_Notice;
    public int table_Sum;
    public int number_Of_Orders;

    public TableClass(long id)
    {
        table_ID = id;
        orders_List = new List<OrderClass>();
        table_Notice = "";
        table_Sum = 0;
        number_Of_Orders = 0;
    }
}