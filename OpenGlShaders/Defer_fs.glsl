#version 330

//pulling the object colour and the texture from the cpp
uniform vec3 Object_Colour;
uniform sampler2D sponza_textures;

in vec3 Varying_Normal;
in vec3 Varying_Position;
in vec2 Varying_Textcoord;

out vec4 Position_Out;
out vec4 Normal_Out;
out vec4 Material_Out;
out vec4 Texture_Out;

void main(void)
{
	Position_Out = vec4(Varying_Position, 1.0);
	Normal_Out = vec4(Varying_Normal, 1.0);
	Material_Out = vec4(Object_Colour, 1.0);
	vec3 Set_Tex = (texture(sponza_textures, Varying_Textcoord).rgb);
	Texture_Out = vec4(Set_Tex, 1.0);
}
