using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _distanceFromTarget = 10;
    [SerializeField] GameObject[] _cops;
    [SerializeField] ParticleSystem _explosionFx;

    bool _isPlaying = false;
    int _score;
    bool _isRevived = false;
    float _timer;

    MenuManager _menuController;

    public static event Action<int> OnUpdateScore;

    private void Awake()
    {
        _menuController = MenuManager.GetInstance();
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerDeath += HandleEndScreen;
        AdsManager.OnRewardedAdWatchedComplete += ContinueGameplay;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerDeath -= HandleEndScreen;
        AdsManager.OnRewardedAdWatchedComplete -= ContinueGameplay;
    }

    private void Update()
    {
        if (Time.time > _timer)
        {
            AddScore(1);
            _timer++;
        }
    }

    #region Public Methods
    public int GetScore()
    {
        return _score;
    }

    public void IsPlaying(bool val)
    {
        _isPlaying = val;

        if (_isPlaying)
            StartCoroutine(GenerateCops());
        else
            StopAllCoroutines();
    }

    public void PlayExplosionFx(Vector3 position)
    {
        GameObject go = Instantiate(_explosionFx.gameObject, position, Quaternion.identity);
        Destroy(go, 2f);

        // play explosion clip
        SoundManager.GetInstance().PlayAudio(AudioType.EXPLOSION, position);
    }

    public void AddScore(int score)
    {
        if (!_isPlaying) return;

        _score += score;
        OnUpdateScore?.Invoke(_score);
    }
    #endregion

    #region Private methods
    private void ContinueGameplay()
    {
        _menuController.CloseMenu();
        _menuController.SwitchMenu(MenuType.Gameplay);

        IsPlaying(true);
        _target.gameObject.GetComponent<PlayerMovement>().Revive();
    }

    void SpawnEnemy()
    {
        int upAngle = UnityEngine.Random.Range(-30, 30);
        int botAngle = UnityEngine.Random.Range(160, 200);
        int randomAngle = UnityEngine.Random.value < .5f ? upAngle : botAngle;

        Vector3 dir = new Vector3(Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0, Mathf.Cos(randomAngle * Mathf.Deg2Rad));
        Vector3 enemyPosition = _target.position + dir * _distanceFromTarget;
        GameObject enemy = Instantiate(GetCops(), enemyPosition, Quaternion.Euler(Vector3.up * (randomAngle + 180)));
        enemy.transform.SetParent(transform);
    }

    GameObject GetCops()
    {
        float number = UnityEngine.Random.Range(0, 100);

        // - calculate percentage base on score
        int percentage = 50;
        if (_score < 50) percentage = 100;
        else if (_score >= 50 && _score < 120) percentage = 90;
        else if (_score >= 120 && _score < 200) percentage = 80;
        else if (_score >= 200 && _score < 400) percentage = 70;
        else if (_score >= 600 && _score < 600) percentage = 60;

        return number < percentage ? _cops[0] : _cops[1];
    }

    IEnumerator GenerateCops()
    {
        _timer = Time.time + 1f;

        yield return new WaitForSeconds(1f);

        while (true)
        {
            int enemyCount = 3;
            if (_score < 50) enemyCount = 1;
            else if (_score >= 50 && _score < 200) enemyCount = 2;


            int randomCount = UnityEngine.Random.Range(0, enemyCount);
            for (int i = 0; i < randomCount + 1; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(2f);
        }
    }
    #endregion

    #region Event handler
    void HandleEndScreen()
    {
        IsPlaying(false);

        AdsManager.GetInstance().ShowInterstitial();

        _menuController.SwitchMenu(MenuType.EndScreen);

        bool isRewardLoaded = AdsManager.GetInstance().IsRewardedAdLoaded();

        if (!_isRevived && isRewardLoaded)
        {
            _isRevived = true;
            _menuController.OpenMenu(MenuType.ReviveMenu);
        }
        else
        {
            _menuController.OpenMenu(MenuType.Gameover);
        }
    }
    #endregion
}
