using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EnemyTargetProvider
    {
        private PlayerModel[] _playersModels;
        private PlayerView[] _playersViews;

        public EnemyTargetProvider(PlayerModel[] playersModels, PlayerView[] playersViews)
        {
            _playersModels = playersModels;
            _playersViews = playersViews;
        }  

        public Transform GetRandomTarget()
        {
            var targets = new List<Transform>();

            for (int i = 0; i < _playersModels.Length; i++)
            {
                if (!_playersModels[i].IsDead)
                    targets.Add(_playersViews[i].transform);
            }

            var index = Random.Range(0, targets.Count);
            return targets[index].transform;
        }
    }
}
