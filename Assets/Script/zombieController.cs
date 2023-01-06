using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class zombieController : NetworkBehaviour
{
    [SerializeField]
    public int Hp = 100;
    [SerializeField]
    private int maxHp = 100;

    public Animator networkAnimator;
    [SerializeField]
    private Image hpBar = null;
    //public GameObject Coins;

    public void Start()
    {
        //networkAnimator = GetComponentInChildren<NetworkMecanimAnimator>();
        Hp = maxHp;
        Debug.Log("hp init");
    }

    private void Update()
    {
        //healthBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        Hp -= damageAmount;
        OnHpChanged();
        if (Hp <= 0)
        {
            networkAnimator.SetTrigger("die");
            //GetComponent<Collider>().enabled = false;
            
        }
    }
    private void OnHpChanged()
    {
        hpBar.fillAmount = (float)Hp / maxHp;
    }
}
