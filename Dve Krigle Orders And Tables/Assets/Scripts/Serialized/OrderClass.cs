[System.Serializable]
public class OrderClass {

    public int order_ID;
    public int order_Value;

    public OrderClass(int id, int value)
    {
        order_ID = id;
        order_Value = value;
    }
}