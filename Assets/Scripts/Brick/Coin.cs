using UnityEngine;

public class Coin : MonoBehaviour, IEffector
{
    [SerializeField] int amount = 1;
    
    public void ApplyEffectOn(in GameJamCharacter player)
    {
        PickUpCoin();
    }

    private void PickUpCoin()
    {
        GameManager.Instance.CurrentLevel.AddCredits(amount);
        Destroy(gameObject);
    }
}
