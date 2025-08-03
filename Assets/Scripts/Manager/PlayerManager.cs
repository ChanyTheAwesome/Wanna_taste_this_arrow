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
        Debug.Log(_selectedIndex + "�� ĳ���ͷ� ����");
        _playerController.GetComponentInChildren<SpriteRenderer>().sprite = characterSprites[_selectedIndex];
        _playerController.GetComponentInChildren<Animator>().runtimeAnimatorController = characterAnimators[_selectedIndex];
    }

    public void InitPlayer()
    {
        Debug.Log("�÷��̾� ���� �õ�");
        GameObject player = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();
        _playerController = playerController;
        _abilityController = _playerController.GetComponent<PlayerAbilityController>();
        abilitySelectUI.gameObject.SetActive(false);
        //if (!DungeonManager.Instance.IsFirstStage)
        //{
        //    SetCharacter();
        //}
    }
}
