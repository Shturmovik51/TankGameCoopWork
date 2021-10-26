using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class PlayerModel : IPlayerModel
    {
        public GameObject Tank { get; }     

        public PlayerModel(GameObject tank)
        {
            Tank = tank;
        }
    }
}