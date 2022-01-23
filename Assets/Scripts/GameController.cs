using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _victoryMenu;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private PlayerController _player;
    [SerializeField] private TextMeshProUGUI _timeCounter;
    [SerializeField] private float _musicVolumeWhenPaused = .3f;

    private float _timeElapsed;

    private void Start()
    {
        StartGame();
    }

    private IEnumerator CountTime()
    {
        while (true)
        {
            _timeCounter.text = $"{_timeElapsed}s";
            yield return new WaitForSeconds(1);
            _timeElapsed += 1;
        }
    }

    public void StartGame()
    {
        _timeElapsed = 0; 
        StartCoroutine(CountTime());
        Time.timeScale = 1;
        _deathMenu.SetActive(false);
        _victoryMenu.SetActive(false);
        _player.Revive();
        _player.gameObject.SetActive(true);
        _player.transform.position = _spawnPoint.position;
    }

    public void HandleDeath()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        _deathMenu.SetActive(true);
    }

    public void HandleVictory()
    {
        StopAllCoroutines();
        Time.timeScale = 0;
        _victoryMenu.SetActive(true);
    }

    public void TogglePause()
    {
        if (_pauseMenu is null) return;
        var pauseActive = _pauseMenu.activeSelf;
        _pauseMenu.SetActive(!pauseActive);
        Time.timeScale = pauseActive ? 1 : 0;
        FindObjectOfType<MusicPlayer>().SetVolume(pauseActive ? 1 : _musicVolumeWhenPaused);
    }
}