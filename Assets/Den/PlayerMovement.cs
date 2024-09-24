using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Den
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField]
        private float speed = 5f;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        }
    }
}

