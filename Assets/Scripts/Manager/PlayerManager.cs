using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance => instance;

    [SerializeField] private GameObject characterPrefab;

    public PlayerController _playerController;  // �׽�Ʈ������  public ������ private

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
    public int RequiredExp => Level * 10 + 50;  // �ϴ� �⺻ 50 + ������ 10���� ����

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

    public void GetEXP(int exp) // ����ġ ȹ�� �� ���� �� �����ϱ�
    {
        Exp += exp;
        LevelUpCheck();
    }

    public void LevelUpCheck()  // ������ Ȯ�� �� ���� ���
    {
        if(Exp >= RequiredExp)
        {
            Exp -= RequiredExp;
            Level++;

            List<AbilityData> randomAbilities = _abilityController.GetRandomAbility(3);
            abilitySelectUI.ShowSelect(randomAbilities, _playerController);

        }
    }

    public void ResetPlayer()    // ����, ����ġ, �ɷ� �ʱ�ȭ, �������� ���� �� �����ϱ�
    {
        Level = 1;
        Exp = 0;
        // �ɷ� �ʱ�ȭ
        // �ɷ� �ʱ�ȭ�� �ɷ��� ��� ����Ǵ����� ���� ���缭 ����
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
