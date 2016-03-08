using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;

namespace bitzhuwei.CGCompiler.Winform
{
    public partial class FormMain4BuildingGrammar : Form
    {
        class CodeGeneratorParam
        {
            public string sourceCode;
            public string compilerName;
            public string folder;
            public string _namespace;
            public CodeGeneratorParam(string sourceCode, string compilerName, string _namespace, string folder)
            {
                this.sourceCode = sourceCode;
                this.compilerName = compilerName;
                this._namespace = _namespace;
                this.folder = folder;
            }
        }

        /// 
        /// </summary>
        /// <param name="sourceCode"></param>
        public FormMain4BuildingGrammar(string sourceCode)
        {
            InitializeComponent();
            this.txtSourceCode.Text = sourceCode;

            List<ProductionNode> notImplementedNodeList = GetNotImplementedNodeList(sourceCode);
            StringBuilder builder = new StringBuilder();
            foreach (var node in notImplementedNodeList)
            {
                builder.Append('<');
                builder.Append(node.NodeName);
                builder.Append('>');
                builder.Append(" ::= ");
                builder.Append("\"");
                builder.Append("tmp_" + node.NodeName);
                builder.Append("\"");
                builder.Append(" ;");
                builder.AppendLine();
            }
            this.txtSourceCode.AppendText(Environment.NewLine);
            this.txtSourceCode.AppendText(builder.ToString());

        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (generatedCodeFolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtCodeFolder.Text = generatedCodeFolderBrowser.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (codeGenerator.IsBusy)
            {
                MessageBox.Show("上次计算尚未完成，请稍候。");
                return;
            }
            if (string.IsNullOrEmpty(txtNamespace.Text))
            {
                MessageBox.Show("命名空间不要不填哦亲！", "提示");
                return;
            }
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (txtNamespace.Text.Contains(c))
                {
                    MessageBox.Show("命名空间不要包含字符【" + c + "】哦亲！", "提示");
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtCompilerName.Text))
            {
                MessageBox.Show("编译器名不要不填哦亲！", "提示");
                return;
            }
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (txtCompilerName.Text.Contains(c))
                {
                    MessageBox.Show("编译器名不要包含字符【" + c + "】哦亲！", "提示");
                    return;
                }
            }
            if (txtCompilerName.Text.Contains('.'))
            {
                MessageBox.Show("编译器名不要包含字符【" + "." + "】哦亲！", "提示");
                return;
            }
            if (string.IsNullOrEmpty(txtCodeFolder.Text))
            {
                MessageBox.Show("生成代码存放的位置不要不选哦亲！", "提示");
                return;
            }
            if (!Directory.Exists(txtCodeFolder.Text))
            {
                MessageBox.Show("您选择的文件夹不存在哦亲！", "提示");
                return;
            }

            this.pgbProcess.Value = this.pgbProcess.Minimum;
            this.txtProcess.Text = string.Empty;

            codeGenerator.RunWorkerAsync(
                new CodeGeneratorParam(this.txtSourceCode.Text, txtCompilerName.Text, txtNamespace.Text, txtCodeFolder.Text));


        }

