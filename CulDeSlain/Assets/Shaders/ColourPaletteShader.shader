Shader "Unlit/ColourPaletteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
         
        Pass
        {
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            static const float3 c_205[256] = 
            {
                float3(6,0,19),
                float3(25,20,29),
                float3(233,227,150),
                float3(206,187,108),
                float3(181,143,70),
                float3(158,102,40),
                float3(132,59,17),
                float3(120,37,15),
                float3(210,121,126),
                float3(187,106,110),
                float3(164,93,98),
                float3(141,81,82),
                float3(118,65,69),
                float3(94,53,53),
                float3(72,40,41),
                float3(54,29,29),
                float3(75,9,212),
                float3(67,8,189),
                float3(58,7,166),
                float3(50,6,144),
                float3(41,6,121),
                float3(33,5,98),
                float3(24,4,76),
                float3(29,16,68),
                float3(130,149,168),
                float3(114,133,148),
                float3(101,117,132),
                float3(86,102,112),
                float3(73,86,96),
                float3(57,70,76),
                float3(45,55,60),
                float3(34,40,46),
                float3(162,111,112),
                float3(149,98,99),
                float3(133,86,87),
                float3(120,78,79),
                float3(104,69,69),
                float3(91,57,57),
                float3(75,48,48),
                float3(71,47,53),
                float3(162,127,184),
                float3(146,115,164),
                float3(130,103,148),
                float3(114,91,131),
                float3(98,78,111),
                float3(82,66,95),
                float3(66,54,78),
                float3(55,43,63),
                float3(202,151,163),
                float3(179,134,144),
                float3(158,118,128),
                float3(138,102,112),
                float3(114,86,92),
                float3(94,71,76),
                float3(73,55,57),
                float3(62,49,55),
                float3(141,136,188),
                float3(123,121,167),
                float3(109,104,148),
                float3(94,90,127),
                float3(79,74,108),
                float3(65,57,86),
                float3(50,44,68),
                float3(37,32,51),
                float3(232,152,140),
                float3(209,132,121),
                float3(186,115,105),
                float3(164,98,92),
                float3(142,82,76),
                float3(119,69,63),
                float3(97,53,50),
                float3(76,56,37),
                float3(21,199,20),
                float3(18,187,17),
                float3(15,157,13),
                float3(13,129,10),
                float3(10,104,7),
                float3(7,76,5),
                float3(5,48,3),
                float3(2,27,1),
                float3(56,179,250),
                float3(41,150,229),
                float3(31,121,208),
                float3(21,96,187),
                float3(14,75,166),
                float3(8,55,145),
                float3(3,35,124),
                float3(22,42,115),
                float3(249,241,220),
                float3(214,196,180),
                float3(182,152,140),
                float3(151,115,107),
                float3(119,83,78),
                float3(88,55,53),
                float3(56,28,28),
                float3(28,11,11),
                float3(237,216,228),
                float3(195,176,195),
                float3(157,140,166),
                float3(122,107,137),
                float3(91,78,107),
                float3(63,53,78),
                float3(36,28,49),
                float3(37,31,20),
                float3(250,244,0),
                float3(222,187,0),
                float3(194,137,0),
                float3(170,95,0),
                float3(142,61,0),
                float3(118,34,0),
                float3(90,11,0),
                float3(68,3,0),
                float3(228,0,2),
                float3(203,0,21),
                float3(181,0,21),
                float3(156,0,20),
                float3(133,0,20),
                float3(108,0,20),
                float3(86,0,19),
                float3(80,16,19),
                float3(228,192,19),
                float3(203,163,18),
                float3(178,137,18),
                float3(155,112,18),
                float3(130,90,17),
                float3(108,71,17),
                float3(82,50,17),
                float3(62,37,16),
                float3(0,0,35),
                float3(0,0,246),
                float3(0,0,242),
                float3(0,0,234),
                float3(0,0,230),
                float3(0,0,226),
                float3(0,0,222),
                float3(0,0,218),
                float3(0,0,214),
                float3(0,0,210),
                float3(0,0,206),
                float3(0,0,202),
                float3(0,0,198),
                float3(0,0,194),
                float3(0,0,190),
                float3(0,0,186),
                float3(0,0,182),
                float3(0,0,178),
                float3(0,0,173),
                float3(0,0,166),
                float3(0,0,162),
                float3(0,0,158),
                float3(0,0,154),
                float3(0,0,150),
                float3(0,0,146),
                float3(0,0,142),
                float3(0,0,138),
                float3(0,0,134),
                float3(0,0,130),
                float3(0,0,126),
                float3(0,0,122),
                float3(0,0,118),
                float3(21,21,131),
                float3(20,20,126),
                float3(19,19,119),
                float3(18,18,115),
                float3(17,17,111),
                float3(16,16,107),
                float3(15,15,103),
                float3(14,14,99),
                float3(13,13,95),
                float3(12,12,91),
                float3(11,11,87),
                float3(10,10,83),
                float3(10,10,79),
                float3(9,9,75),
                float3(8,8,71),
                float3(7,7,67),
                float3(229,229,251),
                float3(215,215,242),
                float3(204,204,236),
                float3(193,193,231),
                float3(182,182,225),
                float3(171,171,220),
                float3(160,160,214),
                float3(151,149,208),
                float3(163,142,197),
                float3(150,131,190),
                float3(137,120,186),
                float3(128,113,183),
                float3(118,105,179),
                float3(106,95,175),
                float3(96,87,172),
                float3(88,81,168),
                float3(0,21,229),
                float3(0,19,207),
                float3(0,17,185),
                float3(0,14,163),
                float3(0,12,145),
                float3(0,10,123),
                float3(0,8,101),
                float3(21,7,79),
                float3(250,21,21),
                float3(226,19,19),
                float3(202,17,17),
                float3(178,15,15),
                float3(154,13,13),
                float3(130,11,11),
                float3(106,9,9),
                float3(87,7,7),
                float3(21,246,0),
                float3(21,220,0),
                float3(21,196,0),
                float3(21,169,0),
                float3(21,146,0),
                float3(21,119,0),
                float3(21,96,0),
                float3(41,88,21),
                float3(250,228,250),
                float3(222,203,222),
                float3(198,181,198),
                float3(174,159,174),
                float3(150,137,150),
                float3(126,115,126),
                float3(102,93,102),
                float3(80,73,80),
                float3(231,193,0),
                float3(232,179,1),
                float3(233,166,3),
                float3(235,153,4),
                float3(236,143,6),
                float3(238,130,7),
                float3(239,117,8),
                float3(241,100,30),
                float3(240,11,240),
                float3(216,13,216),
                float3(195,14,195),
                float3(175,16,175),
                float3(154,17,154),
                float3(134,18,134),
                float3(113,20,113),
                float3(95,21,95),
                float3(1,1,1),
                float3(13,13,13),
                float3(30,30,30),
                float3(49,49,49),
                float3(65,65,65),
                float3(81,81,81),
                float3(98,98,98),
                float3(117,117,117),
                float3(133,133,133),
                float3(149,149,149),
                float3(166,166,166),
                float3(185,185,185),
                float3(201,201,201),
                float3(217,217,217),
                float3(234,234,234),
                float3(252,252,252)
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // convert col to 0-255 range
                col.rgb *= 255.0;
                // find the closest color in _ColorPalette
                float minDist = 1000000.0;
                fixed4 closestColor = fixed4(c_205[0].rgb / 255.0, 1.0);
                for (int j = 0; j < c_205.Length; j++) {
                    // convert c_205[j] to 0-255 range
                    fixed3 c = c_205[j].rgb * 255.0;
                    float dist = distance(col.rgb, c);
                    if (dist < minDist) {
                        minDist = dist;
                        closestColor = fixed4(c / 255.0, 1.0);
                    }
                }
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, closestColor);
                return (0,0,1,1);
            }
            ENDCG
        }
    }
}
