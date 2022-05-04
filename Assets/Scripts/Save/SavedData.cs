using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    [Serializable]
    public sealed class SavedData
    {
        public PlayerSaveModel PlayerSave;
        public EnemySaveModel[] EnemiesSave;
        public SkillButtonSaveModel[] SkillButtonsSave;
    }
}