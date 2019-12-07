#version 330

//pulling the position, normal, material and texture from the cpp
uniform sampler2DRect sampler_world_position;
uniform sampler2DRect sampler_world_normal;
uniform sampler2DRect sampler_world_material;
uniform sampler2DRect sampler_world_texture;

//pulling the lights intensity and directionthe cpp
uniform vec3 light_direction;
uniform vec3 light_intensity;

//sending out the reflected lights reletive to the material it hits
out vec3 Reflected_Light_Intensity;

void main(void)
{
	//setting values to use 
	vec3 Diffuse_Intensity = vec3(0, 0, 0);
	vec3 L;
	vec3 Texturing;

	//fetching the values from the samplers to use in the below equasion
	vec3 texel_N = texelFetch(sampler_world_normal, ivec2(gl_FragCoord.xy)).rgb;
	vec3 texel_P = texelFetch(sampler_world_position, ivec2(gl_FragCoord.xy)).rgb;
	vec3 Object_Colour = texelFetch(sampler_world_material, ivec2(gl_FragCoord.xy)).rgb;
	vec3 tex = texelFetch(sampler_world_texture, ivec2(gl_FragCoord.xy)).rgb;
	vec3 N = normalize(texel_N);

	//calculating the values for the Directional Lights leading to the calculation for the difuse intensity
	L = normalize(-light_direction);
	Diffuse_Intensity += max(0, dot(L, N)) * light_intensity * 3.5;

	Texturing = Diffuse_Intensity * tex * Object_Colour;

	Reflected_Light_Intensity = Texturing;
}
