using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{


    public GameObject newBird;

    public GameObject newEnemyBird;

    public Transform spawnPos;

    //FirstPos
    Vector3 FirstPos;

    //Range
    public float limitRange;


    public void Start()
    {
        FirstPos = transform.position;
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
        GetComponent<Rigidbody2D>().AddForce(vectorForce);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        //Hàm gọi phương thức sau ** giây
        Invoke("ResetBird", 4f);

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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    //reset bird
    void ResetBird()
    {
        transform.position = FirstPos;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.identity;
    }
}
