// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Screen_RedFX"
{
	Properties
	{
		_Main_Tex("Main_Tex", 2D) = "white" {}
		_Scale("Scale", Float) = 1
		_Color0("Color 0", Color) = (1,1,1,1)
		_Alpha("Alpha", Float) = 1
		_TillingOffect("Tilling Offect", Vector) = (1,1,0,0)
		_MaskTillingOffect("Mask Tilling Offect", Vector) = (1,1,0,0)
		_Panner("Panner", Vector) = (1,1,0,0)
		_RGBA_Mask("RGBA_Mask", Vector) = (1,0,0,0)
		_mask("mask", 2D) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" "Queue"="Transparent+4001" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest Always
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _Main_Tex;
			uniform half4 _Panner;
			uniform half4 _TillingOffect;
			uniform half4 _Color0;
			uniform half _Scale;
			uniform half4 _RGBA_Mask;
			uniform half _Alpha;
			uniform sampler2D _mask;
			uniform half4 _MaskTillingOffect;
			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half mulTime40 = _Time.y * _Panner.z;
				half2 appendResult39 = (half2(_Panner.x , _Panner.y));
				float4 screenPos = i.ase_texcoord1;
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				half4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
				half2 appendResult58 = (half2(ase_grabScreenPosNorm.r , ase_grabScreenPosNorm.g));
				half2 temp_output_59_0 = (appendResult58*2.0 + -1.0);
				half2 break62 = temp_output_59_0;
				half2 appendResult65 = (half2(length( temp_output_59_0 ) , (0.0 + (atan2( break62.y , break62.x ) - 0.0) * (1.0 - 0.0) / (3.141593 - 0.0))));
				half2 appendResult36 = (half2(_TillingOffect.x , _TillingOffect.y));
				half2 appendResult37 = (half2(_TillingOffect.z , _TillingOffect.w));
				half2 panner32 = ( ( mulTime40 + _Panner.w ) * appendResult39 + (appendResult65*appendResult36 + appendResult37));
				half4 tex2DNode1 = tex2D( _Main_Tex, panner32 );
				half2 appendResult35 = (half2(ase_grabScreenPosNorm.r , ase_grabScreenPosNorm.g));
				half2 appendResult52 = (half2(_MaskTillingOffect.x , _MaskTillingOffect.y));
				half2 appendResult51 = (half2(_MaskTillingOffect.z , _MaskTillingOffect.w));
				half4 appendResult28 = (half4(( tex2DNode1 * _Color0 * _Scale ).rgb , ( ( ( _RGBA_Mask.x * tex2DNode1.r ) + ( _RGBA_Mask.y * tex2DNode1.g ) + ( _RGBA_Mask.z * tex2DNode1.b ) + ( _RGBA_Mask.w * 0.0 * tex2DNode1.a ) ) * _Alpha * ( tex2D( _mask, (appendResult35*appendResult52 + appendResult51) ).r * 1.0 ) )));
				
				
				finalColor = appendResult28;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
220;122;1444;989;1085.985;191.93;1.378527;True;False
Node;AmplifyShaderEditor.GrabScreenPosition;30;-3213.535,-320.4694;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;58;-2921.994,-198.1103;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;59;-2718.994,-195.1103;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;62;-2483.577,94.62112;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ATan2OpNode;63;-2291.577,90.62112;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;64;-2152.577,64.62112;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;3.141593;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;60;-2363.892,-207.8693;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;38;-1603.975,197.8676;Inherit;False;Property;_Panner;Panner;6;0;Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;34;-1688.476,-89.43251;Inherit;False;Property;_TillingOffect;Tilling Offect;4;0;Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;40;-1334.876,266.7677;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-1450.576,-124.5325;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;37;-1458.376,10.66753;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;65;-1896.178,-50.60208;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;50;-1091.849,646.4945;Inherit;False;Property;_MaskTillingOffect;Mask Tilling Offect;5;0;Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;33;-1207.634,-227.5859;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-1150.276,273.2674;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;39;-1290.675,154.9674;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-853.9492,611.3945;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;51;-861.7491,746.5945;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;35;-1540.839,-252.103;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;32;-939.6755,-58.23306;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-632,-110.5;Inherit;True;Property;_Main_Tex;Main_Tex;0;0;Create;True;0;0;0;False;0;False;-1;2d3cd0f9edf4e034c9763a70334eca57;2d3cd0f9edf4e034c9763a70334eca57;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScaleAndOffsetNode;53;-608.249,650.394;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;43;-523.6753,420.1668;Inherit;False;Property;_RGBA_Mask;RGBA_Mask;7;0;Create;True;0;0;0;False;0;False;1,0,0,0;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;49;-311.2783,624.1549;Inherit;True;Property;_mask;mask;8;0;Create;True;0;0;0;False;0;False;-1;0000000000000000f000000000000000;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-225.9754,487.7673;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-222.0754,180.9672;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-227.1785,880.7638;Inherit;False;Constant;_maskScale;maskScale;9;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-223.3754,282.3674;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-222.0756,386.3674;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;30.82153,626.7638;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-572,97.5;Inherit;False;Property;_Color0;Color 0;2;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;24.92042,436.4962;Inherit;False;Property;_Alpha;Alpha;3;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-521.2613,300.6548;Inherit;False;Property;_Scale;Scale;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-6.275424,286.2672;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-248.9001,-6.999999;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;188.7211,318.1962;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;28;69.12421,-7.533082;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;250.9,-7.799998;Half;False;True;-1;2;ASEMaterialInspector;100;1;Screen_RedFX;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;2;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;False;-1;True;7;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Opaque=RenderType;Queue=Transparent=Queue=4001;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;58;0;30;1
WireConnection;58;1;30;2
WireConnection;59;0;58;0
WireConnection;62;0;59;0
WireConnection;63;0;62;1
WireConnection;63;1;62;0
WireConnection;64;0;63;0
WireConnection;60;0;59;0
WireConnection;40;0;38;3
WireConnection;36;0;34;1
WireConnection;36;1;34;2
WireConnection;37;0;34;3
WireConnection;37;1;34;4
WireConnection;65;0;60;0
WireConnection;65;1;64;0
WireConnection;33;0;65;0
WireConnection;33;1;36;0
WireConnection;33;2;37;0
WireConnection;41;0;40;0
WireConnection;41;1;38;4
WireConnection;39;0;38;1
WireConnection;39;1;38;2
WireConnection;52;0;50;1
WireConnection;52;1;50;2
WireConnection;51;0;50;3
WireConnection;51;1;50;4
WireConnection;35;0;30;1
WireConnection;35;1;30;2
WireConnection;32;0;33;0
WireConnection;32;2;39;0
WireConnection;32;1;41;0
WireConnection;1;1;32;0
WireConnection;53;0;35;0
WireConnection;53;1;52;0
WireConnection;53;2;51;0
WireConnection;49;1;53;0
WireConnection;46;0;43;4
WireConnection;46;2;1;4
WireConnection;42;0;43;1
WireConnection;42;1;1;1
WireConnection;44;0;43;2
WireConnection;44;1;1;2
WireConnection;45;0;43;3
WireConnection;45;1;1;3
WireConnection;57;0;49;1
WireConnection;57;1;55;0
WireConnection;47;0;42;0
WireConnection;47;1;44;0
WireConnection;47;2;45;0
WireConnection;47;3;46;0
WireConnection;2;0;1;0
WireConnection;2;1;4;0
WireConnection;2;2;5;0
WireConnection;48;0;47;0
WireConnection;48;1;29;0
WireConnection;48;2;57;0
WireConnection;28;0;2;0
WireConnection;28;3;48;0
WireConnection;0;0;28;0
ASEEND*/
//CHKSM=DD4DD2D73E2FF4A7EC578A8928A783574089CE2B