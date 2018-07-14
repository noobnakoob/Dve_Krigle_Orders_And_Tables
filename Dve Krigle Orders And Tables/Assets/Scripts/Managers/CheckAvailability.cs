using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAvailability : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Table"))
        {
            if (this.gameObject.tag == "Wall")
                TableManager.instance.IsAvailable = false;
            else
            {
                if (TableManager.instance.IsAvailable)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Table"))
        {
            if (this.gameObject.tag == "Wall")
                TableManager.instance.IsAvailable = true;
            else
            {                
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);                
            }
        }
    }
}