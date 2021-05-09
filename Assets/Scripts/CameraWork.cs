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

        public static CameraWork Instance;

        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;


        #endregion


        #region MonoBehaviour Callbacks

        private void Awake()
        {
            Instance = this;
            if (!Instance)
            Debug.LogError("CameraWork: Instance was not assigned!");
        }
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
        {
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
            if (isFollowing == false)
            {
                playerTransform = PlayerManager.LocalPlayerInstance.transform;
                if (playerTransform != null)
                {
                    cinemaMachine.Follow = playerTransform;
                    isFollowing = true;
                }
            }
        }


        #endregion

    }
}