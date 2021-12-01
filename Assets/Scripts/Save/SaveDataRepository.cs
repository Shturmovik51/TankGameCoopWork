using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private EnemyModel[] _enemyModels;
        private List<SkillButton> _skillButtons;
        private StartGameParametersManager _startGameParametersManager;

        public SaveDataRepository(InputController inputController, /*PlayerModel playerModel*/ EnemyModel[] enemyModels, 
                    SkillButtonsManager skillButtonsManager, StartGameParametersManager startGameParametersManager)
        {
            _inputController = inputController;
            //_playerModel = playerModel;
            _enemyModels = enemyModels;
            _skillButtons = skillButtonsManager.GetAllSkillButtons();
            _startGameParametersManager = startGameParametersManager;
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

            var playerSave = new PlayerSaveModel(_playerModel.ShootDamageForce, _playerModel.Health.HP, 
                                        _playerModel.Health.MaxHP, _playerModel.LifesCount);

            var enemiesSave = new EnemySaveModel[_enemyModels.Length];
            for (int i = 0; i < _enemyModels.Length; i++)
            {
                enemiesSave[i] = new EnemySaveModel(_enemyModels[i].ShootDamageForce, _enemyModels[i].Health.HP,
                                        _enemyModels[i].Health.MaxHP, _enemyModels[i].AbilityID, _enemyModels[i].IsDead);
            }

            var skillButtonsSave = new SkillButtonSaveModel[_skillButtons.Count];
            for (int i = 0; i < _skillButtons.Count; i++)
            {
                skillButtonsSave[i] = new SkillButtonSaveModel(_skillButtons[i].IsOnCD, _skillButtons[i].CurrentCD);
            }

            var savedData = new SavedData()
            {
                PlayerSave = playerSave,
                EnemiesSave = enemiesSave,
                SkillButtonsSave = skillButtonsSave
            };            

            _data.Save(savedData, Path.Combine(_path, _fileName));
            Debug.Log("Save");
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file))
            {
                throw new DataException($"File {file} not found");
            }

            var savedData = _data.Load(file);
            _startGameParametersManager.InitsavedData(savedData);
            SceneManager.LoadScene(0);

            Debug.Log("Load");
        }
    }
}