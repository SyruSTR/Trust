using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Arsenal_Controller : MonoBehaviour
{
    public Transform rightBone;
    public Transform leftBone;
    public List<Arsenal> arsenal;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (arsenal.Count > 0)
        {
            SetArsenal(arsenal[0].name);
        }

    }

    public void SetArsenal(string name)
    {
        var weapon = arsenal.Find(w => w.name == name);

        if (weapon.name != name)
            return;

        if (rightBone.childCount > 0)
        {
            Destroy(rightBone.GetChild(0).gameObject);
        }
        if (leftBone.childCount > 0)
        {
            Destroy(leftBone.GetChild(0).gameObject);
        }
        if (weapon.rightWeapon != null)
        {
            GameObject newRightWeapon = (GameObject)Instantiate(weapon.rightWeapon);
            newRightWeapon.transform.parent = rightBone;
            newRightWeapon.transform.localPosition = weapon.positionR;
            newRightWeapon.transform.localRotation = Quaternion.Euler(weapon.rotationR);
        }
        if (weapon.leftWeapon != null)
        {
            GameObject newLeftWeapon = (GameObject)Instantiate(weapon.leftWeapon);
            newLeftWeapon.transform.parent = leftBone;
            newLeftWeapon.transform.localPosition = weapon.positionL;
            newLeftWeapon.transform.localRotation = Quaternion.Euler(weapon.rotationL);
        }

        animator.runtimeAnimatorController = weapon.controller;
    }

    [System.Serializable]
    public struct Arsenal
    {
        public string name;
        public GameObject rightWeapon;
        public GameObject leftWeapon;
        public RuntimeAnimatorController controller;
        [Header("RightWeapon Transform")]
        public Vector3 positionR;
        public Vector3 rotationR;
        [Header("RightWeapon Transform")]
        public Vector3 positionL;
        public Vector3 rotationL;
    }
}
