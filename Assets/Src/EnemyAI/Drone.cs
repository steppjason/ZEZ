using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
	[SerializeField] float MAX_ATTACK_DELAY = 3;
	[SerializeField] float ATTACK_DAMAGE = 5;

	[SerializeField] EnemyController _enemyController;
	PlayerController _playerController;
	GameController _gameController;
	[SerializeField] CircleCollider2D _visionCollider;
	[SerializeField] CircleCollider2D _hitBoxCollider;

	public bool _isAttacking;
	float _attackDelay;

	private void Start() {
		_playerController = FindObjectOfType<PlayerController>();
		_gameController = FindObjectOfType<GameController>();
	}
    // Update is called once per frame
    void Update()
    {
		Attack();
	}

	private void Attack(){

		if(_isAttacking){
			if(_attackDelay <= 0){
				_playerController.TakeDamage(5);
				_attackDelay = MAX_ATTACK_DELAY;
			}
		}
		_attackDelay -= Time.deltaTime * 1;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(_visionCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = true;
			_enemyController.velocity = (other.transform.position - transform.position).normalized;
		}

		if(_hitBoxCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = false;
			_isAttacking = true;
		} else {
			_isAttacking = false;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(!_visionCollider.IsTouching(other)){
			_enemyController.isChasingPlayer = false;
		}
	}
}
