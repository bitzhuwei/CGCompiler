using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bitzhuwei.CGCompiler.Winform
{
    static class StringExtension
    {
        public static void Save(this string source, string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, false))
            {
                sw.Write(source);
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string sourceCode = 预处理originalGrammar();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormMain4BuildingGrammar(sourceCode));
            Application.Run(new FormMain());
        }

        private static string 预处理originalGrammar()
        {
            string filename = "GLSL4.3Grammar";
            string extensionName = "txt";
            v43Step0RemoveEmptyLines(filename, extensionName);
            v43Step1AddBracket(filename, extensionName);
            v43Step2FindLeaves(filename, extensionName);

            return File.ReadAllText(filename + "2." + extensionName);
        }

        private static void v43Step2FindLeaves(string filename, string extensionName)
        {
            StringBuilder builder = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(filename + "1." + extensionName);
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];
                line = Regex.Replace(line, "<TRUE>", "\"true\"");
                line = Regex.Replace(line, "<FALSE>", "\"false\"");
                line = Regex.Replace(line, "<PRECISION>", "\"precision\"");
                line = Regex.Replace(line, "<null>", "null");
                line = Regex.Replace(line, "<number>", "number");
                line = Regex.Replace(line, "<constString>", "constString");
                line = Regex.Replace(line, "<IDENTIFIER>", "identifier");
                line = Regex.Replace(line, "<identifier>", "identifier");
                line = Regex.Replace(line, "<LEFT_PAREN>", "\"(\"");
                line = Regex.Replace(line, "<RIGHT_PAREN>", "\")\"");
                line = Regex.Replace(line, "<LEFT_BRACKET>", "\"[\"");
                line = Regex.Replace(line, "<RIGHT_BRACKET>", "\"]\"");
                line = Regex.Replace(line, "<DOT>", "\".\"");
                line = Regex.Replace(line, "<INC_OP>", "\"++\"");
                line = Regex.Replace(line, "<DEC_OP>", "\"--\"");
                line = Regex.Replace(line, "<VOID>", "\"void\"");
                line = Regex.Replace(line, "<COMMA>", "\",\"");
                line = Regex.Replace(line, "<PLUS>", "\"+\"");
                line = Regex.Replace(line, "<DASH>", "\"-\"");
                line = Regex.Replace(line, "<BANG>", "\"!\"");
                line = Regex.Replace(line, "<TILDE>", "\"~\"");
                line = Regex.Replace(line, "<STAR>", "\"*\"");
                line = Regex.Replace(line, "<SLASH>", "\"/\"");
                line = Regex.Replace(line, "<PERCENT>", "\"%\"");
                line = Regex.Replace(line, "<LEFT_OP>", "\"<<\"");
                line = Regex.Replace(line, "<RIGHT_OP>", "\">>\"");
                line = Regex.Replace(line, "<LEFT_ANGLE>", "\"<\"");
                line = Regex.Replace(line, "<RIGHT_ANGLE>", "\">\"");
                line = Regex.Replace(line, "<LE_OP>", "\"<=\"");
                line = Regex.Replace(line, "<GE_OP>", "\">=\"");
                line = Regex.Replace(line, "<EQ_OP>", "\"==\"");
                line = Regex.Replace(line, "<NE_OP>", "\"!=\"");
                line = Regex.Replace(line, "<AMPERSAND>", "\"&\"");
                line = Regex.Replace(line, "<CARET>", "\"^\"");
                line = Regex.Replace(line, "<VERTICAL_BAR>", "\"|\"");
                line = Regex.Replace(line, "<AND_OP>", "\"&&\"");
                line = Regex.Replace(line, "<XOR_OP>", "\"^^\"");
                line = Regex.Replace(line, "<OR_OP>", "\"||\"");
                line = Regex.Replace(line, "<QUESTION>", "\"?\"");
                line = Regex.Replace(line, "<COLON>", "\":\"");
                line = Regex.Replace(line, "<EQUAL>", "\"=\"");
                line = Regex.Replace(line, "<MUL_ASSIGN>", "\"*=\"");
                line = Regex.Replace(line, "<DIV_ASSIGN>", "\"/=\"");
                line = Regex.Replace(line, "<MOD_ASSIGN>", "\"%=\"");
                line = Regex.Replace(line, "<ADD_ASSIGN>", "\"+=\"");
                line = Regex.Replace(line, "<SUB_ASSIGN>", "\"-=\"");
                line = Regex.Replace(line, "<LEFT_ASSIGN>", "\"<<=\"");
                line = Regex.Replace(line, "<RIGHT_ASSIGN>", "\">>=\"");
                line = Regex.Replace(line, "<AND_ASSIGN>", "\"&=\"");
                line = Regex.Replace(line, "<XOR_ASSIGN>", "\"^=\"");
                line = Regex.Replace(line, "<OR_ASSIGN>", "\"|=\"");
                line = Regex.Replace(line, "<COMMA>", "\",\"");
                line = Regex.Replace(line, "<INVARIANT>", "\"invariant\"");
                line = Regex.Replace(line, "<SMOOTH>", "\"smooth\"");
                line = Regex.Replace(line, "<FLAT>", "\"flat\"");
                line = Regex.Replace(line, "<NOPERSPECTIVE>", "\"noperspective\"");
                line = Regex.Replace(line, "<LAYOUT>", "\"layout\"");
                line = Regex.Replace(line, "<PRECISE>", "\"precise\"");
                line = Regex.Replace(line, "<CONST>", "\"const\"");
                line = Regex.Replace(line, "<INOUT>", "\"inout\"");
                line = Regex.Replace(line, "<IN>", "\"in\"");
                line = Regex.Replace(line, "<OUT>", "\"out\"");
                line = Regex.Replace(line, "<CENTROID>", "\"centroid\"");
                line = Regex.Replace(line, "<PATCH>", "\"patch\"");
                line = Regex.Replace(line, "<SAMPLE>", "\"sample\"");
                line = Regex.Replace(line, "<UNIFORM>", "\"uniform\"");
                line = Regex.Replace(line, "<BUFFER>", "\"buffer\"");
                line = Regex.Replace(line, "<SHARED>", "\"shared\"");
                line = Regex.Replace(line, "<COHERENT>", "\"coherent\"");
                line = Regex.Replace(line, "<VOLATILE>", "\"volatile\"");
                line = Regex.Replace(line, "<RESTRICT>", "\"restrict\"");
                line = Regex.Replace(line, "<READONLY>", "\"readonly\"");
                line = Regex.Replace(line, "<WRITEONLY>", "\"writeonly\"");
                line = Regex.Replace(line, "<SUBROUTINE>", "\"subroutine\"");
                line = Regex.Replace(line, "<VOID>", "\"void\"");
                line = Regex.Replace(line, "<FLOAT>", "\"float\"");
                line = Regex.Replace(line, "<DOUBLE>", "\"double\"");
                line = Regex.Replace(line, "<INT>", "\"int\"");
                line = Regex.Replace(line, "<UINT>", "\"uint\"");
                line = Regex.Replace(line, "<BOOL>", "\"bool\"");
                line = Regex.Replace(line, "<VEC2>", "\"vec2\"");
                line = Regex.Replace(line, "<VEC3>", "\"vec3\"");
                line = Regex.Replace(line, "<VEC4>", "\"vec4\"");
                line = Regex.Replace(line, "<DVEC2>", "\"dvec2\"");
                line = Regex.Replace(line, "<DVEC3>", "\"dvec3\"");
                line = Regex.Replace(line, "<DVEC4>", "\"dvec4\"");
                line = Regex.Replace(line, "<BVEC2>", "\"bvec2\"");
                line = Regex.Replace(line, "<BVEC3>", "\"bvec3\"");
                line = Regex.Replace(line, "<BVEC4>", "\"bvec4\"");
                line = Regex.Replace(line, "<IVEC2>", "\"ivec2\"");
                line = Regex.Replace(line, "<IVEC3>", "\"ivec3\"");
                line = Regex.Replace(line, "<IVEC4>", "\"ivec4\"");
                line = Regex.Replace(line, "<UVEC2>", "\"uvec2\"");
                line = Regex.Replace(line, "<UVEC3>", "\"uvec3\"");
                line = Regex.Replace(line, "<UVEC4>", "\"uvec4\"");
                line = Regex.Replace(line, "<MAT2>", "\"mat2\"");
                line = Regex.Replace(line, "<MAT3>", "\"mat3\"");
                line = Regex.Replace(line, "<MAT4>", "\"mat4\"");
                line = Regex.Replace(line, "<MAT2X2>", "\"mat2x2\"");
                line = Regex.Replace(line, "<MAT2X3>", "\"mat2x3\"");
                line = Regex.Replace(line, "<MAT2X4>", "\"mat2x4\"");
                line = Regex.Replace(line, "<MAT3X2>", "\"mat3x2\"");
                line = Regex.Replace(line, "<MAT3X3>", "\"mat3x3\"");
                line = Regex.Replace(line, "<MAT3X4>", "\"mat3x4\"");
                line = Regex.Replace(line, "<MAT4X2>", "\"mat4x2\"");
                line = Regex.Replace(line, "<MAT4X3>", "\"mat4x3\"");
                line = Regex.Replace(line, "<MAT4X4>", "\"mat4x4\"");
                line = Regex.Replace(line, "<DMAT2>", "\"dmat2\"");
                line = Regex.Replace(line, "<DMAT3>", "\"dmat3\"");
                line = Regex.Replace(line, "<DMAT4>", "\"dmat4\"");
                line = Regex.Replace(line, "<DMAT2X2>", "\"dmat2x2\"");
                line = Regex.Replace(line, "<DMAT2X3>", "\"dmat2x3\"");
                line = Regex.Replace(line, "<DMAT2X4>", "\"dmat2x4\"");
                line = Regex.Replace(line, "<DMAT3X2>", "\"dmat3x2\"");
                line = Regex.Replace(line, "<DMAT3X3>", "\"dmat3x3\"");
                line = Regex.Replace(line, "<DMAT3X4>", "\"dmat3x4\"");
                line = Regex.Replace(line, "<DMAT4X2>", "\"dmat4x2\"");
                line = Regex.Replace(line, "<DMAT4X3>", "\"dmat4x3\"");
                line = Regex.Replace(line, "<DMAT4X4>", "\"dmat4x4\"");
                line = Regex.Replace(line, "<ATOMIC_UINT>", "\"atomic_uint\"");
                line = Regex.Replace(line, "<SAMPLER1D>", "\"sampler1D\"");
                line = Regex.Replace(line, "<SAMPLER2D>", "\"sampler2D\"");
                line = Regex.Replace(line, "<SAMPLER3D>", "\"sampler3D\"");
                line = Regex.Replace(line, "<SAMPLERCUBE>", "\"samplerCube\"");
                line = Regex.Replace(line, "<SAMPLER1DSHADOW>", "\"sampler1DShadow\"");
                line = Regex.Replace(line, "<SAMPLER2DSHADOW>", "\"sampler2DShadow\"");
                line = Regex.Replace(line, "<SAMPLERCUBESHADOW>", "\"samplerCubeShadow\"");
                line = Regex.Replace(line, "<SAMPLER1DARRAY>", "\"sampler1DArray\"");
                line = Regex.Replace(line, "<SAMPLER2DARRAY>", "\"sampler2DArray\"");
                line = Regex.Replace(line, "<SAMPLER1DARRAYSHADOW>", "\"sampler1DArrayShadow\"");
                line = Regex.Replace(line, "<SAMPLER2DARRAYSHADOW>", "\"sampler2DArrayShadow\"");
                line = Regex.Replace(line, "<SAMPLERCUBEARRAY>", "\"samplerCubeArray\"");
                line = Regex.Replace(line, "<SAMPLERCUBEARRAYSHADOW>", "\"samplerCubeArrayShadow\"");
                line = Regex.Replace(line, "<ISAMPLER1D>", "\"isampler1D\"");
                line = Regex.Replace(line, "<ISAMPLER2D>", "\"isampler2D\"");
                line = Regex.Replace(line, "<ISAMPLER3D>", "\"isampler3D\"");
                line = Regex.Replace(line, "<ISAMPLERCUBE>", "\"isamplerCube\"");
                line = Regex.Replace(line, "<ISAMPLER1DARRAY>", "\"isampler1DArray\"");
                line = Regex.Replace(line, "<ISAMPLER2DARRAY>", "\"isampler2DArray\"");
                line = Regex.Replace(line, "<ISAMPLERCUBEARRAY>", "\"isamplerCubeArray\"");
                line = Regex.Replace(line, "<USAMPLER1D>", "\"usampler1D\"");
                line = Regex.Replace(line, "<USAMPLER2D>", "\"usampler2D\"");
                line = Regex.Replace(line, "<USAMPLER3D>", "\"usampler3D\"");
                line = Regex.Replace(line, "<USAMPLERCUBE>", "\"usamplerCube\"");
                line = Regex.Replace(line, "<USAMPLER1DARRAY>", "\"usampler1DArray\"");
                line = Regex.Replace(line, "<USAMPLER2DARRAY>", "\"usampler2DArray\"");
                line = Regex.Replace(line, "<USAMPLERCUBEARRAY>", "\"usamplerCubeArray\"");
                line = Regex.Replace(line, "<SAMPLER2DRECT>", "\"sampler2DRect\"");
                line = Regex.Replace(line, "<SAMPLER2DRECTSHADOW>", "\"sampler2DRectShadow\"");
                line = Regex.Replace(line, "<ISAMPLER2DRECT>", "\"isampler2DRect\"");
                line = Regex.Replace(line, "<USAMPLER2DRECT>", "\"usampler2DRect\"");
                line = Regex.Replace(line, "<SAMPLERBUFFER>", "\"samplerBuffer\"");
                line = Regex.Replace(line, "<ISAMPLERBUFFER>", "\"isamplerBuffer\"");
                line = Regex.Replace(line, "<USAMPLERBUFFER>", "\"usamplerBuffer\"");
                line = Regex.Replace(line, "<SAMPLER2DMS>", "\"sampler2DMS\"");
                line = Regex.Replace(line, "<ISAMPLER2DMS>", "\"isampler2DMS\"");
                line = Regex.Replace(line, "<USAMPLER2DMS>", "\"usampler2DMS\"");
                line = Regex.Replace(line, "<SAMPLER2DMSARRAY>", "\"sampler2DMSArray\"");
                line = Regex.Replace(line, "<ISAMPLER2DMSARRAY>", "\"isampler2DMSArray\"");
                line = Regex.Replace(line, "<USAMPLER2DMSARRAY>", "\"usampler2DMSArray\"");
                line = Regex.Replace(line, "<IMAGE1D>", "\"image1D\"");
                line = Regex.Replace(line, "<IIMAGE1D>", "\"iimage1D\"");
                line = Regex.Replace(line, "<UIMAGE1D>", "\"uimage1D\"");
                line = Regex.Replace(line, "<IMAGE2D>", "\"image2D\"");
                line = Regex.Replace(line, "<IIMAGE2D>", "\"iimage2D\"");
                line = Regex.Replace(line, "<UIMAGE2D>", "\"uimage2D\"");
                line = Regex.Replace(line, "<IMAGE3D>", "\"image3D\"");
                line = Regex.Replace(line, "<IIMAGE3D>", "\"iimage3D\"");
                line = Regex.Replace(line, "<UIMAGE3D>", "\"uimage3D\"");
                line = Regex.Replace(line, "<IMAGE2DRECT>", "\"image2DRect\"");
                line = Regex.Replace(line, "<IIMAGE2DRECT>", "\"iimage2DRect\"");
                line = Regex.Replace(line, "<UIMAGE2DRECT>", "\"uimage2DRect\"");
                line = Regex.Replace(line, "<IMAGECUBE>", "\"imageCube\"");
                line = Regex.Replace(line, "<IIMAGECUBE>", "\"iimageCube\"");
                line = Regex.Replace(line, "<UIMAGECUBE>", "\"uimageCube\"");
                line = Regex.Replace(line, "<IMAGEBUFFER>", "\"imageBuffer\"");
                line = Regex.Replace(line, "<IIMAGEBUFFER>", "\"iimageBuffer\"");
                line = Regex.Replace(line, "<UIMAGEBUFFER>", "\"uimageBuffer\"");
                line = Regex.Replace(line, "<IMAGE1DARRAY>", "\"image1DArray\"");
                line = Regex.Replace(line, "<IIMAGE1DARRAY>", "\"iimage1DArray\"");
                line = Regex.Replace(line, "<UIMAGE1DARRAY>", "\"uimage1DArray\"");
                line = Regex.Replace(line, "<IMAGE2DARRAY>", "\"image2DArray\"");
                line = Regex.Replace(line, "<IIMAGE2DARRAY>", "\"iimage2DArray\"");
                line = Regex.Replace(line, "<UIMAGE2DARRAY>", "\"uimage2DArray\"");
                line = Regex.Replace(line, "<IMAGECUBEARRAY>", "\"imageCubeArray\"");
                line = Regex.Replace(line, "<IIMAGECUBEARRAY>", "\"iimageCubeArray\"");
                line = Regex.Replace(line, "<UIMAGECUBEARRAY>", "\"uimageCubeArray\"");
                line = Regex.Replace(line, "<IMAGE2DMS>", "\"image2DMS\"");
                line = Regex.Replace(line, "<IIMAGE2DMS>", "\"iimage2DMS\"");
                line = Regex.Replace(line, "<UIMAGE2DMS>", "\"uimage2DMS\"");
                line = Regex.Replace(line, "<IMAGE2DMSARRAY>", "\"image2DMSArray\"");
                line = Regex.Replace(line, "<IIMAGE2DMSARRAY>", "\"iimage2DMSArray\"");
                line = Regex.Replace(line, "<UIMAGE2DMSARRAY>", "\"uimage2DMSArray\"");
                line = Regex.Replace(line, "<HIGH_PRECISION>", "\"high_precision\"");
                line = Regex.Replace(line, "<MEDIUM_PRECISION>", "\"medium_precision\"");
                line = Regex.Replace(line, "<LOW_PRECISION>", "\"low_precision\"");
                line = Regex.Replace(line, "<STRUCT>", "\"struct\"");
                line = Regex.Replace(line, "<LEFT_BRACE>", "\"{\"");
                line = Regex.Replace(line, "<RIGHT_BRACE>", "\"}\"");
                line = Regex.Replace(line, "<SEMICOLON>", "\";\"");
                line = Regex.Replace(line, "<IF>", "\"if\"");
                line = Regex.Replace(line, "<ELSE>", "\"else\"");
                line = Regex.Replace(line, "<SWITCH>", "\"switch\"");
                line = Regex.Replace(line, "<CASE>", "\"case\"");
                line = Regex.Replace(line, "<DEFAULT>", "\"default\"");
                line = Regex.Replace(line, "<WHILE>", "\"while\"");
                line = Regex.Replace(line, "<DO>", "\"do\"");
                line = Regex.Replace(line, "<FOR>", "\"for\"");
                line = Regex.Replace(line, "<CONTINUE>", "\"continue\"");
                line = Regex.Replace(line, "<BREAK>", "\"break\"");
                line = Regex.Replace(line, "<RETURN>", "\"return\"");
                line = Regex.Replace(line, "<DISCARD>", "\"discard\"");
                builder.AppendLine(line);
            }
            System.IO.File.WriteAllText(filename + "2." + extensionName, builder.ToString());
        }

        private static void v43Step1AddBracket(string filename, string extensionName)
        {
            StringBuilder builder = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(filename + "0." + extensionName);
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex].Trim();
                if (string.IsNullOrEmpty(line)) { continue; }
                if (line.StartsWith("//"))
                {
                    builder.AppendLine(lines[lineIndex].Replace("\t", "    "));
                    continue;
                }
                string[] parts = line.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                line = parts[0].Trim();
                if (line.EndsWith(":"))
                {
                    line = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    builder.Append('<'); builder.Append(line); builder.Append("> ::=");
                    for (int i = 1; i < parts.Length; i++) { builder.Append(" //"); builder.Append(parts[i]); }
                    builder.AppendLine();
                }
                else
                {
                    builder.Append("    ");
                    foreach (var part in line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        builder.Append('<'); builder.Append(part); builder.Append("> ");
                    }
                    bool lastProduction = true;
                    if (lineIndex + 1 < lines.Length)
                    {
                        for (int i = lineIndex + 1; i < lines.Length; i++)
                        {
                            if (lines[i].Trim().IndexOf("//") == 0) { continue; }
                            var p = lines[i].Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                            if (p.Length == 0) { continue; }
                            if (p[0].Trim().EndsWith(":"))
                            {
                                lastProduction = true;
                                break;
                            }
                            else if (p[0].Trim().Length > 0)
                            {
                                lastProduction = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        lastProduction = true;
                    }
                    if (lastProduction)
                    {
                        builder.Append(";");
                        for (int i = 1; i < parts.Length; i++) { builder.Append(" //"); builder.Append(parts[i]); }
                        builder.AppendLine();
                    }
                    else
                    {
                        builder.Append("|");
                        for (int i = 1; i < parts.Length; i++) { builder.Append(" //"); builder.Append(parts[i]); }
                        builder.AppendLine();
                    }
                }
            }
            System.IO.File.WriteAllText(filename + "1." + extensionName, builder.ToString());
        }

        private static void v43Step0RemoveEmptyLines(string filename, string extensionName)
        {
            StringBuilder builder = new StringBuilder();
            string[] lines = System.IO.File.ReadAllLines(filename + "." + extensionName);
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex].Trim();
                //if (string.IsNullOrEmpty(line)) { continue; }
                //else
                {
                    builder.AppendLine(lines[lineIndex]);
                }
            }
            System.IO.File.WriteAllText(filename + "0." + extensionName, builder.ToString());
        }

    }
}
