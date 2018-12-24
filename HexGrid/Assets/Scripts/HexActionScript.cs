using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexActionScript : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        		
	}

    void OnMouseDown()
    {
        gameObject.GetComponentInParent<GameManagerScript>().ExecuteState(gameObject);
    }

    public void ChangeColor(Color32 color)
    {
        transform.GetComponent<MeshRenderer>().material.color = color;
    }

    public IEnumerator Choose(Color32 col)
    {
        Color32 color = col;
        Color32 color2 = new Color32(255,255,255,255);
        for (int i = 0; i < 3; i++)
        {
            transform.GetComponent<MeshRenderer>().material.color = color;
            yield return new WaitForSeconds(0.2f);
            transform.GetComponent<MeshRenderer>().material.color = color2;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
