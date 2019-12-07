#version 330

in vec3 Varying_Normal;

out vec4 Depth_Out;

void main(void)
{
	Depth_Out = vec4(Varying_Position, 1.0);

}
