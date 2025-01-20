using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int startLife;
    [SerializeField] int startMoney;

    public static GameManager Instance { get; private set; }

    int life;
    int money;

    public int Life 
    {
        get => life;

        set 
        {
            life = value;

            if (life <= 0)
            {
                Debug.Log("GAME OVER!");
                Time.timeScale = 0;
            }
        }    
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        life = startLife;
        money = startMoney;
    }

}
