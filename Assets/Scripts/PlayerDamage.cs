using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public float health = 100.0f;
    public float timer = 0.0f;
    GameObject LevelManager;
    public float painTimer = 0.0f;
    [SerializeField]
    private GameObject playerDead;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Instantiate(playerDead, new Vector3 (this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), this.transform.rotation);
            reset();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
        }
    }

    private void reset()
    {
        health = 10;
        this.transform.position = new Vector3(-4, 5, 4);
    }
}
