using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

public class EnemyAttacker : MonoBehaviour
{
    public Transform animationTransform;
    void Attack()
    {
        var attackSequence = DOTween.Sequence();
        attackSequence.AppendInterval(0.05f);
        attackSequence.AppendCallback(TryToHit);
        
        var animationSequence = DOTween.Sequence();
        animationSequence.Append(animationTransform.DOLocalRotate(Vector3.right * -50, 0.6f).SetEase(Ease.OutSine));
        animationSequence.Join(animationTransform.DOScale(new Vector3(5.0f, 5.0f, 1.0f),0.6f));
        animationSequence.Append(animationTransform.DOLocalRotate(new Vector3(70, 0, 0), 0.2f).SetEase(Ease.InSine));
        animationSequence.Join(attackSequence);
        animationSequence.Join(animationTransform.DOScale(Vector3.one,0.2f));
        animationSequence.Append(animationTransform.DOLocalRotate(Vector3.zero, 0.4f).SetEase(Ease.InCubic));
        animationSequence.SetTarget(animationTransform);

    }
    
    void StopAttack()
    {
        // animation stopped where it is currently
        //animationSequence.Kill();
        // animation complete and stop
        animationTransform.DOComplete();
    }

    void TryToHit()
    {
        Debug.Log("success");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StopAttack();
        }
    }
}