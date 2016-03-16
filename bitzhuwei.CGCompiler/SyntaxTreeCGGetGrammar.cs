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
        /// 获取文法
        /// <para>这部分是语义分析</para>
        /// </summary>
        /// <param name="tree">语法树</param>
        /// <returns></returns>
        public static ContextfreeGrammar GetGrammar(this SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tree)
        {
            if (tree == null) return null;
            var result = _GetGrammar(tree);
            return result;
        }

        private static ContextfreeGrammar _GetGrammar(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {
            ContextfreeGrammar result = null;
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Start___tail_lessThan_Leave())
            {//<Start> ::= <Vn> "::=" <VList> ";" <PList>; 1
                result = new ContextfreeGrammar();

                var startProduction = new ContextfreeProduction();
                result.ProductionCollection.Add(startProduction);

                var left = GetGrammarVn(syntaxTree.Children[0]);
                startProduction.Left = left;
                result.GrammarName = left.NodeName;

                var vlist = GetGrammarVList(syntaxTree.Children[2]);
                startProduction.RightCollection = vlist;

                GetGrammarPList(result.ProductionCollection, syntaxTree.Children[4]);
            }

            return result;
        }

        /// <summary>
        /// $#$
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static object GetGrammarstartEndLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//$#$
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// 2 &lt;PList&gt; ::= &lt;Vn&gt; "::=" &lt;VList&gt; ";" &lt;PList&gt;;
        /// <para>3 &lt;PList&gt; ::= null;</para>
        /// </summary>
        /// <param name="plist"></param>
        /// <param name="syntaxTree"></param>
        private static void GetGrammarPList(
            ContextfreeProductionList plist, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<PList> ::= <Vn> "::=" <VList> ";" <PList> | null; 2 3
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_PList___tail_lessThan_Leave())
            {
                ContextfreeProduction production = new ContextfreeProduction();
                plist.Add(production);

                var vn = GetGrammarVn(syntaxTree.Children[0]);
                production.Left = vn;

                var vlist = GetGrammarVList(syntaxTree.Children[2]);
                production.RightCollection = vlist;

                GetGrammarPList(plist, syntaxTree.Children[4]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_PList___tail_startEndLeave())
            {
                // nothing to do
                //GetGrammarnull(syntaxTree.Children[0]);
            }
        }
        /// <summary>
        /// ;
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static object GetGrammarsemicolonLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//;
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// 4 &lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static RightSection GetGrammarVList(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<VList> ::= <V> <VOpt>; 4
            RightSection vlist = new RightSection();

            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___tail_constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___tail_identifierLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___tail_lessThan_Leave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___tail_nullLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VList___tail_numberLeave())
            {
                var candidate = new ProductionNodeList();
                vlist.Add(candidate);

                var v = GetGrammarV(syntaxTree.Children[0]);
                candidate.Add(v);

                var info = GetGrammarVOpt(vlist, candidate, syntaxTree.Children[1]);
            }

            return vlist;
        }
        /// <summary>
        /// 7 &lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// <para>8 &lt;VOpt&gt; ::= "|" &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <para>9 &lt;VOpt ::= null;</para>
        /// </summary>
        /// <param name="vlist"></param>
        /// <param name="candidate"></param>
        /// <param name="syntaxTree"></param>
        private static object GetGrammarVOpt(RightSection vlist,
            ProductionNodeList candidate, SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<VOpt> ::= <V> <VOpt> | "|" <V> <VOpt> | null; // 7 8 9
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_nullLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_identifierLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_numberLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_lessThan_Leave()
                )
            {
                var v = GetGrammarV(syntaxTree.Children[0]);
                candidate.Add(v);

                return GetGrammarVOpt(vlist, candidate, syntaxTree.Children[1]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_or_Leave())
            {
                ProductionNodeList newCandidate = new ProductionNodeList();
                vlist.Add(newCandidate);

                var v = GetGrammarV(syntaxTree.Children[1]);
                newCandidate.Add(v);

                return GetGrammarVOpt(vlist, newCandidate, syntaxTree.Children[2]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_VOpt___tail_semicolon_Leave())
            {
                return GetGrammarnull(syntaxTree.Children[0]);
            }
            else
                return string.Format("{0}", syntaxTree.CandidateFunc.ToString());
        }

        private static object GetGrammarnull(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// |
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static object GetGrammarorLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {// |
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// 5 &lt;V&gt; ::= &lt;Vn&gt;;
        /// <para>6 &lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <para>7 &lt;V&gt; ::= identifier;</para>
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static ProductionNode GetGrammarV(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<V> ::= <Vn> | <Vt>; 5 6
            ProductionNode result = null;
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_lessThan_Leave())
            {
                result = GetGrammarVn(syntaxTree.Children[0]);
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_constStringLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_identifierLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_nullLeave()
                || syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_V___tail_numberLeave())
            {
                result = GetGrammarVt(syntaxTree.Children[0]);
            }
            else
                result = new ProductionNode(EnumVTypeCG.case_V.ToString(), EnumVTypeCG.case_V.ToString(), EnumProductionNodePosition.Unknown);
            return result;
        }
        /// <summary>
        /// 11 &lt;Vt&gt; ::= "null";
        /// <para>12 &lt;Vt&gt; ::= "identifier";</para>
        /// <para>13 &lt;Vt&gt; ::= "number";</para>
        /// <para>14 &lt;Vt&gt; ::= "constString";</para>
        /// <para>15 &lt;Vt&gt; ::= identifier;</para>
        /// <para>16 &lt;Vt&gt; ::= number;</para>
        /// <para>17 &lt;Vt&gt; ::= constString;</para>
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static ProductionNode GetGrammarVt(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<Vt> ::= "null" | "identifier" | "number" | "constString" | identifier | number | constString; // 11 12 13 14 15 16 17
            ProductionNode result = null;
            if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_nullLeave())
            {
                result = ProductionNode.tail_null;
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_identifierLeave())
            {
                result = ProductionNode.tail_identifier;
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_numberLeave())
            {
                result = ProductionNode.tail_number;
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___tail_constStringLeave())
            {
                result = ProductionNode.tail_constString;
            }
            else if (syntaxTree.CandidateFunc == LL1SyntaxParserCG.GetFuncParsecase_Vt___constStringLeave())
            {
                var name = GetGrammarconstString(syntaxTree.Children[0]);
                result = new ProductionNode(name, name, EnumProductionNodePosition.Leave);
            }

            return result;
        }
        /// <summary>
        /// "xxx"
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static string GetGrammarconstString(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//"xxx"
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// ::=
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static object GetGrammarpointToLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//::=
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// 10 &lt;Vn&gt; ::= "&lt;" identifier "&gt";
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static ProductionNode GetGrammarVn(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<Vn> ::= "<" identifier ">"; // 10
            var identifier = GetGrammaridentifierLeave(syntaxTree.Children[1]);
            ProductionNode VnNode = new ProductionNode(
                identifier, identifier/*EnumVTypeCG.Vn.ToString()*/, EnumProductionNodePosition.NonLeave);
            return VnNode;
        }
        /// <summary>
        /// &lt;
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static string GetGrammarlessThanLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//<
            return syntaxTree.NodeValue.NodeName;
        }

        /// <summary>
        /// identifier
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static string GetGrammaridentifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//identifier
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// number
        /// </summary>
        /// <param name="syntaxTree"></param>
        /// <returns></returns>
        private static string GetGrammarnumberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//number
            return syntaxTree.NodeValue.NodeName;
        }
        /// <summary>
        /// &gt;
        /// </summary>
        /// <param name="syntaxTree"></param>
        private static object GetGrammargreaterThan(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> syntaxTree)
        {//>
            //vlist.Append(syntaxTree.NodeValue.NodeName);
            return syntaxTree.NodeValue.NodeName;
        }

    }
}
