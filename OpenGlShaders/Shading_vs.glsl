#version 330

layout(std140) uniform PerModelUniforms
{
	uniform mat4 combined_xform;
	uniform mat4 model_xform;
};

in vec3 vertex_position;
in vec3 vertex_normal;
in vec2 vertex_textcoord;

out vec3 Varying_Normal;
out vec2 Varying_Textcoord;
out vec3 Varying_Position;

void main(void)
{
	Varying_Position = mat4x3(model_xform) * vec4(vertex_position, 1.0);
	Varying_Textcoord = vertex_textcoord;
	Varying_Normal = (mat3(model_xform) * vertex_normal);
	gl_Position = combined_xform * vec4(vertex_position, 1.0);
}
