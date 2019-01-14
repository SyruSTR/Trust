using UnityEngine;

public class IK_Controller : MonoBehaviour
{
    public float offset;
    public bool isActiveIK = true;

    [Space]
    public float rayDistance = 5f;
    public float speedIK = 10f;

    private Animator animator;

    private Vector3 leftFootTargetPos;
    private Vector3 rightFootTargetPos;

    private Quaternion leftFootTargetRot;
    private Quaternion rightFootTargetRot;

    private float leftFootWeight;
    private float rightFootWeight;

    private Transform leftFoot;
    private Transform rightFoot;

    private bool isGround;
    [Space]
    public float kneeHeight = 0.5f;
    public float lookIKWeight;
    public float headWeight;
    public float bodyWeight;
    public float clampWeight;
    public Transform target;

    void Start()
    {
        animator = GetComponent<Animator>();

        leftFoot = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        leftFootTargetRot = leftFoot.rotation;
        rightFoot = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        rightFootTargetRot = rightFoot.rotation;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (!isActiveIK)
        {
            return;
        }

        isGround = true;

        CheckFootPosition(leftFoot.position, ref leftFootTargetPos, ref leftFootTargetRot);
        CheckFootPosition(rightFoot.position, ref rightFootTargetPos, ref rightFootTargetRot);

        void CheckFootPosition(Vector3 _footCurrentPosition, ref Vector3 _footNextPosition, ref Quaternion _footNextRotation)
        {
            RaycastHit hit;
            if (Physics.Raycast(_footCurrentPosition + Vector3.up * kneeHeight, Vector3.down, out hit, rayDistance))
            {
                _footNextPosition = Vector3.Lerp(_footCurrentPosition, hit.point + Vector3.up * offset, Time.deltaTime * 10f);
                _footNextRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                Debug.DrawLine(_footCurrentPosition, _footNextPosition, Color.blue);
            }
            else
                isGround = false;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!isActiveIK)
            return;

        if (!isGround)
            return;
        if (target != null)
        {
            animator.SetLookAtWeight(lookIKWeight,bodyWeight,headWeight,0,clampWeight);
            animator.SetLookAtPosition(target.position + new Vector3(0,0.6f,0));
        }
        if (target.name == "ShootTarget")
        {

        }
        float leftWeight = animator.GetFloat("leftFoot");
        float rightWeight = animator.GetFloat("rightFoot");

        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftWeight);

        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightWeight);

        animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootTargetPos);
        animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootTargetRot);

        animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootTargetPos);
        animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootTargetRot);
        //if (target.name == "ShootTarget") ;
    }
    //public void WaitCall()
    //{
    //    if (isActiveIK)
    //        return;
    //    if (isWaitCall)
    //        return;
    //    isWaitCall = true;
    //    Invoke("ActivateIK", 1f);
    //}
    //private void ActivateIK()
    //{
    //    isActiveIK = true;
    //    isWaitCall = false;
    //    Debug.Log("ActivateIK");
    //}
    //public void DeactivateIK()
    //{
    //    Debug.Log("DeactivateIK");
    //    isActiveIK = false;
    //    if (isWaitCall)
    //    {
    //        CancelInvoke("ActivateIK");
    //        isWaitCall = false;
    //    }
    //}
}
