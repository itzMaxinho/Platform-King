using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class finish : MonoBehaviour
{
    [SerializeField] private Image needRuby;
    [SerializeField] private Image youWin;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerMovement.instance.allowFinish == true)
        {
            Time.timeScale = 0f;
            needRuby.gameObject.SetActive(false);
            youWin.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Jump"))
            {
                youWin.gameObject.SetActive(false);
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            needRuby.gameObject.SetActive(true);
        }
    }
}
