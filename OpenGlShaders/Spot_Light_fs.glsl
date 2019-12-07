#version 330

//pulling the position, normal, material and texture from the cpp
uniform sampler2DRect sampler_world_position;
uniform sampler2DRect sampler_world_normal;
uniform sampler2DRect sampler_world_material;
uniform sampler2DRect sampler_world_texture;

//pulling the lights position, range, intensity, direction and angle from the cpp
uniform vec3 spot_light_position;
uniform float spot_light_range;
uniform vec3 spot_light_intensity;
uniform vec3 spot_light_direction;
uniform float spot_light_cone_angle_degrees;

//sending out the reflected lights reletive to the material it hits
out vec3 Reflected_Light_Intensity;

void main(void)
{
	//setting values to use 
	vec3 Diffuse_Intensity = vec3(0, 0, 0);
	vec3 L;
	float D;
	float FD;

	vec3 Texturing;

	//fetching the values from the samplers to use in the below equasion
	vec3 texel_N = texelFetch(sampler_world_normal, ivec2(gl_FragCoord.xy)).rgb;
	vec3 texel_P = texelFetch(sampler_world_position, ivec2(gl_FragCoord.xy)).rgb;
	vec3 Object_Colour = texelFetch(sampler_world_material, ivec2(gl_FragCoord.xy)).rgb;
	vec3 tex = texelFetch(sampler_world_texture, ivec2(gl_FragCoord.xy)).rgb;
	vec3 N = normalize(texel_N);

	//calculating the values for the spotlights position, distance, its smoothstep when moving leading to the calculation for the difuse intensity
	L = normalize(spot_light_position - texel_P);
	D = distance(spot_light_position, texel_P);
	float smoothfd = 1 - smoothstep(0, spot_light_range * 0.35f, D);
	float fc = smoothstep(cos(0.5 * spot_light_cone_angle_degrees), 1, dot(-L, spot_light_direction));
	Diffuse_Intensity += smoothfd *  fc * max(dot(L, N), 0.0) * spot_light_intensity * 1.2f;
	//texturing uses the difuse intensity the texture and its colour
	Texturing = Diffuse_Intensity * tex * Object_Colour;
	//output the reflected light
	Reflected_Light_Intensity = Texturing;
}
