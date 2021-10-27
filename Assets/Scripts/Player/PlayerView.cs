using UnityEngine;

namespace TankGame
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform ShellStartPosition;

        public void Shoot(GameObject shell, int shootForce)
        {
            shell.transform.position = ShellStartPosition.position;
            shell.transform.rotation = transform.rotation;
            shell.SetActive(true);
            shell.GetComponent<Rigidbody>().AddForce(ShellStartPosition.forward * shootForce, ForceMode.Impulse);
        }
    }
}