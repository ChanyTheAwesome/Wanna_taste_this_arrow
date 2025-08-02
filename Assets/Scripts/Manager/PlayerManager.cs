using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance => instance;

    //[SerializeField] private GameObject[] _characterPrefabs;

    private PlayerController _playerController;

    public PlayerController PlayerController { get { return _playerController; } set { _playerController = value; } }

    //private int selectedIndex = 0;

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
        Debug.Log(_selectedIndex);
        _playerController.GetComponentInChildren<SpriteRenderer>().sprite = characterSprites[_selectedIndex];
        //AnimatorOverrideController animatorOverrideController = new();
        //animatorOverrideController.runtimeAnimatorController = characterAnimators[_selectedIndex];
        //animatorOverrideController["Idle"] = idleAnimationClips[_selectedIndex];
        //animatorOverrideController["Move"] = moveAnimationClips[_selectedIndex];
        //animatorOverrideController["Damage"] = damageAnimationClips[_selectedIndex];
        //nowAnim.runtimeAnimatorController = animatorOverrideController;
        //_playerController.GetComponentInChildren<Animator>(). = animatorOverrideController;
        //nowAnim.runtimeAnimatorController = characterAnimators[_selectedIndex];
        _playerController.GetComponentInChildren<Animator>().runtimeAnimatorController = characterAnimators[_selectedIndex];
    }
}
