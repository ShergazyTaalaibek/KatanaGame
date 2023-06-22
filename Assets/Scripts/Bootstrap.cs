using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private PlayerPerson _playerPerson;
    [SerializeField] private EnemyPerson _enemyPerson;

    private void Awake()
    {
        _playerPerson?.Initialize();
        _enemyPerson?.Initialize();
    }
}
