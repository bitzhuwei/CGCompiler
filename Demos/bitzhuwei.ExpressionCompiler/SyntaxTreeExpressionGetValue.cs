using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.ExpressionCompiler
{
    /// <summary>
    /// 提供SyntaxTree&lt;EnumTokenTypeCG, EnumVTypeCG, SyntaxTreeNodeValueCG&gt;的扩展方法
    /// </summary>
    public static partial class SyntaxTreeExpression
    {
        /// <summary>
        /// 获取源代码的规范格式
        /// <para>语法分析的副产品</para>
        /// </summary>
        /// <param name="tree">语法树</param>
        /// <returns></returns>
        public static double GetValue(this SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> tree)
        {
            if (tree == null) return double.NaN;

            var tmpTree = tree.Clone() as SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>;
            _GetValue(tmpTree);

            return double.Parse(tmpTree.Tag.ToString());
        }

        private static void _GetValue(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> tree)
        {
            switch (tree.NodeValue.NodeType)
            {
                case EnumVTypeExpression.Unknown:
                    break;
                case EnumVTypeExpression.case_Expression://<Expression> ::= <Multiply> <PlusOpt>;
                    _GetValue(tree.Children[0]);
                    _GetValue(tree.Children[1]);
                    tree.Tag = double.Parse(tree.Children[0].Tag.ToString()) + double.Parse(tree.Children[1].Tag.ToString());
                    break;
                case EnumVTypeExpression.case_PlusOpt://<PlusOpt> ::= "+" <Multiply> | "-" <Multiply> | null;
                    if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_plus_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = tree.Children[1].Tag;
                    }
                    else if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_minus_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = -double.Parse(tree.Children[1].Tag.ToString());
                    }
                    else if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_rightParentheses_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_startEndLeave())
                    {
                        tree.Tag = 0;
                    }
                    break;
                case EnumVTypeExpression.case_Multiply://<Multiply> ::= <Unit> <MultiplyOpt>;
                    _GetValue(tree.Children[0]);
                    _GetValue(tree.Children[1]);
                    tree.Tag = double.Parse(tree.Children[0].Tag.ToString()) * double.Parse(tree.Children[1].Tag.ToString());
                    break;
                case EnumVTypeExpression.case_MultiplyOpt://<MultiplyOpt> ::= "*" <Unit> | "/" <Unit> | null;
                    if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_multiply_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = double.Parse(tree.Children[1].Tag.ToString());
                    }
                    else if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_divide_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = 1 / (double)tree.Children[1].Tag;
                    }
                    else if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_plus_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_minus_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_rightParentheses_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_startEndLeave())
                    {
                        tree.Tag = 1;
                    }
                    break;
                case EnumVTypeExpression.case_Unit://<Unit> ::= number | "(" <Expression> ")";
                    if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_Unit___numberLeave())
                    {
                        tree.Tag = double.Parse(tree.Children[0].NodeValue.NodeName);
                    }
                    else if (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_Unit___tail_leftParentheses_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = tree.Children[1].Tag;
                    }
                    break;
                default:
                    break;
            }
        }


    }
}
