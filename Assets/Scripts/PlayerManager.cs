using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
public class PlayerManager : MonoBehaviourPunCallbacks , IPunObservable
{
    public static PlayerManager Instance;

    #region Private Fields

    [SerializeField] GameObject _playerUiPrefab;
    private PlayerMovement _playerMovement;



    #endregion
    #region Public Fields
    private static GameObject localPlayerInstance;
    public static GameObject LocalPlayerInstance
    {
        get
        {

            return localPlayerInstance;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // streaming and recieving!


        if (stream.IsWriting)
        {
            // send inputs to other clients
        }
        else
        {
            // recieve inputs in the same order as sending!
        }
    }
    #endregion


    private void Awake()
    {

        if (photonView.IsMine)
        {
            Instance = this;
            localPlayerInstance = this.gameObject;
        }
        else
            Debug.Log("PlayerManager Awake() : Not Local Client");

        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        if (Photon.Pun.Demo.PunBasics.CameraWork.GetInstance != null)
        {
            if (photonView.IsMine)
                Photon.Pun.Demo.PunBasics.CameraWork.GetInstance.OnStartFollowing();
        }
        else
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork componenet on player prefab.");


        if (_playerUiPrefab != null)
        {

            // when we will have UI
        //    GameObject _uiGO = Instantiate(_playerUiPrefab);
         //   _uiGO.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);








        if (photonView.IsMine)
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerMovement.enabled = true;
            _playerMovement.InitMovement();
        }

  
  

#if UNITY_5_4_OR_NEWER
            // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
#endif
    }





#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        => this.CalledOnLevelWasLoaded(scene.buildIndex);
#endif
#if !UNITY_5_4_OR_NEWER
/// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        private void OnLevelWasLoaded(int level)
        {
      this.CalledOnLevelWasLoaded(level)
        }
#endif
    private void CalledOnLevelWasLoaded(int level)
    {
      //  // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
      //  if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
      //      transform.position = new Vector3(0, 5f, 0);

      //  GameObject _uiGo = Instantiate(this._playerUiPrefab);
      ////  _uiGo.GetComponent<PlayerUI>().SetTarget(this); //("SetTarget", this, SendMessageOptions.RequireReceiver);
    }
#if UNITY_5_4_OR_NEWER

    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif



}

