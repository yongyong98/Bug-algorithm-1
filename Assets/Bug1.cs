using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug1 : MonoBehaviour
{
    public float speed = 5f;
    public Transform goal;
    
    public GameObject hitpoint;
    
    private Transform obstacle = null;
    float isMinus = 1f;
    bool check = false;
    float distance = Mathf.Infinity;
    Vector3 closestPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(distance > (transform.position - goal.position).magnitude) // 최소지점 구하는 코드 
        {
            closestPos = transform.position;
            distance = (transform.position - goal.position).magnitude;
            Debug.Log(closestPos);
        }

        if(obstacle != null) // obstacle이 true일때 벽면을 타고 움직임 
        {
            Vector3 toObstacle = obstacle.position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position,toObstacle.normalized*2f+ Quaternion.Euler(0,0,90) * toObstacle*isMinus + transform.position, speed * Time.deltaTime); //두번째 파라메터 마지막 +전까지가 vector 방향을 나타냄 (앞에 vector normalization을 더한 이유는 옆으로 돌때 잘 돌라고 보정한거임 )
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, goal.position, speed * Time.deltaTime);
        }
        
        if(isMinus==-1f){ // leave point 도착하면 obstacle,check,isminus를 초기화 시켜주면서 다음번 obstacle 또는 goal로 가도록 함 
            if((transform.position-closestPos).magnitude<0.05f){
                obstacle = null;
                check = false;
                isMinus = 1f;
            }
        } 
    }

    void OnCollisionEnter2D(Collision2D collision) //collision에 부딫친 object가 들어옴  ## 부딫쳤을때
    {
        if(obstacle == null){
            Instantiate(hitpoint, transform.position, Quaternion.identity);
            obstacle = collision.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {  //겹쳤을때 
        if(check == false)
            check = true;
        else if(check == true){
            isMinus = -1f;
        }
    }
}
