using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour 
{

    public Animator m_anim;
    public SpriteRenderer m_sprite;
    public BoxCollider2D m_bc;
    public Enemy m_enemy;

    public AttackType m_AttackType;

    public float m_dist;
    public bool m_AlreadyAttacking;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();
        m_bc = GetComponent<BoxCollider2D>();
        m_anim = GetComponent<Animator>();
        m_enemy = GetComponent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {

        if (m_enemy.Follow) m_dist = Vector3.Distance(transform.position, m_enemy.HitInfo[0].transform.position);
        if (m_dist <= 4f && !m_AlreadyAttacking && m_enemy.Follow) m_AttackType = (AttackType)Random.Range(1, 3);
        if (m_enemy.Follow) StartCoroutine(Attacks());



        m_anim.SetBool("IsDeath", m_enemy.EnemyLife <= 0);
        m_sprite.flipX = !(m_enemy.Direction);

        //Makes the collider2D follow the sprite size during animations
        Vector2 S = m_sprite.bounds.size;
        m_bc.size = S;
        if (m_sprite.flipX == false) m_bc.offset = new Vector2(S.x / 2, S.y / 2);
        if (m_sprite.flipX == true) m_bc.offset = new Vector2(-S.x / 2, S.y / 2);
    }

    //enemy's attack logic
    IEnumerator Attacks()
    {
        m_AlreadyAttacking = true;
        if(m_AttackType == AttackType.Melee && m_dist > 3.22f)
        {
            m_anim.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(1f);
            m_anim.SetBool("IsAttacking", false);
        }
        else if(m_AttackType == AttackType.skill && m_dist > 3.22f)
        {
            m_anim.SetBool("IsSkilling", true);
            yield return new WaitForSeconds(1f);
            m_anim.SetBool("IsSkilling", false);
        }
        m_AttackType = AttackType.none;
        m_AlreadyAttacking = false;
    }

    public enum AttackType
    {
        Melee = 1,
        skill = 2,
        none = 0
    }


}
