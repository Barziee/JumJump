using System;
using System.Collections;
using Photon.Pun;
using UnityEngine;


public enum PowerUpTypes {Jump, Speed};

public class PowerUp : MonoBehaviourPun
{
    public float ResetTimer = 5;
    public PowerUpTypes PowerUpTypes;

    [SerializeField] MeshRenderer _mr;
    [SerializeField] BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        if (!_mr)
            Debug.LogError("PowerUp: Mesh Renderer was not assiged correctly");
        if (!_boxCollider2D)
            Debug.LogError("PowerUp: Box Collider 2d was not assiged correctly");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Powerup was picked");
           // PowerUpsManager.Instance.Pickup(this, collision.transform.GetComponent<PlayerManager>());
            this.photonView.RPC("InSceneView", RpcTarget.AllBuffered, false);
            if (photonView.IsMine)
                StartCoroutine(ResetTime());
        }
    }
    [PunRPC]
    public void InSceneView(bool toShow)
    {
        Debug.Log(toShow ? "Send To All client To Disable The PowerUp" : "Recieved from client the enable the powerup!");
         
        _boxCollider2D.enabled = toShow;
        _mr.enabled = toShow;
    }


    IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(ResetTimer);
        this.photonView.RPC("InSceneView", RpcTarget.AllBufferedViaServer, true);
    }

}
