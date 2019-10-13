using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class DisparoDeCarga : MonoBehaviour
    {
        // Start is called before the first frame update
        public float timeLife;
        private float auxTimeLife;
        public float damage;
        void Start()
        {
            auxTimeLife = timeLife;
        }
        private void OnEnable()
        {
            timeLife = auxTimeLife;
        }
        // Update is called once per frame
        void Update()
        {
            CheckTimeLife();
        }
        public void CheckTimeLife()
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else if (timeLife <= 0)
            {
                timeLife = auxTimeLife;
                gameObject.SetActive(false);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Player player = collision.GetComponent<Player>();
                if (player == null)
                {
                    return;
                }
                player.PD.lifePlayer = player.PD.lifePlayer - damage;
            }
        }
    }
}
