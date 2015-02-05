using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 提供SyntaxTree&lt;EnumTokenTypeCG, EnumVTypeCG, SyntaxTreeNodeValueCG&gt;的扩展方法
    /// </summary>
    public static partial class SyntaxTreeCG
    {
        /// <summary>
        /// 获取源代码的规范格式
        /// <para>语法分析的副产品</para>
        /// </summary>
        /// <param name="tree">语法树</param>
        /// <returns></returns>
        public static string GetFormatted(this SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tree)
        {
            if (tree == null)
#if DEBUG
                return string.Format("param in extension function: public static string GetFormatted(this SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tree) is null.");
#else
                return string.Empty;
#endif
            //if (tree.SyntaxError)
            //    return "此语法树有语法错误！";
            StringBuilder builder = new StringBuilder();
            try
            {
                _GetFormattedSourceCode(builder, tree);
            }
            catch (System.Exception)
            {
            	
            }
            return builder.ToString();
        }

        private static void _GetFormattedSourceCode(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tree)
        {
            if (tree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Start___tail_lessThan_Leave())
            {//<Start> ::= <Vn> "::=" <VList> ";" <PList>; 1
                GetFormattedVn(builder, tree.Children[0]);
                builder.Append(" ");
                GetFormattedpointToLeave(builder, tree.Children[1]);
                builder.Append(" ");
                GetFormattedVList(builder, tree.Children[2]);
                GetFormattedsemicolonLeave(builder, tree.Children[3]);
                builder.AppendLine();
                GetFormattedPList(builder, tree.Children[4]);
            }
            else
            {
                builder.Append(string.Format("error: ProductionNodeList {0} not found", tree.CandidateFunc));
            }
        }
        /// <summary>
        /// $#$
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="tree"></param>
        private static void GetFormattedstartEndLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tree)
        {//$#$
            //todo: builder.Append("$#$");//用于显示终结符结点
            //builder.Append("$#$");
        }
        /// <summary>
        /// 2 &lt;PList&gt; ::= &lt;Vn&gt; "::=" &lt;VList&gt; ";" &lt;PList&gt;;
        /// <para>3 &lt;PList&gt; ::= null;</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedPList(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<PList> ::= <Vn> "::=" <VList> ";" <PList> | null; 2 3
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_PList___tail_lessThan_Leave())
            {
                GetFormattedVn(builder, syntaxTree.Children[0]);
                builder.Append(" ");
                GetFormattedpointToLeave(builder, syntaxTree.Children[1]);
                builder.Append(" ");
                GetFormattedVList(builder, syntaxTree.Children[2]);
                GetFormattedsemicolonLeave(builder, syntaxTree.Children[3]);
                builder.AppendLine();
                GetFormattedPList(builder, syntaxTree.Children[4]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_PList___tail_startEndLeave())
            {
                // nothing to do
                //GetFormattednull(builder, syntaxTree.Children[0]);
            }
        }
        /// <summary>
        /// ;
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedsemicolonLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//;
            builder.Append(syntaxTree.NodeValue.NodeName);
        }
        /// <summary>
        /// 4 &lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedVList(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<VList> ::= <V> <VOpt>; 4
            GetFormattedV(builder, syntaxTree.Children[0]);
            //builder.Append(" ");
            GetFormattedVOpt(builder, syntaxTree.Children[1]);
        }
        /// <summary>
        /// 7 &lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// <para>8 &lt;VOpt&gt; ::= "|" &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <para>9 &lt;VOpt ::= null;</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedVOpt(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<VOpt> ::= <V> <VOpt> | "|" <V> <VOpt> | null; // 7 8 9
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_identifierLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_lessThan_Leave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_nullLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_numberLeave())
            {
                builder.Append(" ");
                GetFormattedV(builder, syntaxTree.Children[0]);
                //builder.Append(" ");
                GetFormattedVOpt(builder, syntaxTree.Children[1]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_or_Leave())
            {
                builder.Append(" ");
                GetFormattedorLeave(builder, syntaxTree.Children[0]);
                builder.Append(" ");
                GetFormattedV(builder, syntaxTree.Children[1]);
                //builder.Append(" ");
                GetFormattedVOpt(builder, syntaxTree.Children[2]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_semicolon_Leave())
            {
                GetFormattednull(builder, syntaxTree.Children[0]);
            }
        }

        private static void GetFormattednull(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {
            // nothing needed
            //todo: builder.Append("$null$");//用于显示null结点
            //builder.Append("$null$");
        }

        private static void GetFormattedorLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {
            builder.Append(syntaxTree.NodeValue.NodeName);
        }
        /// <summary>
        /// 5 &lt;V&gt; ::= &lt;Vn&gt;;
        /// <para>6 &lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <para>7 &lt;V&gt; ::= identifier;</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedV(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<V> ::= <Vn> | <Vt> | identifier; 5 6 7
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_lessThan_Leave())
            {
                GetFormattedVn(builder, syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_identifierLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_nullLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_numberLeave())
            {
                GetFormattedVt(builder, syntaxTree.Children[0]);
            }
            else
                builder.Append(string.Format("error: {0} not matching [{1}] or [{2}] or [{3}] or [{4}] or [{5}] or [{6}]",
                    syntaxTree.Children[0].CandidateFunc,
                    LL1SyntaxParserCG.GetFuncParsecase_V___tail_lessThan_Leave(),
                    LL1SyntaxParserCG.GetFuncParsecase_V___constStringLeave(),
                    LL1SyntaxParserCG.GetFuncParsecase_V___tail_constStringLeave(),
                    LL1SyntaxParserCG.GetFuncParsecase_V___tail_identifierLeave(),
                    LL1SyntaxParserCG.GetFuncParsecase_V___tail_nullLeave(),
                    LL1SyntaxParserCG.GetFuncParsecase_V___tail_numberLeave()));
        }
        /// <summary>
        /// 11 &lt;Vt&gt; ::= "null";
        /// <para>12 &lt;Vt&gt; ::= "identifier";</para>
        /// <para>13 &lt;Vt&gt; ::= "number";</para>
        /// <para>14 &lt;Vt&gt; ::= "constString";</para>
        /// <para>15 &lt;Vt&gt; ::= constString;</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedVt(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<Vt> ::= "null" | "identifier" | "number" | "constString" | constString; // 11 12 13 14 15
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_nullLeave())
            {
                builder.Append(ProductionNode.tail_null.NodeName);
                //GetFormattednull(builder, syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_identifierLeave())
            {
                builder.Append(ProductionNode.tail_identifier.NodeName);
                //GetFormattedidentifierLeave(builder, syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_numberLeave())
            {
                builder.Append(ProductionNode.tail_number.NodeName);
                //GetFormattednumberLeave(builder, syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_constStringLeave())
            {
                builder.Append(ProductionNode.tail_constString.NodeName);
                //GetFormattedconstString(builder, syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___constStringLeave())
            {
                GetFormattedconstString(builder, syntaxTree.Children[0]);
            }
        }
        /// <summary>
        /// "xxx"
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedconstString(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//"xxx"
            builder.Append(syntaxTree.NodeValue.NodeName);
        }
        /// <summary>
        /// ::=
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedpointToLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//::=
            builder.Append(syntaxTree.NodeValue.NodeName);
        }
        /// <summary>
        /// 10 &lt;Vn&gt; ::= "&lt;" identifier &gt;;
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedVn(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<Vn> ::= "<" identifier ">"; 10
            GetFormattedlessThanLeave(builder, syntaxTree.Children[0]);
            GetFormattedidentifierLeave(builder, syntaxTree.Children[1]);
            GetFormattedgreaterThan(builder, syntaxTree.Children[2]);
        }
        /// <summary>
        /// &lt;
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedlessThanLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<
            builder.Append(syntaxTree.NodeValue.NodeName);
        }

        /// <summary>
        /// identifier
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedidentifierLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//identifier
            builder.Append(syntaxTree.NodeValue.NodeName);
        }

        /// <summary>
        /// identifier
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattednumberLeave(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//identifier
            builder.Append(syntaxTree.NodeValue.NodeName);
        }

        /// <summary>
        /// &gt;
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="syntaxTree"></param>
        private static void GetFormattedgreaterThan(StringBuilder builder, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//>
            builder.Append(syntaxTree.NodeValue.NodeName);
        }



    }
}
