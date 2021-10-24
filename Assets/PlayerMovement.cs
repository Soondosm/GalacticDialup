using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  
  public float moveSpeed = 5f;
  public Transform movePoint;
  public LayerMask whatStopsMovement;
//   public Animator anim;
  public string up = "up";
  public string down = "down";
  public string right = "right";
  public string left = "left";

  public static string INPUT = "";




    void Start() {

        movePoint.parent = null; // no parent
    }
    void Update()
    {
        if(INPUT != "") {
            
        }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePoint.position) <= .05f) {
            // input
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement)) {
                    Debug.Log("cant stop wont stop");
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                } 
            }
            if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        // float horizontalInput = Input.GetAxisRaw("Horizontal"); // will give something between -1 and 1
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // anim.SetBool("moving", false);

        } 
        // else {
        //     anim.SetBool("moving", true);
        // }

    }

    public static void ChangeInput(string new_val) {
        INPUT = new_val;
    }
}
