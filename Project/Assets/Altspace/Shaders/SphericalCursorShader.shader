Shader "Custom/SphericalCursorShader" {
	Properties {
        _Color ("Main Color", Color) = (0, 0, 0, 0)
    }
    SubShader {
    	Tags { 
    		"Queue" = "Overlay"
    		}

        Pass {
        	Color [_Color]
			
            ZTest Always
//            Lighting Off
        }
    }
}