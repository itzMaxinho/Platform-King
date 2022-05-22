using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ruby : MonoBehaviour
{
    [SerializeField] private Image ruby;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ruby.gameObject.SetActive(true);
        }
            }
}
