using Cinemachine;
using UnityEngine;
using Photon.Pun;

namespace Photon.Pun.Demo.PunBasics
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;
        //  transform of the target
        Transform playerTransform;

        [SerializeField] CinemachineVirtualCamera cinemaMachine;

        public static CameraWork GetInstance { get {
                if (Instance == null)
                    Debug.LogError("CameraWork: Tried to get Instance but it was not assigned");

                return Instance;
            }
        }
        private static CameraWork Instance;

        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;


        #endregion
        public bool SetFollowOnStart { set => followOnStart = value; }

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Instance = this;
            if (!GetInstance)
            Debug.LogError("CameraWork: Instance was not assigned!");
        }
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
        {
            isFollowing = false;
            playerTransform = null;
            cinemaMachine = GetComponent<CinemachineVirtualCamera>();
            // Start following the target if wanted.
            if (followOnStart )
            {
                OnStartFollowing();
            }
        }


        void LateUpdate()
        {
        
            if (isFollowing)
            {
                // The transform target may not destroy on level load,
                // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens

                if (playerTransform == null)
                    OnStartFollowing();
            }
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Raises the start following event.
        /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
        /// </summary>
        public void OnStartFollowing()
        {
                Debug.Log("* isFollowing " + isFollowing);
            if (isFollowing == false)
            {
                Debug.Log("** PlayerManager.LocalPlayerInstance " + PlayerManager.LocalPlayerInstance);
                playerTransform = PlayerManager.LocalPlayerInstance.transform;
                Debug.Log("*** playerTransform " + playerTransform);
                Debug.Log("**** cinemaMachine " + cinemaMachine);
                Debug.Log("***** cinemaMachine.Follow " + cinemaMachine.Follow);
                 cinemaMachine.Follow = playerTransform;
                    isFollowing = true;
                
            }
        }
        public void ResetCameraStats()
        {
            if (true)
            {

            }
        }

        #endregion

    }
}