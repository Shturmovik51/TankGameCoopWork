using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/InputKeysData", fileName = nameof(InputKeysData))]
    public sealed class InputKeysData : ScriptableObject
    {
        [SerializeField] private KeyCode _shoot;
        [SerializeField] private KeyCode _nextTarget;
        [SerializeField] private KeyCode _previousTarget;

        public KeyCode Shoot => _shoot;
        public KeyCode NextTarget => _nextTarget;
        public KeyCode PreviousTarget => _previousTarget;
    }
}