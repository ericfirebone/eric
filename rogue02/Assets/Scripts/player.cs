using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
   
    private float restTimer = 0;
    private Vector2 targetPos=new Vector2(1,1);
    private Rigidbody2D rigidbody;
    private BoxCollider2D colider;
    private Animator animator;
    public float smoothing = 1;
    public float restTime= 0.5f;

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        colider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        restTimer += Time.deltaTime;
        rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));
        if (restTimer< restTime) { return; }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h>0)
        {
            v = 0;
        }
        if (h != 0 || v != 0)
        {
            colider.enabled = false;//先忽略自己的碰撞器
            //检测
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            colider.enabled = true;//检测完再恢复
            if (hit.transform == null)
            {
                targetPos += new Vector2(h, v);
            
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case "outwall":

                        break;
                    case "wall":
                        animator.SetTrigger("attack");
                        hit.collider.SendMessage("takeDamage");
                        break;
                    case "food":

                        targetPos += new Vector2(h, v);
                        GameManager._instance.addFood(10);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "soda":
                        targetPos += new Vector2(h, v);
                        GameManager._instance.addFood(20);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "enemy":

                        break;
                }

            }
            restTimer = 0;

        }
        


	}
}
