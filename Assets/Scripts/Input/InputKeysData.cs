using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/InputKeysData", fileName = nameof(InputKeysData))]
    public sealed class InputKeysData : ScriptableObject
    {
        [SerializeField] private KeyCode _shoot;

        public KeyCode Shoot => _shoot;
    }
}