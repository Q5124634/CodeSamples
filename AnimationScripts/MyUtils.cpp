#include "MyUtils.hpp"

/*
* Tutorial: use this source and its associated header file to declare
*           and define your own helper functions.
*/

namespace TomBarnabyPass
{

	tyga::Matrix4x4 translate(float x, float y, float z)
	{
		return tyga::Matrix4x4(1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			x, y, z, 1);
	}

	tyga::Matrix4x4 rotateZ(float x)
	{
		const float c = std::cos(x);
		const float s = std::sin(x);

		return tyga::Matrix4x4(c, s, 0, 0,
			-s, c, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1);
	}

	tyga::Matrix4x4 rotateX(float x)
	{
		const float c = std::cos(x);
		const float s = std::sin(x);

		return tyga::Matrix4x4(1, 0, 0, 0,
			0, c, s, 0,
			0, -s, c, 0,
			0, 0, 0, 1);
	}

	tyga::Matrix4x4 rotateY(float x)
	{
		const float c = std::cos(x);
		const float s = std::sin(x);

		return tyga::Matrix4x4(c, 0, -s, 0,
			0, 1, 0, 0,
			s, 0, c, 0,
			0, 0, 0, 1);
	}

	float continuous(float rotation, float time)
	{
		return rotation * time;
	}

	float oscillate(float min_value, float max_value, float time)
	{
		float output{ (sinf(time) + 1) / 2 * (max_value - min_value) + min_value };
		return output;
	}

	tyga::Matrix4x4 scale(float x)
	{
		return tyga::Matrix4x4(x, 0, 0, 0,
			0, x, 0, 0,
			0, 0, x, 0,
			0, 0, 0, 1);
	}

	float clamp(float min, float max, float x)
	{
		if (x < min)
		{
			return min;
		}
		else if (x > max)
		{
			return max;
		}
		else
		{
			return x;
		}
	}

	float linear_step(float min, float max, float x)
	{
		return clamp(0, 1, (x - min) / (max - min));
	}

	float smooth_step(float min, float max, float x)
	{
		x = linear_step(min, max, x);
		return 3 * x * x - 2 * x * x * x;
	}

	float lerp(float a, float b, float s)
	{
		return (a * (1 - s) + b * s);
	}

	tyga::Vector3 euler(tyga::Vector3 p, float t, tyga::Vector3 v)
	{
		return p + t * v;
	}
}