using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwooshTest : MonoBehaviour
{
    [SerializeField]
	AnimationClip _animation;
	AnimatorStateInfo _animationStateInfo;
	
	[SerializeField]
	int _start = 0;
	
	[SerializeField]
	int _end = 0;
	
	float _startN = 0.0f;
	float _endN = 0.0f;
	
	float _time = 0.0f;
	float _prevTime = 0.0f;
	float _prevAnimTime = 0.0f;
		
	[SerializeField]
	WeaponTrail _trail;
	
	bool _firstFrame = true;
	
	void Start()
	{
        _animation = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip;
		float frames = _animation.frameRate * _animation.length;
		_startN = _start/frames;
		_endN = _end/frames;
		_animationStateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
	}
	
	void Update()
	{
        if(GetComponentInChildren<WeaponTrail>() != null && _trail == null){
            _trail = GetComponentInChildren<WeaponTrail>();
            _trail.Emit = false;
        }else{
            // _time += _animationStateInfo.normalizedTime - _prevAnimTime;
            // if (_time > 1.0f || _firstFrame)
            // {
            // 	if (!_firstFrame)
            // 	{
            // 		_time -= 1.0f;
            // 	}
            // 	_firstFrame = false;
            // }
            
            // if (_prevTime < _startN && _time >= _startN)
            // {
            // 	_trail.Emit = true;
            // }
            // else if (_prevTime < _endN && _time >= _endN)
            // {
            // 	_trail.Emit = false;
            // }

            _prevTime = _time;
            _prevAnimTime = _animationStateInfo.normalizedTime;
        }
	}
}
