using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance => instance;

    [SerializeField] private GameObject characterPrefab;

    public PlayerController _playerController;  // 테스트용으로  public 끝나면 private

    public PlayerController PlayerController { get { return _playerController; } set { _playerController = value; } }

    private PlayerAbilityController _abilityController;
    public PlayerAbilityController AbilityController { get { return _abilityController; } set { _abilityController = value; } }
    //private int selectedIndex = 0;
    [SerializeField] private AbilitySelectUI abilitySelectUI;
    public AbilitySelectUI AbilitySelectUI { get { return abilitySelectUI; } set { abilitySelectUI = value; } }
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private RuntimeAnimatorController[] characterAnimators;
    //[SerializeField] private AnimationClip[] idleAnimationClips;
    //[SerializeField] private AnimationClip[] moveAnimationClips;
    //[SerializeField] private AnimationClip[] damageAnimationClips;

    //public Animator nowAnim;
    private int _selectedIndex;
    public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; } }

    public int Exp { get; private set; }
    public int Level { get; private set; }
    public int RequiredExp => Level * 10 + 50;  // 일단 기본 50 + 레벨당 10으로 설정

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetEXP(30);
        }
    }

    public void GetEXP(int exp) // 경험치 획득 적 죽을 때 실행하기
    {
        Exp += exp;
        LevelUpCheck();
    }

    public void LevelUpCheck()  // 레벨업 확인 및 레벨 상승
    {
        if(Exp >= RequiredExp)
        {
            Exp -= RequiredExp;
            Level++;

            List<AbilityData> randomAbilities = _abilityController.GetRandomAbility(3);
            abilitySelectUI.ShowSelect(randomAbilities, _playerController);

        }
    }

    public void ResetPlayer()    // 레벨, 경험치, 능력 초기화, 던전에서 나갈 때 실행하기
    {
        Level = 1;
        Exp = 0;
        // 능력 초기화
        // 능력 초기화는 능력이 어떻게 적용되는지에 따라 맞춰서 설정
    }

    public void SetCharacter()
    {
        _playerController.GetComponentInChildren<SpriteRenderer>().sprite = characterSprites[_selectedIndex];
        _playerController.GetComponentInChildren<Animator>().runtimeAnimatorController = characterAnimators[_selectedIndex];
    }

    public void InitPlayer()
    {
        GameObject player;
        if(GameManager.Instance.player == null)
        {
            player = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            GameManager.Instance.player = player;
            DontDestroyOnLoad(player);
        }
        else
        {
            player = GameManager.Instance.player;
            GameManager.Instance.player.transform.position = new Vector3(0, 0, 0);
        }
        
        //PlayerController playerController = player.GetComponent<PlayerController>();
        //PlayerController playerController = GameManager.Instance.player.GetComponent<PlayerController>();
        //_playerController = playerController;
        _playerController = player.GetComponent<PlayerController>();
        //_abilityController = _playerController.GetComponent<PlayerAbilityController>();
        _abilityController = player.GetComponent<PlayerAbilityController>();

        if(GameManager.Instance.StageCount > 1)
        {
            _playerController.NextStageEntryInitailize();
        }
        else
        {
            _playerController.FirstStageAbilityInit();
        }
        FindObjectOfType<ResourceController>()?.LoadCurrentHealth();
        //abilitySelectUI.gameObject.SetActive(false);
        //if (!DungeonManager.Instance.IsFirstStage)
        //{
        //    SetCharacter();
        //}
    }
}
