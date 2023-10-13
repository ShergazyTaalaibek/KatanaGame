using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private GameObject collision;

    public void SetActiveCollision(bool isActive)
    {
        collision.SetActive(isActive);
    }
}
