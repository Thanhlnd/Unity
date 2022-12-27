using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private GameObject newBird;
   
    [SerializeField] private GameObject newEnemyBird;
   
    [SerializeField] private Transform spawnPos;

    //FirstPos
    Vector3 FirstPos;

    //Range
    [SerializeField] private float limitRange;

    private Rigidbody2D Rg2d;
    

    public void Start()
    {
        FirstPos = transform.position;
        Rg2d = GetComponent<Rigidbody2D>();
    }

    public void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        float distance = (FirstPos - mousePos).magnitude;
        Debug.Log(distance);

        if (distance < limitRange)
        {
            transform.position = new Vector3(mousePos.x, mousePos.y);
        }

    }

    public void OnMouseUp()
    {
        Vector3 vectorForce = (FirstPos - transform.position) * 300;
        Rg2d.AddForce(vectorForce);

        Rg2d.gravityScale = 1f;



        // StartCoroutine(respawnBird());

    }

    // respawn lại bird
    // IEnumerator respawnBird()
    // {
    //     yield return new WaitForSeconds(2f);
    //     GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    //     GetComponent<Rigidbody2D>().gravityScale = 0;
    //     GetComponent<Rigidbody2D>().angularVelocity = 0;
    //     //bird.transform.position = FirstPos;
    //     Instantiate(newBird, FirstPos, Quaternion.identity);
    // }

    //va chạm
    void OnCollisionEnter2D(Collision2D other)
    {
        // If the Collider2D component is enabled on the collided object
        if (other.collider.tag == "Enemy") //đk khi va chạm tag "Enemy"
        {
            //xoá enemybird
            Destroy(other.collider.gameObject);
            // Destroy(this.gameObject);
            // GameObject bird = Instantiate(newBird, FirstPos, Quaternion.identity);
            ResetBird();
            // bird.GetComponent<Collider2D>().enabled = true;
            // bird.GetComponent<Bird>().enabled = true;


            GameObject enemy = Instantiate(newEnemyBird, spawnPos.position, Quaternion.identity);

            enemy.GetComponent<CircleCollider2D>().enabled = true;

        }
        if (other.collider.tag == "Bg")
        {
            Rg2d.velocity = Vector2.zero;
            Rg2d.angularVelocity = 0;
            //Hàm gọi phương thức sau ** giây
            Invoke("ResetBird", 1.5f);
        }
    }

    //reset bird
    void ResetBird()
    {
        Debug.Log("run reset");
        transform.position = FirstPos;
        Rg2d.velocity = Vector2.zero;
        Rg2d.gravityScale = 0;
        Rg2d.angularVelocity = 0;
        transform.rotation = Quaternion.identity;
    }
}
