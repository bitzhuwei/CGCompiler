namespace CSharpGL.GLSL430Compiler
{
    /// <summary>
    /// 文法GLSL430的单词枚举类型
    /// </summary>
    public enum EnumTokenTypeGLSL430
    {
        /// <summary>
        /// 未知的单词符号
        /// </summary>
        unknown,
        /// <summary>
        /// null
        /// </summary>
        epsilon,
        /// <summary>
        /// &quot;precision&quot;
        /// </summary>
        token_precision,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        token_Semicolon_,
        /// <summary>
        /// &quot;void&quot;
        /// </summary>
        token_void,
        /// <summary>
        /// identifier
        /// </summary>
        identifier,
        /// <summary>
        /// &quot;[&quot;
        /// </summary>
        token_LeftBracket_,
        /// <summary>
        /// &quot;]&quot;
        /// </summary>
        token_RightBracket_,
        /// <summary>
        /// &quot;{&quot;
        /// </summary>
        token_LeftBrace_,
        /// <summary>
        /// &quot;}&quot;
        /// </summary>
        token_RightBrace_,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        token_LeftParentheses_,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        token_RightParentheses_,
        /// <summary>
        /// &quot;if&quot;
        /// </summary>
        token_if,
        /// <summary>
        /// &quot;else&quot;
        /// </summary>
        token_else,
        /// <summary>
        /// &quot;switch&quot;
        /// </summary>
        token_switch,
        /// <summary>
        /// &quot;case&quot;
        /// </summary>
        token_case,
        /// <summary>
        /// &quot;:&quot;
        /// </summary>
        token_Colon_,
        /// <summary>
        /// &quot;default&quot;
        /// </summary>
        token_default,
        /// <summary>
        /// &quot;while&quot;
        /// </summary>
        token_while,
        /// <summary>
        /// &quot;do&quot;
        /// </summary>
        token_do,
        /// <summary>
        /// &quot;for&quot;
        /// </summary>
        token_for,
        /// <summary>
        /// &quot;,&quot;
        /// </summary>
        token_Comma_,
        /// <summary>
        /// &quot;=&quot;
        /// </summary>
        token_Equality_,
        /// <summary>
        /// &quot;continue&quot;
        /// </summary>
        token_continue,
        /// <summary>
        /// &quot;break&quot;
        /// </summary>
        token_break,
        /// <summary>
        /// &quot;return&quot;
        /// </summary>
        token_return,
        /// <summary>
        /// &quot;discard&quot;
        /// </summary>
        token_discard,
        /// <summary>
        /// &quot;++&quot;
        /// </summary>
        token_Plus_Plus_,
        /// <summary>
        /// &quot;--&quot;
        /// </summary>
        token_Minus_Minus_,
        /// <summary>
        /// &quot;.&quot;
        /// </summary>
        token_Dot_,
        /// <summary>
        /// number
        /// </summary>
        number,
        /// <summary>
        /// &quot;true&quot;
        /// </summary>
        token_true,
        /// <summary>
        /// &quot;false&quot;
        /// </summary>
        token_false,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        token_Plus_,
        /// <summary>
        /// &quot;-&quot;
        /// </summary>
        token_Minus_,
        /// <summary>
        /// &quot;!&quot;
        /// </summary>
        token_Not_,
        /// <summary>
        /// &quot;~&quot;
        /// </summary>
        token_Reverse_,
        /// <summary>
        /// &quot;*=&quot;
        /// </summary>
        token_Multiply_Equality_,
        /// <summary>
        /// &quot;/=&quot;
        /// </summary>
        token_Divide_Equality_,
        /// <summary>
        /// &quot;%=&quot;
        /// </summary>
        token_Percent_Equality_,
        /// <summary>
        /// &quot;+=&quot;
        /// </summary>
        token_Plus_Equality_,
        /// <summary>
        /// &quot;-=&quot;
        /// </summary>
        token_Minus_Equality_,
        /// <summary>
        /// &quot;&lt;&lt;=&quot;
        /// </summary>
        token_LessThan_LessThan_Equality_,
        /// <summary>
        /// &quot;&gt;&gt;=&quot;
        /// </summary>
        token_GreaterThan_GreaterThan_Equality_,
        /// <summary>
        /// &quot;&amp;=&quot;
        /// </summary>
        token_And_Equality_,
        /// <summary>
        /// &quot;^=&quot;
        /// </summary>
        token_Xor_Equality_,
        /// <summary>
        /// &quot;|=&quot;
        /// </summary>
        token_Or_Equality_,
        /// <summary>
        /// &quot;?&quot;
        /// </summary>
        token_Question_,
        /// <summary>
        /// &quot;||&quot;
        /// </summary>
        token_Or_Or_,
        /// <summary>
        /// &quot;^^&quot;
        /// </summary>
        token_Xor_Xor_,
        /// <summary>
        /// &quot;&amp;&amp;&quot;
        /// </summary>
        token_And_And_,
        /// <summary>
        /// &quot;|&quot;
        /// </summary>
        token_Or_,
        /// <summary>
        /// &quot;^&quot;
        /// </summary>
        token_Xor_,
        /// <summary>
        /// &quot;&amp;&quot;
        /// </summary>
        token_And_,
        /// <summary>
        /// &quot;==&quot;
        /// </summary>
        token_Equality_Equality_,
        /// <summary>
        /// &quot;!=&quot;
        /// </summary>
        token_Not_Equality_,
        /// <summary>
        /// &quot;&lt;&quot;
        /// </summary>
        token_LessThan_,
        /// <summary>
        /// &quot;&gt;&quot;
        /// </summary>
        token_GreaterThan_,
        /// <summary>
        /// &quot;&lt;=&quot;
        /// </summary>
        token_LessThan_Equality_,
        /// <summary>
        /// &quot;&gt;=&quot;
        /// </summary>
        token_GreaterThan_Equality_,
        /// <summary>
        /// &quot;&lt;&lt;&quot;
        /// </summary>
        token_LessThan_LessThan_,
        /// <summary>
        /// &quot;&gt;&gt;&quot;
        /// </summary>
        token_GreaterThan_GreaterThan_,
        /// <summary>
        /// &quot;*&quot;
        /// </summary>
        token_Multiply_,
        /// <summary>
        /// &quot;/&quot;
        /// </summary>
        token_Divide_,
        /// <summary>
        /// &quot;%&quot;
        /// </summary>
        token_Percent_,
        /// <summary>
        /// &quot;float&quot;
        /// </summary>
        token_float,
        /// <summary>
        /// &quot;double&quot;
        /// </summary>
        token_double,
        /// <summary>
        /// &quot;int&quot;
        /// </summary>
        token_int,
        /// <summary>
        /// &quot;uint&quot;
        /// </summary>
        token_uint,
        /// <summary>
        /// &quot;bool&quot;
        /// </summary>
        token_bool,
        /// <summary>
        /// &quot;vec2&quot;
        /// </summary>
        token_vec2,
        /// <summary>
        /// &quot;vec3&quot;
        /// </summary>
        token_vec3,
        /// <summary>
        /// &quot;vec4&quot;
        /// </summary>
        token_vec4,
        /// <summary>
        /// &quot;dvec2&quot;
        /// </summary>
        token_dvec2,
        /// <summary>
        /// &quot;dvec3&quot;
        /// </summary>
        token_dvec3,
        /// <summary>
        /// &quot;dvec4&quot;
        /// </summary>
        token_dvec4,
        /// <summary>
        /// &quot;bvec2&quot;
        /// </summary>
        token_bvec2,
        /// <summary>
        /// &quot;bvec3&quot;
        /// </summary>
        token_bvec3,
        /// <summary>
        /// &quot;bvec4&quot;
        /// </summary>
        token_bvec4,
        /// <summary>
        /// &quot;ivec2&quot;
        /// </summary>
        token_ivec2,
        /// <summary>
        /// &quot;ivec3&quot;
        /// </summary>
        token_ivec3,
        /// <summary>
        /// &quot;ivec4&quot;
        /// </summary>
        token_ivec4,
        /// <summary>
        /// &quot;uvec2&quot;
        /// </summary>
        token_uvec2,
        /// <summary>
        /// &quot;uvec3&quot;
        /// </summary>
        token_uvec3,
        /// <summary>
        /// &quot;uvec4&quot;
        /// </summary>
        token_uvec4,
        /// <summary>
        /// &quot;mat2&quot;
        /// </summary>
        token_mat2,
        /// <summary>
        /// &quot;mat3&quot;
        /// </summary>
        token_mat3,
        /// <summary>
        /// &quot;mat4&quot;
        /// </summary>
        token_mat4,
        /// <summary>
        /// &quot;mat2x2&quot;
        /// </summary>
        token_mat2x2,
        /// <summary>
        /// &quot;mat2x3&quot;
        /// </summary>
        token_mat2x3,
        /// <summary>
        /// &quot;mat2x4&quot;
        /// </summary>
        token_mat2x4,
        /// <summary>
        /// &quot;mat3x2&quot;
        /// </summary>
        token_mat3x2,
        /// <summary>
        /// &quot;mat3x3&quot;
        /// </summary>
        token_mat3x3,
        /// <summary>
        /// &quot;mat3x4&quot;
        /// </summary>
        token_mat3x4,
        /// <summary>
        /// &quot;mat4x2&quot;
        /// </summary>
        token_mat4x2,
        /// <summary>
        /// &quot;mat4x3&quot;
        /// </summary>
        token_mat4x3,
        /// <summary>
        /// &quot;mat4x4&quot;
        /// </summary>
        token_mat4x4,
        /// <summary>
        /// &quot;dmat2&quot;
        /// </summary>
        token_dmat2,
        /// <summary>
        /// &quot;dmat3&quot;
        /// </summary>
        token_dmat3,
        /// <summary>
        /// &quot;dmat4&quot;
        /// </summary>
        token_dmat4,
        /// <summary>
        /// &quot;dmat2x2&quot;
        /// </summary>
        token_dmat2x2,
        /// <summary>
        /// &quot;dmat2x3&quot;
        /// </summary>
        token_dmat2x3,
        /// <summary>
        /// &quot;dmat2x4&quot;
        /// </summary>
        token_dmat2x4,
        /// <summary>
        /// &quot;dmat3x2&quot;
        /// </summary>
        token_dmat3x2,
        /// <summary>
        /// &quot;dmat3x3&quot;
        /// </summary>
        token_dmat3x3,
        /// <summary>
        /// &quot;dmat3x4&quot;
        /// </summary>
        token_dmat3x4,
        /// <summary>
        /// &quot;dmat4x2&quot;
        /// </summary>
        token_dmat4x2,
        /// <summary>
        /// &quot;dmat4x3&quot;
        /// </summary>
        token_dmat4x3,
        /// <summary>
        /// &quot;dmat4x4&quot;
        /// </summary>
        token_dmat4x4,
        /// <summary>
        /// &quot;atomic_uint&quot;
        /// </summary>
        token_atomic_uint,
        /// <summary>
        /// &quot;sampler1D&quot;
        /// </summary>
        token_sampler1D,
        /// <summary>
        /// &quot;sampler2D&quot;
        /// </summary>
        token_sampler2D,
        /// <summary>
        /// &quot;sampler3D&quot;
        /// </summary>
        token_sampler3D,
        /// <summary>
        /// &quot;samplerCube&quot;
        /// </summary>
        token_samplerCube,
        /// <summary>
        /// &quot;sampler1DShadow&quot;
        /// </summary>
        token_sampler1DShadow,
        /// <summary>
        /// &quot;sampler2DShadow&quot;
        /// </summary>
        token_sampler2DShadow,
        /// <summary>
        /// &quot;samplerCubeShadow&quot;
        /// </summary>
        token_samplerCubeShadow,
        /// <summary>
        /// &quot;sampler1DArray&quot;
        /// </summary>
        token_sampler1DArray,
        /// <summary>
        /// &quot;sampler2DArray&quot;
        /// </summary>
        token_sampler2DArray,
        /// <summary>
        /// &quot;sampler1DArrayShadow&quot;
        /// </summary>
        token_sampler1DArrayShadow,
        /// <summary>
        /// &quot;sampler2DArrayShadow&quot;
        /// </summary>
        token_sampler2DArrayShadow,
        /// <summary>
        /// &quot;samplerCubeArray&quot;
        /// </summary>
        token_samplerCubeArray,
        /// <summary>
        /// &quot;samplerCubeArrayShadow&quot;
        /// </summary>
        token_samplerCubeArrayShadow,
        /// <summary>
        /// &quot;isampler1D&quot;
        /// </summary>
        token_isampler1D,
        /// <summary>
        /// &quot;isampler2D&quot;
        /// </summary>
        token_isampler2D,
        /// <summary>
        /// &quot;isampler3D&quot;
        /// </summary>
        token_isampler3D,
        /// <summary>
        /// &quot;isamplerCube&quot;
        /// </summary>
        token_isamplerCube,
        /// <summary>
        /// &quot;isampler1DArray&quot;
        /// </summary>
        token_isampler1DArray,
        /// <summary>
        /// &quot;isampler2DArray&quot;
        /// </summary>
        token_isampler2DArray,
        /// <summary>
        /// &quot;isamplerCubeArray&quot;
        /// </summary>
        token_isamplerCubeArray,
        /// <summary>
        /// &quot;usampler1D&quot;
        /// </summary>
        token_usampler1D,
        /// <summary>
        /// &quot;usampler2D&quot;
        /// </summary>
        token_usampler2D,
        /// <summary>
        /// &quot;usampler3D&quot;
        /// </summary>
        token_usampler3D,
        /// <summary>
        /// &quot;usamplerCube&quot;
        /// </summary>
        token_usamplerCube,
        /// <summary>
        /// &quot;usampler1DArray&quot;
        /// </summary>
        token_usampler1DArray,
        /// <summary>
        /// &quot;usampler2DArray&quot;
        /// </summary>
        token_usampler2DArray,
        /// <summary>
        /// &quot;usamplerCubeArray&quot;
        /// </summary>
        token_usamplerCubeArray,
        /// <summary>
        /// &quot;sampler2DRect&quot;
        /// </summary>
        token_sampler2DRect,
        /// <summary>
        /// &quot;sampler2DRectShadow&quot;
        /// </summary>
        token_sampler2DRectShadow,
        /// <summary>
        /// &quot;isampler2DRect&quot;
        /// </summary>
        token_isampler2DRect,
        /// <summary>
        /// &quot;usampler2DRect&quot;
        /// </summary>
        token_usampler2DRect,
        /// <summary>
        /// &quot;samplerBuffer&quot;
        /// </summary>
        token_samplerBuffer,
        /// <summary>
        /// &quot;isamplerBuffer&quot;
        /// </summary>
        token_isamplerBuffer,
        /// <summary>
        /// &quot;usamplerBuffer&quot;
        /// </summary>
        token_usamplerBuffer,
        /// <summary>
        /// &quot;sampler2DMS&quot;
        /// </summary>
        token_sampler2DMS,
        /// <summary>
        /// &quot;isampler2DMS&quot;
        /// </summary>
        token_isampler2DMS,
        /// <summary>
        /// &quot;usampler2DMS&quot;
        /// </summary>
        token_usampler2DMS,
        /// <summary>
        /// &quot;sampler2DMSArray&quot;
        /// </summary>
        token_sampler2DMSArray,
        /// <summary>
        /// &quot;isampler2DMSArray&quot;
        /// </summary>
        token_isampler2DMSArray,
        /// <summary>
        /// &quot;usampler2DMSArray&quot;
        /// </summary>
        token_usampler2DMSArray,
        /// <summary>
        /// &quot;image1D&quot;
        /// </summary>
        token_image1D,
        /// <summary>
        /// &quot;iimage1D&quot;
        /// </summary>
        token_iimage1D,
        /// <summary>
        /// &quot;uimage1D&quot;
        /// </summary>
        token_uimage1D,
        /// <summary>
        /// &quot;image2D&quot;
        /// </summary>
        token_image2D,
        /// <summary>
        /// &quot;iimage2D&quot;
        /// </summary>
        token_iimage2D,
        /// <summary>
        /// &quot;uimage2D&quot;
        /// </summary>
        token_uimage2D,
        /// <summary>
        /// &quot;image3D&quot;
        /// </summary>
        token_image3D,
        /// <summary>
        /// &quot;iimage3D&quot;
        /// </summary>
        token_iimage3D,
        /// <summary>
        /// &quot;uimage3D&quot;
        /// </summary>
        token_uimage3D,
        /// <summary>
        /// &quot;image2DRect&quot;
        /// </summary>
        token_image2DRect,
        /// <summary>
        /// &quot;iimage2DRect&quot;
        /// </summary>
        token_iimage2DRect,
        /// <summary>
        /// &quot;uimage2DRect&quot;
        /// </summary>
        token_uimage2DRect,
        /// <summary>
        /// &quot;imageCube&quot;
        /// </summary>
        token_imageCube,
        /// <summary>
        /// &quot;iimageCube&quot;
        /// </summary>
        token_iimageCube,
        /// <summary>
        /// &quot;uimageCube&quot;
        /// </summary>
        token_uimageCube,
        /// <summary>
        /// &quot;imageBuffer&quot;
        /// </summary>
        token_imageBuffer,
        /// <summary>
        /// &quot;iimageBuffer&quot;
        /// </summary>
        token_iimageBuffer,
        /// <summary>
        /// &quot;uimageBuffer&quot;
        /// </summary>
        token_uimageBuffer,
        /// <summary>
        /// &quot;image1DArray&quot;
        /// </summary>
        token_image1DArray,
        /// <summary>
        /// &quot;iimage1DArray&quot;
        /// </summary>
        token_iimage1DArray,
        /// <summary>
        /// &quot;uimage1DArray&quot;
        /// </summary>
        token_uimage1DArray,
        /// <summary>
        /// &quot;image2DArray&quot;
        /// </summary>
        token_image2DArray,
        /// <summary>
        /// &quot;iimage2DArray&quot;
        /// </summary>
        token_iimage2DArray,
        /// <summary>
        /// &quot;uimage2DArray&quot;
        /// </summary>
        token_uimage2DArray,
        /// <summary>
        /// &quot;imageCubeArray&quot;
        /// </summary>
        token_imageCubeArray,
        /// <summary>
        /// &quot;iimageCubeArray&quot;
        /// </summary>
        token_iimageCubeArray,
        /// <summary>
        /// &quot;uimageCubeArray&quot;
        /// </summary>
        token_uimageCubeArray,
        /// <summary>
        /// &quot;image2DMS&quot;
        /// </summary>
        token_image2DMS,
        /// <summary>
        /// &quot;iimage2DMS&quot;
        /// </summary>
        token_iimage2DMS,
        /// <summary>
        /// &quot;uimage2DMS&quot;
        /// </summary>
        token_uimage2DMS,
        /// <summary>
        /// &quot;image2DMSArray&quot;
        /// </summary>
        token_image2DMSArray,
        /// <summary>
        /// &quot;iimage2DMSArray&quot;
        /// </summary>
        token_iimage2DMSArray,
        /// <summary>
        /// &quot;uimage2DMSArray&quot;
        /// </summary>
        token_uimage2DMSArray,
        /// <summary>
        /// &quot;struct&quot;
        /// </summary>
        token_struct,
        /// <summary>
        /// &quot;const&quot;
        /// </summary>
        token_const,
        /// <summary>
        /// &quot;inout&quot;
        /// </summary>
        token_inout,
        /// <summary>
        /// &quot;in&quot;
        /// </summary>
        token_in,
        /// <summary>
        /// &quot;out&quot;
        /// </summary>
        token_out,
        /// <summary>
        /// &quot;centroid&quot;
        /// </summary>
        token_centroid,
        /// <summary>
        /// &quot;patch&quot;
        /// </summary>
        token_patch,
        /// <summary>
        /// &quot;sample&quot;
        /// </summary>
        token_sample,
        /// <summary>
        /// &quot;uniform&quot;
        /// </summary>
        token_uniform,
        /// <summary>
        /// &quot;buffer&quot;
        /// </summary>
        token_buffer,
        /// <summary>
        /// &quot;shared&quot;
        /// </summary>
        token_shared,
        /// <summary>
        /// &quot;coherent&quot;
        /// </summary>
        token_coherent,
        /// <summary>
        /// &quot;volatile&quot;
        /// </summary>
        token_volatile,
        /// <summary>
        /// &quot;restrict&quot;
        /// </summary>
        token_restrict,
        /// <summary>
        /// &quot;readonly&quot;
        /// </summary>
        token_readonly,
        /// <summary>
        /// &quot;writeonly&quot;
        /// </summary>
        token_writeonly,
        /// <summary>
        /// &quot;subroutine&quot;
        /// </summary>
        token_subroutine,
        /// <summary>
        /// &quot;layout&quot;
        /// </summary>
        token_layout,
        /// <summary>
        /// &quot;high_precision&quot;
        /// </summary>
        token_high_precision,
        /// <summary>
        /// &quot;medium_precision&quot;
        /// </summary>
        token_medium_precision,
        /// <summary>
        /// &quot;low_precision&quot;
        /// </summary>
        token_low_precision,
        /// <summary>
        /// &quot;smooth&quot;
        /// </summary>
        token_smooth,
        /// <summary>
        /// &quot;flat&quot;
        /// </summary>
        token_flat,
        /// <summary>
        /// &quot;noperspective&quot;
        /// </summary>
        token_noperspective,
        /// <summary>
        /// &quot;invariant&quot;
        /// </summary>
        token_invariant,
        /// <summary>
        /// &quot;precise&quot;
        /// </summary>
        token_precise,
        /// <summary>
        /// #
        /// </summary>
        token_startEnd,
    }

}

