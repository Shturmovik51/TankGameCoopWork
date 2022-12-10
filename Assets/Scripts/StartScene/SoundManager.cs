using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TankGame;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _menuBG;
    [SerializeField] private AudioSource _gameMainTheme;
    [SerializeField] private AudioSource _click;
    [SerializeField] private AudioSource _endScreenSound;

    private List<AudioSource> _soundsBG = new List<AudioSource>();

    private List<Button> _menuButtons;
    private List<Button> _gameButtons;

    private List<AudioSource> _tanksAudioSources;
    //private List<AudioSource> _cellsAudioSourses;

    private void Awake()
    {
        _soundsBG = new List<AudioSource>()
        {
            _menuBG,
            _gameMainTheme,
        };

        LoadSoundOptions();
    }

    private void LoadSoundOptions()
    {
        if (PlayerPrefs.HasKey($"MusicSoundOptions"))
        {
            SetMusicValue(PlayerPrefs.GetFloat($"MusicSoundOptions"));
        }

        if (PlayerPrefs.HasKey($"EffectsSoundOptions"))
        {
            SetEffectsValue(PlayerPrefs.GetFloat($"EffectsSoundOptions"));
        }
    }

    public void PlayMenuSound()
    {
        ResetMusicBG();
        _menuBG.loop = true;
        _menuBG.Play();
    }

    public void PlayGameMainTheme()
    {
        ResetMusicBG();
        _gameMainTheme.loop = true;
        _gameMainTheme.Play();
    }        

    public void PlayEndScreenSound()
    {
        ResetMusicBG();
        _endScreenSound.Play();
    }

    public void ResetSoundManager()
    {
        _menuButtons = null;
        _gameButtons = null;
        _tanksAudioSources = null;
    }

    private void ResetMusicBG()
    {
        foreach (var sours in _soundsBG)
        {
            sours.Stop();
        }
    }

    public void SubscribeMenuButtons()
    {
        if (_menuButtons != null)
        {
            _menuButtons.Clear();
        }

        _menuButtons = FindObjectsOfType<Button>().ToList();
        foreach (var button in _menuButtons)
        {
            button.onClick.AddListener(() => _click.Play());
            //button.OnPointerEnterEvent += () => _onPointerEnter.Play();
        }
    }

    public void SubscribeGameButtons()
    {
        if (_gameButtons != null)
        {
            _gameButtons.Clear();
        }

        _gameButtons = FindObjectsOfType<Button>().ToList();
        foreach (var button in _gameButtons)
        {
            button.onClick.AddListener(() => _click.Play());
            //button.OnPointerEnterEvent += () => _onPointerEnter.Play();
        }
    }

    public void SetMusicValue(float value)
    {
        _menuBG.volume = value;
        _gameMainTheme.volume = value;
        _endScreenSound.volume = value;
    }

    public void SetEffectsValue(float value)
    {
        _click.volume = value;

        if (_tanksAudioSources != null)
        {
            foreach (var source in _tanksAudioSources)
            {
                source.volume = value;
            }
        }
    }

    public void AddButton(Button button)
    {
        button.onClick.AddListener(() => _click.Play());
    }

    public void AddTanksAudioSources()
    {
        if (_tanksAudioSources != null)
        {
            _tanksAudioSources.Clear();
        }
        else
        {
            _tanksAudioSources = new List<AudioSource>();
        }

        var playerTanks = FindObjectsOfType<PlayerView>().ToList();
        var enemyTanks = FindObjectsOfType<EnemyView>().ToList();

        foreach (var tank in playerTanks)
        {
            _tanksAudioSources.AddRange(tank.GetComponentsInChildren<AudioSource>());
        }
        foreach (var tank in enemyTanks)
        {
            _tanksAudioSources.AddRange(tank.GetComponentsInChildren<AudioSource>());
        }
    }    
}
