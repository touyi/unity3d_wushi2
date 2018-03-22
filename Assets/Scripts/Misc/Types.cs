using UnityEngine;
using System.Collections;

/// <summary>
/// E_ motion type.
/// 运动类型
/// </summary>
public enum E_MotionType
{
	None,
	Walk,   // 走
	Run,    // 跑
	Sprint, // 冲刺
	Attack,
}

/// <summary>
/// E_ move type.
/// </summary>
public enum E_MoveType
{
	None,
	Forward,
	Backward,
	StrafeLeft,
	StrafeRight,
}

public enum E_WeaponType 
{
	None = -1,
	Katana = 0,
	Body,
	Bow,
	Max,
}


public enum E_WeaponState
{
	NotInHands,
	Ready,
	Attacking,
	Reloading,
	Empty,
}

public enum E_AttackType
{
		None = -1,
		X = 0,
		O = 1,
		Count,
}

