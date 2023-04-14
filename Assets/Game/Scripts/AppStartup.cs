using UnityEngine;

namespace Game
{
    /// <summary>
    /// Use for initialization
    /// </summary>
    public class AppStartup : MonoBehaviour
    {
        [Header("Prefabs:")]
        [SerializeField] private GameObject bookPrefab;
        
        /// <summary>
        /// App entry
        /// </summary>
        private void Start()
        {
            Instantiate(bookPrefab);
        }
    }
}
