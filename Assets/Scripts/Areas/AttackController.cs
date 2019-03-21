using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackController : MonoBehaviour {

    public WeaponItem defaultWeapon;

    private bool active;
    private bool canAttack;
    private float lastActive;
    private float lastAttack;
    private List<GameObject> hitList = new List<GameObject>();
    private WeaponItem weapon;

    public bool Attack()
    {
        if (!canAttack)
            return false;

        GetWeapon();

        hitList.Clear();

        active = true;
        canAttack = false;

        lastActive = Time.fixedTime;
        lastAttack = Time.fixedTime;

        return true;
    }

    private void Awake()
    {
        lastActive = Time.fixedTime;
        lastAttack = Time.fixedTime;
    }

    private void GetWeapon()
    {
        weapon = defaultWeapon;

        WeaponEquipment equipment = GetComponentInParent<WeaponEquipment>();
        if (equipment)
        {
            WeaponItem equipmentWeapon = equipment.GetWeapon();
            if (equipmentWeapon)
                weapon = equipmentWeapon;
        }
    }

    private void Hit(Collider2D other)
    {
        if (active && !hitList.Contains(other.gameObject))
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth)
            {
                targetHealth.Damage(weapon.damage, transform.parent.gameObject);
                hitList.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Hit(collision);
    }

    private void Start()
    {
        GetWeapon();
    }

    private void Update()
    {
        if (!canAttack && Time.fixedTime - lastAttack >= weapon.attackRate)
        {
            canAttack = true;
        }

        if (active && Time.fixedTime - lastActive >= weapon.attackDuration)
        {
            active = false;
        }

        if (!PauseController.IsPaused()
            && Input.GetButtonDown("Fire1")
            && canAttack)
        {
            Attack();
        }
    }
}
