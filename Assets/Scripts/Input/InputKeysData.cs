using UnityEngine;

namespace TankGame
{
    [CreateAssetMenu(menuName = "DataBase/InputKeysData", fileName = nameof(InputKeysData))]
    public sealed class InputKeysData : ScriptableObject
    {
        [SerializeField] private KeyCode _shoot;
        [SerializeField] private KeyCode _nextTarget;
        [SerializeField] private KeyCode _previousTarget;
        [SerializeField] private KeyCode _save;
        [SerializeField] private KeyCode _load;

        public KeyCode Shoot => _shoot;
        public KeyCode NextTarget => _nextTarget;
        public KeyCode PreviousTarget => _previousTarget;
        public KeyCode Save => _save;
        public KeyCode Load => _load;
    }
}