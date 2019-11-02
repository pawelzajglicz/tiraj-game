using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponent<Player>();
            player.MakeGrowing();
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponent<Player>();
            player.StopGrowing();
        }
    }
}
