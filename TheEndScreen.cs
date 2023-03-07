using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndScreen : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.y < 1235)
        transform.position += new Vector3(0, speed, 0);
    }
}
    