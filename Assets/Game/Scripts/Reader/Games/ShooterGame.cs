using Cinemachine;
using UnityEngine;

namespace Game.Reader.Games
{
    /// <summary>
    /// Simple shooter mini-game
    /// </summary>
    public class ShooterGame : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private CinemachineVirtualCamera gameCamera;
        
        private void Start()
        {
            Debug.Log("[ShooterGame] Start");
        }
    }
}
