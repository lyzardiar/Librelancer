﻿in vec3 vertex_position;
in vec3 vertex_normal;
in vec2 vertex_texture1;

out vec2 frag_texcoord;
out vec3 N;
out vec3 V;
out vec3 world_position;
out vec3 out_normal;

uniform mat4x4 World;
uniform mat4x4 View;
uniform mat4x4 ViewProjection;
uniform mat4x4 NormalMatrix;
uniform vec3 CameraPosition;

void main()
{
	vec4 pos = (ViewProjection * World) * vec4(vertex_position, 1.0);
	gl_Position = pos;

	vec4 p = vec4(vertex_position, 1.0);

	N = normalize(mat3(View * World) * vertex_normal);
	V = normalize(-vec3((View * World) * p));
	world_position = (World * vec4(vertex_position,1)).xyz;
	out_normal = (NormalMatrix * vec4(vertex_normal,0)).xyz;
}
