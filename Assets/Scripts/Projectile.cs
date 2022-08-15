using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _speed;

    private Character _target;

    private UnityAction _hitCallback;


    public void Initialize(Character projectileTarget, UnityAction onHitCallback)
    {
        _target = projectileTarget;
        _hitCallback = onHitCallback;
    }


    void Update()
    {
        if (_target == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);

        if (transform.position == _target.transform.position)
        {
            _target.TakeDamage(_damage);
            _hitCallback?.Invoke();
            Destroy(gameObject);
        }
    }
}
