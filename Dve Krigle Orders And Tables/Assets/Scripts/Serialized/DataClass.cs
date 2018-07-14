using System.Collections.Generic;

[System.Serializable]
public class DataClass {

    public static DataClass current;
    public int selected_Table;
    public List<TableClass> tables_List;
    public long table_ID = 1;

    public DataClass()
    {
        tables_List = new List<TableClass>();
    }
}