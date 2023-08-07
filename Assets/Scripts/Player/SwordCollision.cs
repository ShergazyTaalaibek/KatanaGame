using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] private GameObject collision;

    public void SetActiveCollision(bool isActive)
    {
        collision.SetActive(isActive);
    }
}
