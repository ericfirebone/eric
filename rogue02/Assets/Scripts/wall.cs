using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {
    private int hp = 2;
    public Sprite damageSprite;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    public void takeDamage()
    {
        hp -= 1;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        if (hp==0)
        {
            Destroy(this.gameObject);
        }
    }
}
