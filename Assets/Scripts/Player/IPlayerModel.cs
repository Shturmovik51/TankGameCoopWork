using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public interface IPlayerModel
    {
        public GameObject Tank { get; }
        public Transform ShellStartPosition { get; set; }
    }
}