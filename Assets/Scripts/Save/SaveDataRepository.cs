using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace TankGame
{
    public sealed class SaveDataRepository : ISaveDataRepository, IInitializable, IController
    {
        private IData<SavedData> _data;
        private string _path;
        private InputController _inputController;
        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private PlayerModel _playerModel;

        public SaveDataRepository(InputController inputController, PlayerModel playerModel)
        {
            _inputController = inputController;
            _playerModel = playerModel;
        }

        public void Initialization()
        {
            _data = new JsonData<SavedData>();
            _path = Path.Combine(Application.dataPath, _folderName);
            _inputController.OnClickSave += Save;
            _inputController.OnClickLoad += Load;
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }            

            var playerSave = new PlayerModelForSave(_playerModel.ShootDamageForce, _playerModel.Health.HP, 
                                        _playerModel.Health.MaxHP, _playerModel.LifesCount);            

            var saveObjects = new SavedData()
            {
                PlayerSave = playerSave
            };            

            _data.Save(saveObjects, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }

            var savedObjects = _data.Load(file).PlayerSave;                    

            Debug.Log("Load");
        }
    }
}