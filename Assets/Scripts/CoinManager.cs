using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject[] coinImgs;

    public int NumCoinsCollected { get; private set; }
    public static CoinManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ResetCoins()
    {
        foreach (GameObject coin in coinImgs) coin.SetActive(false);
        NumCoinsCollected = 0;
    }

    public void GotCoin()
    {
        if (NumCoinsCollected >= coinImgs.Length) return;
        
        coinImgs[NumCoinsCollected].SetActive(true);
        NumCoinsCollected++;
    }
}
