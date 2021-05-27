using Photon.Pun;

public class PowerUpsManager : MonoBehaviourPun
{
    public static PowerUpsManager Instance;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
    }

    internal void Pickup(PowerUp powerUp, PlayerManager playerManager)
    {
        if (powerUp == null || !photonView.IsMine)
            return;
    }
}