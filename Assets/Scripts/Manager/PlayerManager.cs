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
