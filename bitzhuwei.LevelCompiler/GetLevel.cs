using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.LevelCompiler
{
    public static partial class GetLevel
    {
        public static Level GetValue(this SyntaxTree<EnumTokenTypeLevelCompiler,
            EnumVTypeLevelCompiler,TreeNodeValueLevelCompiler> tree)
        {
            if (tree == null) { return null; }

            var result = new Level();
            _GetLevel(result, tree);
            return result;
        }

        private static void _GetLevel(Level result, SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> tree)
        {
            if (tree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_Level___tail_levelLeave())
            {
                GetStepList(result.Steps, tree.Children[2]);
            }
        }

        private static void GetStepList(Queue<LevelStep> queue, SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if (syntaxTree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_StepList___tail_stepLeave())
            {
                var step = new LevelStep();
                GetStep(step, syntaxTree.Children[0]);
                queue.Enqueue(step);
                GetStepList(queue, syntaxTree.Children[1]);
            }
            else if(syntaxTree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_StepList___tail_rightBrace_Leave())
            {
                //nothing to do
            }
        }

        private static void GetStep(LevelStep step, SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if (syntaxTree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_Step___tail_stepLeave())
            {
                GetTankList(step, syntaxTree.Children[2]);
            }
        }

        private static void GetTankList(LevelStep step, SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if(syntaxTree.CandidateFunc==LL1SyntaxParserLevelCompiler.GetFuncParsecase_TankList___tail_tankLeave())
            {
                var egg = GetTank(syntaxTree.Children[0]);
                step.enemyEggs.Enqueue(egg);
                GetTankList(step, syntaxTree.Children[1]);
            }
            else if(syntaxTree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_TankList___tail_rightBrace_Leave())
            {
                //nothing to do
            }

        }

        private static EnemyEgg GetTank(SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if (syntaxTree.CandidateFunc== LL1SyntaxParserLevelCompiler.GetFuncParsecase_Tank___tail_tankLeave())
            {
                var tankPrefab = GetTankPrefab(syntaxTree.Children[2]);
                var bornPoint = GetBornPoint(syntaxTree.Children[3]);
                var result = new EnemyEgg(tankPrefab, bornPoint);
                return result;
            }

            Debug.Write(string.Format("syntaxTree.CandidateFunc should be <Tank> ::=..."));
            return null;
        }

        private static int GetBornPoint(SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if (syntaxTree.CandidateFunc==LL1SyntaxParserLevelCompiler.GetFuncParsecase_BornPoint___numberLeave())
            {
                var result = int.Parse(syntaxTree.Children[0].NodeValue.NodeName);
                return result;
            }

            Debug.Write(string.Format("syntaxTree.CandidateFunc should be <TankPrefab> ::=..."));
            return 0;
        }

        private static int GetTankPrefab(SyntaxTree<EnumTokenTypeLevelCompiler, EnumVTypeLevelCompiler, TreeNodeValueLevelCompiler> syntaxTree)
        {
            if(syntaxTree.CandidateFunc==LL1SyntaxParserLevelCompiler.GetFuncParsecase_TankPrefab___numberLeave())
            {
                var result = int.Parse(syntaxTree.Children[0].NodeValue.NodeName);
                return result;
            }

            Debug.Write(string.Format("syntaxTree.CandidateFunc should be <BornPoint> ::=..."));
            return 0;
        }
    }
}
