using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TableType { Small, Medium, Large }


[System.Serializable]
public class TablePositionClass {

    public long table_ID;
    public float pos_x, pos_y, pos_z;
    public float quat_x, quat_y, quat_z, quat_w;

   // public Vector3 table_Position;
    //public Quaternion table_Rotation;
    public TableType table_Type;

    public TablePositionClass(long id, Vector3 pos, Quaternion rot, TableType type)
    {
        table_ID = id;
        pos_x = pos.x;
        pos_y = pos.y;
        pos_z = pos.z;

        quat_x = rot.x;
        quat_y = rot.y;
        quat_z = rot.z;
        quat_w = rot.w;
        //table_Position = pos;
       // table_Rotation = rot;
        table_Type = type;
    }
}