        private void codeGenerator_DoWork(object sender, DoWorkEventArgs e)
        {
            var param = e.Argument as CodeGeneratorParam;
            ContextfreeGrammar grammar = null;
            LL1GeneraterInput input = null;
            {
                codeGenerator.ReportProgress(6, "文法代码已加载！");
                var lexAna = new bitzhuwei.CGCompiler.LexicalAnalyzerCG();
                lexAna.SetSourceCode(param.sourceCode);
                var tokens = lexAna.Analyze();
                codeGenerator.ReportProgress(12, "得到文法代码的单词列表！");
                var syntaxParser = new bitzhuwei.CGCompiler.LL1SyntaxParserCG();
                syntaxParser.SetTokenListSource(tokens);
                var tree = syntaxParser.Parse();
                codeGenerator.ReportProgress(18, "得到文法代码的语法树！");
                grammar = tree.GetGrammar();
                grammar.GrammarName = param.compilerName;
                grammar.Namespace = param._namespace;
                codeGenerator.ReportProgress(24, "得到文法的数据结构！");
            }
            //var grammar = TestGetCGGrammar();
            {
                var xml = grammar.ToXElement();
                var xmlFullname = Path.Combine(param.folder, param.compilerName + ".xml");
                xml.Save(xmlFullname);
                codeGenerator.ReportProgress(30, string.Format("文法数据结构已保存为[{0}]！", xmlFullname));
            }
            {
                var txt = grammar.ToString();
                var txtFullname = Path.Combine(param.folder, param.compilerName + ".txt");
                txt.Save(txtFullname);
                codeGenerator.ReportProgress(33, string.Format("文法已保存为[{0}]！", txtFullname));
            }
            {
                var firstCollection = grammar.GetFirstCollection();
                DumpFirstCollection(param, firstCollection);
                codeGenerator.ReportProgress(36, "得到文法的FIRST集！");
                if (firstCollection.Conflicts())
                {
                    var conflicted = firstCollection.GetConflicted();
                    codeGenerator.ReportProgress(37, "冲突的FIRST集所在的产生式：" + Environment.NewLine + conflicted.ToString());
                    throw new Exception("您给定的文法的FIRST集有冲突，详情见状态框。");
                }
            }
            {
                var followCollection = grammar.GetFollowCollection();
                string fullname = Path.Combine(param.folder, "FollowCollection" + param.compilerName + ".txt");
                File.WriteAllText(fullname, followCollection.ToString());
            }
            codeGenerator.ReportProgress(42, "得到文法的FOLLOW集！");
            {
                var ll1ParserMap = grammar.GetLL1ParserMap();
                string fullname = Path.Combine(param.folder, "LL1ParserMap" + param.compilerName + ".txt");
                File.WriteAllText(fullname, ll1ParserMap.ToString());
                codeGenerator.ReportProgress(48, "得到文法的LL1分析表！");
            }
            input = new LL1GeneraterInput(grammar);
            {
                string fullname = Path.Combine(param.folder, "LexicalAnalyzer" + param.compilerName + ".cs");
                grammar.GenerateLexicalAnalyzer(fullname, input);
                codeGenerator.ReportProgress(54, string.Format("自动生成\"{0}\"的代码！", grammar.GetLexicalAnalyzerName()));
            }
            {
                var ll1ParserGenerated = grammar.GenerateLL1SyntaxParser(input);
                codeGenerator.ReportProgress(60, string.Format("自动生成\"{0}\"的代码！", grammar.GetLL1SyntaxParserName()));
                ll1ParserGenerated.Save(Path.Combine(param.folder, "LL1SyntaxParser" + param.compilerName + ".cs"));
            }
            {
                var enumCharTypeGenerated = grammar.GenerateEnumCharType(input);
                codeGenerator.ReportProgress(76, string.Format("自动生成\"{0}\"的代码！", grammar.GetEnumCharTypeSG()));
                enumCharTypeGenerated.Save(Path.Combine(param.folder, "EnumCharType" + param.compilerName + ".cs"));
            }
            {
                var enumTokenTypeGenerated = grammar.GenerateEnumTokenType(input);
                codeGenerator.ReportProgress(82, string.Format("自动生成\"{0}\"的代码！", grammar.GetEnumTokenTypeSG()));
                enumTokenTypeGenerated.Save(Path.Combine(param.folder, "EnumTokenType" + param.compilerName + ".cs"));
            }
            {
                var enumVTypeGenerated = grammar.GenerateEnumVType(input);
                codeGenerator.ReportProgress(88, string.Format("自动生成\"{0}\"的代码！", grammar.GetEnumVTypeSG()));
                enumVTypeGenerated.Save(Path.Combine(param.folder, "EnumVType" + param.compilerName + ".cs"));
            }
            {
                var treeNodeValueTypeGenerated = grammar.GenerateTreeNodeValueType();
                codeGenerator.ReportProgress(94, string.Format("自动生成\"{0}\"的代码！", grammar.GetTreeNodeValueSG()));
                treeNodeValueTypeGenerated.Save(Path.Combine(param.folder, "SyntaxTreeNodeValue" + param.compilerName + ".cs"));
            }
            {
                File.Copy("bitzhuwei.CompilerBase.dll", Path.Combine(param.folder, "bitzhuwei.CompilerBase.dll"), true);
                File.Copy("使用说明.txt", Path.Combine(param.folder, "使用说明.txt"), true);
                codeGenerator.ReportProgress(98, string.Format("复制基类库文件和使用说明文件！"));
            }
            {
                e.Result = param;
                codeGenerator.ReportProgress(100, "代码生成成功！");
            }
        }

