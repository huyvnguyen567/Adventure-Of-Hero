using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    private Player player;
    private float halfHeight;
    private float halfWidth;
    void Start()
    {
        AudioController.Instance.PlayLevelMusic();
        player = FindObjectOfType<Player>();
        halfHeight = Camera.main.orthographicSize;
        halfWidth =  halfHeight * Camera.main.aspect;
    }

    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, box.bounds.min.x + halfWidth, box.bounds.max.x - halfWidth),
                Mathf.Clamp(player.transform.position.y, box.bounds.min.y + halfHeight, box.bounds.max.y - halfHeight), transform.position.z);
        }
    }
}
