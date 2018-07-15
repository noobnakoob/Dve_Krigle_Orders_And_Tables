using System.Collections.Generic;

[System.Serializable]
public class DataClass {

    public static DataClass current;
    public long selected_Table;
    public List<TableClass> tables_List;
    public List<TablePositionClass> table_Positions;
    public long table_ID = 1;

    public DataClass()
    {
        tables_List = new List<TableClass>();
        table_Positions = new List<TablePositionClass>();
    }
}