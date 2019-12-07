#pragma once

#include <tyga/Math.hpp>

/*
* Tutorial: use this header and its associated source file to declare
*           and define your own helper functions.
*/

namespace TomBarnabyPass
{


	/**
	* Makes a translation transformation matrix.
	*
	*x The amount of X translation.
	* y The amount of Y translation.
	* z The amount of Z translation.
	* A 4x4 transformation matrix for use with row-vectors.
	*/
	tyga::Matrix4x4 translate(float x, float y, float z);


	/**
	* Makes a rotation transformation matrix.
	*
	* x The angle in radians of rotation.
	* Uses sin and cos functions to rotate around the z-axis to the angle @param x.
	*  A 4x4 transformation matrix for use with row-vectors.
	*/
	tyga::Matrix4x4 rotateZ(float x);


	/**
	* Makes a rotation transformation matrix.
	*
	* x The angle in radians of rotation.
	* Uses sin and cos functions to rotate around the x-axis to the angle @param x.
	* A 4x4 transformation matrix for use with row-vectors.
	*/
	tyga::Matrix4x4 rotateX(float x);


	/**
	* Makes a rotation transformation matrix.
	*
	* x The angle in radians of rotation.
	* Uses sin and cos functions to rotate around the y-axis to the angle @param x.
	* A 4x4 transformation matrix for use with row-vectors.
	*/
	tyga::Matrix4x4 rotateY(float x);


	/**
	* Makes a rotation transformation matrix that is continuous.
	*
	* rotation The angle that will be applied to the actor.
	* time The time between each implementation of the angle.
	* A value to be used for a continuous rotation.
	*/
	float continuous(float rotation, float time);


	/**
	* Makes an oscillation.
	*
	* min_value The minimum value of the oscillation.
	* max_value The maximum value of the oscillation.
	*  time The time between each implementation of the oscillation.
	An output value to oscillate and uses sinf to create this effect.
	*/
	float oscillate(float min_value, float max_value, float time);


	/**
	* Makes a scale transformation matrix.

	* x The size of the scale.
	* Uses a matrix to change the scale values.
	* A 4x4 transformation matrix for use with row-vectors.
	*/
	tyga::Matrix4x4 scale(float x);


	/**
	* A clamp function.
	*for clamping
	*
	*/
	float clamp(float min, float max, float x);


	/**
	* A linear step function.
	*for quick movement
	*
	*/
	float linear_step(float min, float max, float x);


	/**
	* A smooth step function.
	* this is a smoother linier interpilation
	*
	*/
	float smooth_step(float min, float max, float x);


	/**
	* A lerp function.
	*this function allows for linier enterpilation
	*
	*/
	float lerp(float a, float b, float s);


	/**
	* A euler equation.
	*
	*
	*/
	tyga::Vector3 euler(tyga::Vector3 p, float t, tyga::Vector3 v);
}
