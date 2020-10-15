
Shader "Unlit/SpecialFX/LiquidViscous"
{
    Properties
    {
        _Tint("Tint", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
        _FillAmount("Fill Amount", Range(-10,10)) = 0.0 //How much of it's full
        [HideInInspector] _WobbleX("WobbleX", Range(-1,1)) = 0.0 ///updated by wobble.cs
        [HideInInspector] _WobbleZ("WobbleZ", Range(-1,1)) = 0.0 // updated by wobble.cs.
        _TopColor("Top Color", Color) = (1,1,1,1) // Not actually top colour! Actually, reverse face colour.
        _FoamColor("Foam Line Color", Color) = (1,1,1,1)  //Front face above secondary line thats below the lip edge of the face. 
        _Rim("Foam Line Width", Range(0,0.1)) = 0.0 //Should always be thin in current style, with foam colour black.
        _RimColor("Rim Color", Color) = (1,1,1,1) //A glow prescribed around edges of shape to help the glass look.
        _RimPower("Rim Power", Range(0,10)) = 0.0 //How much glow.
    }

        SubShader
        {
            Tags {"Queue" = "Geometry"  "DisableBatching" = "True" }

                    Pass
            {
             Zwrite On
             Cull Off // we want the front and back faces. The back faces are used to do the 'top', so we dont wanna cull.
             AlphaToMask On // transparency

             CGPROGRAM


             #pragma vertex vert
             #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
              float4 vertex : POSITION;
              float2 uv : TEXCOORD0;
              float3 normal : NORMAL;
            };

            struct v2f
            {
               float2 uv : TEXCOORD0;
               UNITY_FOG_COORDS(1)
               float4 vertex : SV_POSITION;
               float3 viewDir : COLOR;
               float3 normal : COLOR2;
               float fillEdge : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _FillAmount, _WobbleX, _WobbleZ;
            float4 _TopColor, _RimColor, _FoamColor, _Tint;
            float _Rim, _RimPower;

            float4 RotateAroundYInDegrees(float4 vertex, float degrees) //Ensures we understand the "front" face.
            {
               float alpha = degrees * UNITY_PI / 180;
               float sina, cosa;
               sincos(alpha, sina, cosa);
               float2x2 m = float2x2(cosa, sina, -sina, cosa);
               return float4(vertex.yz , mul(m, vertex.xz)).xzyw;
            }


            v2f vert(appdata v)
            {
               v2f o;

               o.vertex = UnityObjectToClipPos(v.vertex);
               o.uv = TRANSFORM_TEX(v.uv, _MainTex);
               UNITY_TRANSFER_FOG(o,o.vertex);
               // get world position of the vertex. This has the 
               float3 worldPos = mul(unity_ObjectToWorld, v.vertex.xyz);
               // rotate it around XY
               float3 worldPosX = RotateAroundYInDegrees(float4(worldPos,0),360);
               // rotate around XZ
               float3 worldPosZ = float3 (worldPosX.y, worldPosX.z, worldPosX.x);
               // combine rotations with worldPos, based on sine wave from script
               float3 worldPosAdjusted = worldPos + (worldPosX * _WobbleX) + (worldPosZ * _WobbleZ);
               // how high up the liquid is
               o.fillEdge = worldPosAdjusted.y + _FillAmount;

               ///Edit for Ripple
               float rippleAddition = worldPos.x * worldPos.z * (_WobbleX + _WobbleZ) * 1.5;
               o.fillEdge = o.fillEdge + (rippleAddition);
               ///// END RIPPLE EDIT

               o.viewDir = normalize(ObjSpaceViewDir(v.vertex));
               o.normal = v.normal;
               return o;
            }

            fixed4 frag(v2f i, fixed facing : VFACE) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Tint;
            // apply fog
            UNITY_APPLY_FOG(i.fogCoord, col);

            // rim light
            float dotProduct = 1 - pow(dot(i.normal, i.viewDir), _RimPower);
            float4 RimResult = smoothstep(0.5, 1.0, dotProduct);
            RimResult *= _RimColor;

            // foam edge
            float4 foam = (step(i.fillEdge, 0.5) - step(i.fillEdge, (0.5 - _Rim)));
            float4 foamColored = foam * (_FoamColor * 0.9);
            // rest of the liquid
            float4 result = step(i.fillEdge, 0.5) - foam;
            float4 resultColored = result * col;
            // both together, with the texture
            float4 finalResult = resultColored + foamColored;
            finalResult.rgb += RimResult;

            // color of backfaces/ top
            float4 topColor = _TopColor * (foam + result);
            //VFACE returns positive for front facing, negative for backfacing
            return facing > 0 ? finalResult : topColor;

          }
          ENDCG
         }

        }
}