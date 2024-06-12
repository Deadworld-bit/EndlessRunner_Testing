using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.points += 1;
            FindObjectOfType<AudioManager>().PlaySound("Coin Pick Up");
            Destroy(gameObject);
        }
    }
}