        private static void DumpFirstCollection(CodeGeneratorParam param, FIRSTCollection firstCollection)
        {
            string fullname = Path.Combine(param.folder, "FirstCollection" + param.compilerName + ".txt");
            File.WriteAllText(fullname, firstCollection.ToString());

            List<Tuple<FIRSTCollectionItem, FIRSTCollectionItem, List<ProductionNode>>> sameFirst =
                new List<Tuple<FIRSTCollectionItem, FIRSTCollectionItem, List<ProductionNode>>>();
            for (int i = 0; i < firstCollection.Count - 1; i++)
            {
                for (int j = i + 1; j < firstCollection.Count; j++)
                {
                    var rightA = firstCollection[i];
                    var rightB = firstCollection[j];
                    if (rightA.ObjectiveProduction != rightB.ObjectiveProduction) { continue; }
                    List<ProductionNode> sameNode = new List<ProductionNode>();
                    foreach (var item in rightA.Value)
                    {
                        if (rightB.Value.Contains(item))
                        {
                            sameNode.Add(item);
                        }
                    }
                    if (sameNode.Count > 0)
                    {
                        sameFirst.Add(new Tuple<FIRSTCollectionItem, FIRSTCollectionItem, List<ProductionNode>>(
                            rightA, rightB, sameNode));
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            if (sameFirst.Count > 0)
            {
                foreach (var item in sameFirst)
                {
                    builder.AppendLine(string.Format(
                        "产生式【{0}】的下列候选式的First集之间的交集不为空：", item.Item1.ObjectiveProduction));
                    builder.AppendLine("RightA:");
                    builder.Append("    ");
                    builder.AppendLine(item.Item1.ToString());
                    builder.AppendLine("RightB:");
                    builder.Append("    ");
                    builder.AppendLine(item.Item2.ToString());
                    builder.AppendLine("相同的成分:");
                    foreach (var member in item.Item3)
                    {
                        builder.Append(member);
                        builder.Append(", ");
                    }
                    builder.AppendLine();
                    builder.AppendLine();
                }
            }
            else
            {
                builder.AppendLine("First集没有问题。");
            }
            string errorFile = Path.Combine(param.folder, "FirstCollectionError" + param.compilerName + ".txt");
            File.WriteAllText(errorFile, builder.ToString());

            if (sameFirst.Count > 0)
            {
                Process.Start("notepad", errorFile);
            }
        }

        private ContextfreeGrammar TestGetCGGrammar()
        {
            var grammar = new ContextfreeGrammar();
            grammar.Namespace = "bitzhuwei.CGCompiler";
            grammar.GrammarName = "CG";

            var Start = new ProductionNode("Start", EnumProductionNodePosition.NonLeave);
            var PList = new ProductionNode("PList", EnumProductionNodePosition.NonLeave);
            var VList = new ProductionNode("VList", EnumProductionNodePosition.NonLeave);
            var V = new ProductionNode("V", EnumProductionNodePosition.NonLeave);
            var VOpt = new ProductionNode("VOpt", EnumProductionNodePosition.NonLeave);
            var Vn = new ProductionNode("Vn", EnumProductionNodePosition.NonLeave);
            var Vt = new ProductionNode("Vt", EnumProductionNodePosition.NonLeave);

            var colon_Colon_Equality_ = new ProductionNode("\"::=\"", EnumProductionNodePosition.Leave);
            var semicolon_ = new ProductionNode("\";\"", EnumProductionNodePosition.Leave);
            var or_ = new ProductionNode("\"|\"", EnumProductionNodePosition.Leave);
            var lessThan_ = new ProductionNode("\"<\"", EnumProductionNodePosition.Leave);
            var greaterThan_ = new ProductionNode("\">\"", EnumProductionNodePosition.Leave);
            var token_null = new ProductionNode("\"null\"", EnumProductionNodePosition.Leave);
            var token_identifier = new ProductionNode("\"identifier\"", EnumProductionNodePosition.Leave);
            var token_number = new ProductionNode("\"number\"", EnumProductionNodePosition.Leave);
            var token_constString = new ProductionNode("\"constString\"", EnumProductionNodePosition.Leave);

            {
                var startProd = new ContextfreeProduction();
                startProd.Left = Start;
                var candidate = new ProductionNodeList();
                candidate.Add(Vn); candidate.Add(colon_Colon_Equality_); candidate.Add(VList); candidate.Add(semicolon_); candidate.Add(PList);
                startProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(startProd);
            }

            {
                var PListProd = new ContextfreeProduction();
                PListProd.Left = PList;
                var candidate = new ProductionNodeList();
                candidate.Add(Vn); candidate.Add(colon_Colon_Equality_); candidate.Add(VList); candidate.Add(semicolon_); candidate.Add(PList);
                PListProd.RightCollection.Add(candidate);
                candidate = new ProductionNodeList();
                candidate.Add(ProductionNode.tail_null);
                PListProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(PListProd);
            }

            {
                var VListProd = new ContextfreeProduction();
                VListProd.Left = VList;
                var candidate = new ProductionNodeList();
                candidate.Add(V); candidate.Add(VOpt);
                VListProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(VListProd);
            }

            {
                var VProd = new ContextfreeProduction();
                VProd.Left = V;
                var candidate = new ProductionNodeList();
                candidate.Add(Vn);
                VProd.RightCollection.Add(candidate);
                candidate = new ProductionNodeList();
                candidate.Add(Vt);
                VProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(VProd);
            }

            {
                var VOptProd = new ContextfreeProduction();
                VOptProd.Left = VOpt;
                var candidate = new ProductionNodeList();
                candidate.Add(V); candidate.Add(VOpt);
                VOptProd.RightCollection.Add(candidate);
                candidate = new ProductionNodeList();
                candidate.Add(or_); candidate.Add(V); candidate.Add(VOpt);
                VOptProd.RightCollection.Add(candidate);
                candidate = new ProductionNodeList();
                candidate.Add(ProductionNode.tail_null);
                VOptProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(VOptProd);
            }

            {
                var VnProd = new ContextfreeProduction();
                VnProd.Left = Vn;
                var candidate = new ProductionNodeList();
                candidate.Add(lessThan_); candidate.Add(ProductionNode.tail_identifier); candidate.Add(greaterThan_);
                VnProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(VnProd);
            }

            {
                var VtProd = new ContextfreeProduction();
                VtProd.Left = Vt;

                var candidate = new ProductionNodeList();
                candidate.Add(token_null);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(token_identifier);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(token_number);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(token_constString);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(ProductionNode.tail_identifier);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(ProductionNode.tail_number);
                VtProd.RightCollection.Add(candidate);

                candidate = new ProductionNodeList();
                candidate.Add(ProductionNode.tail_constString);
                VtProd.RightCollection.Add(candidate);

                grammar.ProductionCollection.Add(VtProd);
            }

            return grammar;
        }

        private void codeGenerator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                this.pgbProcess.Value = e.ProgressPercentage;
                var text = e.UserState.ToString();
                //this.lblInfo.Text = text;
                this.txtProcess.AppendText(Environment.NewLine);
                this.txtProcess.AppendText(text);
                this.txtProcess.ScrollToCaret();
            }
            catch (Exception)
            {

            }
        }

        private void codeGenerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString(), "出错了亲！不好意思呦~");
                pgbProcess.Value = pgbProcess.Minimum;
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("代码生成过程被取消了，亲！", "提示");
                pgbProcess.Value = pgbProcess.Minimum;
            }
            else
            {

                if (MessageBox.Show("代码生成完成，是否立即打开所在文件夹？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start("explorer", (e.Result as CodeGeneratorParam).folder);
                }
            }

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Config config = null;
            if (File.Exists(configFilename))
            {
                var xml = XElement.Load(configFilename);
                config = Config.Parse(xml);
            }
            else
            {
                config = new Config();
                config.Grammars.Add(new Grammar() { CompilerName = "Expression", Namespace = "bitzhuwei.ExpressionGrammar", GrammarName = "Expression", Content = strExpression });
                config.Grammars.Add(new Grammar() { CompilerName = "CG", Namespace = "bitzhuwei.CGGrammar", GrammarName = "Context-free Grammar", Content = strCGGrammar });
                config.Grammars.SelectedIndex = 0;
                config.Save(configFilename);
            }

            //this.cmbGrammarList.Tag = config;
            this.cmbGrammarList.Items.Clear();
            foreach (var item in config.Grammars)
            {
                this.cmbGrammarList.Items.Add(item);
            }

            if (0 <= config.Grammars.SelectedIndex
                && config.Grammars.SelectedIndex < this.cmbGrammarList.Items.Count)
            {
                var grammar = config.Grammars[config.Grammars.SelectedIndex];
                this.cmbGrammarList.SelectedIndex = config.Grammars.SelectedIndex;
                this.txtCompilerName.Text = grammar.CompilerName;
                this.txtNamespace.Text = grammar.Namespace;
                this.txtCodeFolder.Text = grammar.CodeFolder;
            }
            else
            {
                this.btnRemove.Enabled = false;
            }

            this.btnSave.Enabled = false;
            this.textChangedEvent = new EventHandler(this.content_TextChanged);
            AddcontentTextChangedEvent();
        }

        private void AddcontentTextChangedEvent()
        {
            this.txtCompilerName.TextChanged += this.textChangedEvent;
            this.txtNamespace.TextChanged += this.textChangedEvent;
            this.txtCodeFolder.TextChanged += this.textChangedEvent;
            this.txtSourceCode.TextChanged += this.textChangedEvent;
        }
        private void RemovecontentTextChangedEvent()
        {
            this.txtCompilerName.TextChanged -= this.textChangedEvent;
            this.txtNamespace.TextChanged -= this.textChangedEvent;
            this.txtCodeFolder.TextChanged -= this.textChangedEvent;
            this.txtSourceCode.TextChanged -= this.textChangedEvent;
        }

        ////Config m_config;
        //private void InitGrammarExamples(Config config)
        //{
        //    this.cmbGrammarList.Tag = config;
        //    this.cmbGrammarList.Items.Clear();
        //    foreach (var item in config.Grammars)
        //    {
        //        this.cmbGrammarList.Items.Add(item);
        //    }
        //}

        readonly string strExpression =
            "<Expression>  ::= <Multiply> <PlusOpt>;" + Environment.NewLine
            + "<PlusOpt>     ::= \"+\" <Multiply> | \"-\" <Multiply> | null;" + Environment.NewLine
            + "<Multiply>    ::= <Unit> <MultiplyOpt>;" + Environment.NewLine
            + "<MultiplyOpt> ::= \"*\" <Unit> | \"/\" <Unit> | null;" + Environment.NewLine
            + "<Unit>        ::= number | \"(\" <Expression> \")\";";

        readonly string strCGGrammar =
            "<Start>  ::= <Vn> \"::=\" <VList> \";\" <PList>;" + Environment.NewLine
            + "<PList>  ::= <Vn> \"::=\" <VList> \";\" <PList> | null;" + Environment.NewLine
            + "<VList>  ::= <V> <VOpt>;" + Environment.NewLine
            + "<V>      ::= <Vn> | <Vt>;" + Environment.NewLine
            + "<VOpt>   ::= <V> <VOpt> | \"|\" <V> <VOpt> | null;" + Environment.NewLine
            + "<Vn>     ::= \"<\" identifier \">\";" + Environment.NewLine
            + "<Vt>     ::= \"null\" | \"identifier\" | \"number\" | \"constString\" | identifier | number | constString;";

        const string configFilename = "LL1GrammarCompilerWinform.config";
        private EventHandler textChangedEvent;

        private void content_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;
            this.btnSave.Enabled = false;

            var info = string.Empty;
            if (!this.Ready4Generation(ref info))
            {
                this.lblInfo.Text = info;
                return;
            }

            var grammar = cmbGrammarList.SelectedItem as Grammar;
            if (grammar != null)
            {
                grammar.CodeFolder = this.txtCodeFolder.Text;
                grammar.CompilerName = this.txtCompilerName.Text;
                grammar.Namespace = this.txtNamespace.Text;
                grammar.Content = this.txtSourceCode.Text;
            }

            this.lblInfo.Text = "";
            this.btnOK.Enabled = true;
            this.btnSave.Enabled = true;
        }

        private bool Ready4Generation(ref string resultInfo)
        {
            if (!Directory.Exists(txtCodeFolder.Text))
            {
                resultInfo = string.Format("folder not exists : {0}", txtCodeFolder.Text);
                return false;
            }
            if (!IsValidNamespaceName(txtNamespace.Text))
            {
                resultInfo = string.Format("namespace not valid : {0}", txtNamespace.Text);
                return false;
            }
            if (!IsValidCompilerName(txtCompilerName.Text))
            {
                resultInfo = string.Format("compiler name not valid : {0}", txtCompilerName.Text);
                return false;
            }
            return true;
        }

        private static bool IsValidNamespaceName(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName)) return false;
            if (namespaceName.StartsWith(" ") || namespaceName.EndsWith(" ")) return false;
            bool isValid = true;
            var parts = namespaceName.Split('.');
            foreach (var space in parts)
            {
                if (!IsValidIdentifier(space))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool IsValidCompilerName(string compilerName)
        {
            return IsValidIdentifier(compilerName);
        }

        private static bool IsValidIdentifier(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.StartsWith(" ") || value.EndsWith(" ")) return false;
            var head = value[0];
            if ((!('a' <= head && head <= 'z')) && (!('A' <= head && head <= 'Z'))
                && (!(head != '_')))
                return false;
            var isValid = true;
            foreach (var item in value)
            {
                if ((!('a' <= item && item <= 'z')) && (!('A' <= item && item <= 'Z'))
                    && (!('0' <= item && item <= '9')))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private void cmbGrammarList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var grammar = cmbGrammarList.SelectedItem as Grammar;
            if (grammar != null)
            {
                this.RemovecontentTextChangedEvent();
                this.txtCodeFolder.Text = grammar.CodeFolder;
                this.txtCompilerName.Text = grammar.CompilerName;
                this.txtNamespace.Text = grammar.Namespace;
                //this.txtSourceCode.Text = grammar.Content;
                this.btnRemove.Enabled = true;
                this.btnSave.Enabled = true;
                this.AddcontentTextChangedEvent();
                var info = string.Empty;
                this.btnOK.Enabled = this.Ready4Generation(ref info);
                if (info != string.Empty)
                {
                    this.lblInfo.Text = info;
                }
            }
            else
            {
                this.btnRemove.Enabled = false;
            }
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            //var grammars = new GrammarList();
            //foreach (var item in this.cmbGrammarList.Items)
            //{
            //    grammars.Add(item as Grammar);
            //}
            var insertForm = new FormInsertNewGrammar(
                from item in this.cmbGrammarList.Items.Cast<Grammar>()
                select item
                );
            //var insertForm = new FormInsertNewGrammar(grammars);
            if (insertForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var grammar = insertForm.GetGrammar();
                this.cmbGrammarList.Items.Add(grammar);
                this.cmbGrammarList.SelectedIndex = this.cmbGrammarList.Items.Count - 1;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var selectedObj = this.cmbGrammarList.SelectedItem as Grammar;
            if (selectedObj != null)
            {
                var index = this.cmbGrammarList.SelectedIndex;
                this.cmbGrammarList.Items.Remove(selectedObj);
                if (this.cmbGrammarList.Items.Count > 0)
                {
                    if (index < this.cmbGrammarList.Items.Count)
                        this.cmbGrammarList.SelectedIndex = index;
                    else
                        this.cmbGrammarList.SelectedIndex = index - 1;
                }
            }
            //if (this.m_config.Grammars.SelectedIndex >= 0
            //    && this.m_config.Grammars.SelectedIndex < this.m_config.Grammars.Count)
            //{
            //    this.cmbGrammarList.Items.Remove(this.m_config.Grammars[this.m_config.Grammars.SelectedIndex]);
            //    this.m_config.Grammars.Remove(this.m_config.Grammars[this.m_config.Grammars.SelectedIndex]);
            //    this.m_config.Grammars.SelectedIndex--;
            //    this.cmbGrammarList.SelectedIndex--;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var config = new Config();
            foreach (var item in this.cmbGrammarList.Items)
            {
                config.Grammars.Add(item as Grammar);
            }
            config.Grammars.SelectedIndex = this.cmbGrammarList.SelectedIndex;
            config.Save(configFilename);
            this.btnSave.Enabled = false;
        }

        private static List<ProductionNode> GetNotImplementedNodeList(string sourceCode)
        {
            var lexAna = new bitzhuwei.CGCompiler.LexicalAnalyzerCG();
            lexAna.SetSourceCode(sourceCode);
            var tokens = lexAna.Analyze();
            var syntaxParser = new bitzhuwei.CGCompiler.LL1SyntaxParserCG();
            syntaxParser.SetTokenListSource(tokens);
            var tree = syntaxParser.Parse();
            ContextfreeGrammar grammar = tree.GetGrammar();
            grammar.GrammarName = "TmpGrammarName";
            grammar.Namespace = "TmpNamespace";

            List<ProductionNode> testedNodeList = new List<ProductionNode>();
            List<ProductionNode> notImplementedNodeList = new List<ProductionNode>();

            foreach (var production in grammar.ProductionCollection)
            {
                foreach (var candidate in production.RightCollection)
                {
                    foreach (var node in candidate)
                    {
                        if (node.Position != EnumProductionNodePosition.NonLeave) { continue; }
                        if (testedNodeList.Contains(node)) { continue; }

                        bool implemented = false;
                        foreach (var p in grammar.ProductionCollection)
                        {
                            if (p.Left == node)
                            {
                                testedNodeList.Add(node);
                                implemented = true;
                                break;
                            }
                        }

                        if ((!implemented) && (!notImplementedNodeList.Contains(node)))
                        {
                            notImplementedNodeList.Add(node);
                        }
                    }
                }
            }
            return notImplementedNodeList;
        }

    }

}
