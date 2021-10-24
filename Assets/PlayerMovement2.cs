using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2 : MonoBehaviour
{
  
  public float moveSpeed = 5f;
  public Transform movePoint;
  public LayerMask whatStopsMovement;
//   public Animator anim;

  public static string INPUT = "";




    void Start() {

        movePoint.parent = null; // no parent
    }
    void Update()
    {   
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if(INPUT != "") {
            // up
            if(INPUT.Contains("up") == true) {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(0f, -2f, 0f);
                }
            // down
            } else if(INPUT.Contains("down") == true) {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(0f, 2f, 0f);
                }
            //right
            } else if (INPUT.Contains("right") == true) {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(-2f, 0f, 0f);
                } 
            //left
            } else if(INPUT.Contains("left") == true) {
                Debug.Log("LEFT");
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement)) {
                    movePoint.position += new Vector3(2f, 0f, 0f);
                } 
            }
            INPUT = ""; // reset string for next voice command
        }
    }

    public static void ChangeInput(string new_val) {
        INPUT = new_val;
    }
}
