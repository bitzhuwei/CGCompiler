# CGCompiler
Context-free Grammar Compiler is a compiler-compiler in C# that generates lexical analyzer and syntax parser automatically according to the grammar you defined.
<p>前一阵做了个编译器（仅词法分析、语法分析、部分语义分析，所以说是前端），拿来分享一下，如有错误，欢迎批评指教！</p>
<p>整个代码库具有如下功能：</p>
<p>提供编译器所需基础数据结构、计算流程框架类，可供继承使用； <br />提供基础数据结构的可视化控件； <br />提供类似YACC的词法分析器、语法分析器自动生成功能； <br />提供Winform程序，集成和扩展上述功能，方便研究和应用。</p>
<p>本文及其后续系列将逐步给出所有工程源代码（visual studio 2010版本）。</p>
<p>上图展示一下先。</p>
<p><a title="图1 词法、语法分析和结点匹配" href="http://www.cnblogs.com/bitzhuwei/archive/2012/10/19/SmileWei_Compiler.html" rel="tag"><img style="display: inline; border: 0px;" title="图1 词法、语法分析和结点匹配" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201210/201210222212155221.png" alt="图1 词法、语法分析和结点匹配" width="644" height="319" border="0" /></a> </p>
<p>图1 词法、语法分析和结点匹配</p>
<p><a title="图2 自动生成词法分析器、语法分析器" href="http://www.cnblogs.com/bitzhuwei/archive/2012/10/19/SmileWei_Compiler.html" rel="tag"><img style="display: inline; border: 0px;" title="图2 自动生成词法分析器、语法分析器" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201210/201210222212162929.png" alt="图2 自动生成词法分析器、语法分析器" width="644" height="320" border="0" /></a> </p>
<p>图2 自动生成词法分析器、语法分析器</p>
<p><a title="图3 自动生成词法分析器、语法分析器" href="http://www.cnblogs.com/bitzhuwei/archive/2012/10/19/SmileWei_Compiler.html" rel="tag"><img style="display: inline; border: 0px;" title="图3 自动生成词法分析器、语法分析器" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201210/201210222212182416.png" alt="图3 自动生成词法分析器、语法分析器" width="644" height="320" border="0" /></a> </p>
<p>图3 自动生成词法分析器、语法分析器</p>
<p><a title="一个编译器（前端）的实现" href="http://www.cnblogs.com/bitzhuwei/archive/2012/10/19/SmileWei_Compiler.html" rel="tag"><img style="display: inline; border: 0px;" title="图4 自动打印语法树" src="http://images.cnblogs.com/cnblogs_com/bitzhuwei/201210/201210222205583581.png" alt="图4 自动打印语法树" width="430" height="484" border="0" /></a> </p>
<p>图4 自动打印语法树</p>
<p>为了说清楚编译器这种东西，我想最好还是举例。</p>
<p>比如我们要为数学计算的表达式（Expression）设计一个编译器。（当然有很多方法可以实现读取数学表达式并计算结果的算法，未必使用编译原理）</p>
<p>来看一些数学表达式的例子：</p>
<p>37</p>
<p>19 * 19 - 18 * 18</p>
<p>(19 + 18) * (19 - 18)</p>
<p>18 +19 / (18 / 18)</p>
<p>a&nbsp; + (a + 1) + (a + 2) + (a + 3)</p>
<p>好了够了，大家能够了解本文所讨论的Expression的范围了。那么我们引入&ldquo;文法&rdquo;（Grammar）的概念。Expression的文法就是这样的：</p>
<p>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;; <br />&lt;PlusOpt&gt; ::= "+" &lt;Multiply&gt; | "-" &lt;Multiply&gt; | null; <br />&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;; <br />&lt;MultiplyOpt&gt; ::= "*" &lt;Unit&gt; | "/" &lt;Unit&gt; | null; <br />&lt;Unit&gt; ::= identifier | "(" &lt;Expression&gt; ")" | number;</p>
<p>我们分别展示出上述几个例子用文法展开的过程。</p>
<p>37: &lt;Expression&gt; </p>
<p>=&gt; &lt;Multiply&gt; &lt;PlusOpt&gt; </p>
<p>=&gt; &lt;Unit&gt; &lt;MultiplyOpt&gt;</p>
<p>=&gt; number</p>
<p>19 * 19 - 18 * 18: &lt;Expression&gt; </p>
<p>=&gt; &lt;Multiply&gt; &lt;PlusOpt&gt; </p>
<p>=&gt; &lt;Unit&gt; &lt;MultiplyOPt&gt; "-" &lt;Multiply&gt; </p>
<p>=&gt; number "*" &lt;Unit&gt; "-" &lt;Unit&gt; &lt;MultiplyOpt&gt; </p>
<p>=&gt; number "*" number "-" number "*" &lt;Unit&gt;</p>
<p>=&gt; number "*" number "-" number "*" number</p>
<p>(19 + 18) * (19 - 18): &lt;Expression&gt;</p>
<p>=&gt; &lt;Multiply&gt; &lt;PlusOpt&gt;</p>
<p>=&gt; &lt;Unit&gt; &lt;MultiplyOpt&gt;</p>
<p>=&gt; "(" &lt;Expression&gt; ")" "*" &lt;Unit&gt;</p>
<p>=&gt; "(" &lt;Multiply&gt; &lt;PlusOpt&gt; ")" "*" "(" &lt;Expression&gt; ")"</p>
<p>=&gt; "(" &lt;Unit&gt; &lt;MultiplyOpt&gt; "+" &lt;Multiply&gt; ")" "*" "(" &lt;Multiply&gt; &lt;PlusOpt&gt; ")"</p>
<p>=&gt; "(" number "+" &lt;Unit&gt; &lt;MultiplyOpt&gt; ")" "*" "(" &lt;Unit&gt; &lt;MultiplyOpt&gt; "-" &lt;Multiply&gt; ")"</p>
<p>=&gt; "(" number "+" number ")" "*" "(" number "-" number &lt;MultiplyOpt&gt; ")"</p>
<p>=&gt; "(" number "+" number ")" "*" "(" number "-" number ")"</p>
<p>写到这里就，其余例子大家自己试试~如果写不出来，后面的部分可能就不太容易看了。（试试写写，很快就写的比较熟练了） </p>
<p>&nbsp;</p>
<p>总结一下&ldquo;文法&rdquo;（Grammar）。文法就是描述Expression的构成的，和英语的语法类似吧。 有了文法，我们就可以写编译器了。</p>
<p>Expression的文法有5个式子，这5个式子就叫做&ldquo;产生式&rdquo;（Production），因为他们能从左边的结构产生（推导）出右边的结构来。一个文法至少有一个产生式，第一个产生式的左边的结点是初始结点，所有的推导都必须从初始结点（即第一个产生式）开始。</p>
<p>产生式（Production）左边叫做左部（左部只有始终一个结点），右边叫做右部（废话），中间用【::=】这个符号隔开。</p>
<p>右部由符号【|】分为若干部分，每一部分都是产生式可能推导出的一个结果，且每次只能选择其中一个进行推导。【null】表示什么也不推导出来。（这是个霸气的符号，不要觉得什么都不推导出来就不重要，恰恰相反，这个符号很重要）</p>
<p>为简化后文的说明，继续举例：&lt;PlusOpt&gt; ::= "+" &lt;Multiply&gt; | "-" &lt;Multiply&gt; | null;</p>
<p>对于这个产生式，其实是由三部分&lt;PlusOpt&gt; ::= "+" &lt;Multiply&gt;;和&lt;PlusOpt&gt; ::= "-" &lt;Multiply&gt;和&lt;PlusOpt&gt; ::= null;组成的，每一部分都称为一个&ldquo;推导式&rdquo;（Derivation）。</p>
<p><span style="text-decoration: line-through;">像【(19 + 18) * (19 - 18)】这样一个具体的&ldquo;东西&rdquo;，我们称之为一个&ldquo;句子&rdquo;（Sentence）。</span></p>
<p>明了了上述关于文法的东西，就可以进行编译器的设计了。 </p>
<p>&nbsp;</p>
<p>我们先搞搞清楚，编译器能做什么？以Expression的【19 * 19 - 18 * 18】为例，Expression的编译器首先要读取字符串格式的源代码，即：</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> <span style="color: #0000ff;">var</span> sentence = &ldquo;<span style="color: #800080;">19</span> * <span style="color: #800080;">19</span> - <span style="color: #800080;">18</span> * <span style="color: #800080;">18</span><span style="color: #000000;">&rdquo;;
</span><span style="color: #008080;">2</span> <span style="color: #0000ff;">var</span> expLexicalAnalyzer = <span style="color: #0000ff;">new</span><span style="color: #000000;"> LexicalAnalyzerExpression();
</span><span style="color: #008080;">3</span> expLexicalAnalyzer.SetSourceCode(sentence);</pre>
</div>
<p>&nbsp;</p>
<p>然后，编译器进行词法分析，得到单词流（TokenList）。&ldquo;流&rdquo;这个东西，其实就是数组。</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> <span style="color: #0000ff;">var</span> tokens = expLexicalAnalyzer.Analyze();</pre>
</div>
<p>在此例中，得到的单词流是这样的：</p>
<p>[19]$[Number]$[0,0]$[False]$[] <br />[*]$[Multiply]$[0,3]$[False]$[] <br />[19]$[Number]$[0,5]$[False]$[] <br />[-]$[Minus_]$[0,8]$[False]$[] <br />[18]$[Number]$[0,10]$[False]$[] <br />[*]$[Multiply_]$[0,13]$[False]$[] <br />[18]$[Number]$[0,15]$[False]$[]</p>
<p>第一个单词的意思是：这个单词是【19】，类别是【Number】，在源代码中第一个字符的位置是【行0, 列0】，是否错误的单词【False】，其它描述信息为【】（空，即木有描述信息））</p>
<p>然后是根据这个单词流分析出语法树：</p>
<div class="cnblogs_code">
<pre><span style="color: #008080;">1</span> <span style="color: #0000ff;">var</span> expSyntaxParser = <span style="color: #0000ff;">new</span><span style="color: #000000;"> SyntaxParserExpression();
</span><span style="color: #008080;">2</span> <span style="color: #000000;">expSyntaxParser.SetTokenList(tokens);
</span><span style="color: #008080;">3</span> <span style="color: #0000ff;">var</span> syntaxTree = expSyntaxParser.Parse();</pre>
</div>
<p>得到的语法树是一个树的结构，可以表示如下：</p>
<p><span style="font-family: consolas;">&lt;Expression&gt; <br />&nbsp; ├─&lt;Multiply&gt; <br />&nbsp; │&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp; │&nbsp; └─number(19) <br />&nbsp; │&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; ├─* <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Unit&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─number(19) <br />&nbsp; └─&lt;PlusOpt&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp; ├─-&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Multiply&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─&lt;Unit&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; └─number(18)&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;MultiplyOpt&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─*&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Unit&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─number(18)</span></p>
<p>从此树中可以看到，树的结构和上文的文法展开过程是对应的，并且树的叶结点从上到下组成了我们的例子【19 * 19 - 18 * 18】</p>
<p>然后就是语义分析了。到目前为止（据我所学到的），人类还没有完善的自动生成语义分析代码的能力。我们在此处就把&rdquo;计算结果&ldquo;作为语义分析的任务。仍以上例进行说明。各个叶结点的含义我们是知道的，【+】【-】【*】【/】代表运算，【number】代表数值，【identifier】代表变量名。那么在没有【identifier】的时候，数和数就直接算出结果来，有【identifier】就保留着不动。我们分别为Expression文法的各类结点都赋予语义：</p>
<p>&lt;Expression&gt;：将它的两个子结点进行运算或保留。</p>
<p>&lt;Multiply&gt;：将它的两个子结点进行运算或保留。</p>
<p>&lt;PlusOpt&gt;：去掉自己，用自己的子结点代替自己的位置。</p>
<p>&lt;Unit&gt;：去掉自己，用自己的子结点代替自己的位置。</p>
<p>&lt;MultiplyOpt&gt;：去掉自己，用自己的子结点代替自己的位置。</p>
<p>&ldquo;+&rdquo;：对自己的左右结点进行加法运算。</p>
<p>&ldquo;-&rdquo;：对自己的左右结点进行减法运算。</p>
<p>&ldquo;*&rdquo;：对自己的左右结点进行乘法运算。</p>
<p>&ldquo;/&rdquo;：对自己的左右结点进行除法运算。</p>
<p>identifier：保持不变。</p>
<p>number：保持不变。</p>
<p>&ldquo;(&ldquo;：若自己右部的&lt;Expression&gt;成为数字或单一的【identifier】，则去掉自己，去掉&lt;Expression&gt;右部的&rdquo;)&rdquo;；否则不变。</p>
<p>&ldquo;)&rdquo;：保持不变。</p>
<p>上例经过语义分析（对语法树自顶向下进行递归分析其语义），就得到一个数值&rdquo;37&ldquo;。</p>
<p>语义分析的伪代码如下：</p>
<div class="cnblogs_code" onclick="cnblogs_code_show('702d1cfb-6c9c-4e68-a27d-daf6af97422b')"><img id="code_img_closed_702d1cfb-6c9c-4e68-a27d-daf6af97422b" class="code_img_closed" src="http://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif" alt="" /><img id="code_img_opened_702d1cfb-6c9c-4e68-a27d-daf6af97422b" class="code_img_opened" style="display: none;" onclick="cnblogs_code_hide('702d1cfb-6c9c-4e68-a27d-daf6af97422b',event)" src="http://images.cnblogs.com/OutliningIndicators/ExpandedBlockStart.gif" alt="" /><span class="cnblogs_code_collapse">语义分析伪代码</span>
<div id="cnblogs_code_open_702d1cfb-6c9c-4e68-a27d-daf6af97422b" class="cnblogs_code_hide">
<pre><span style="color: #008080;"> 1</span> <span style="color: #000000;">SyntaxTreeExpression SemanticAnalyze(SyntaxTree root)
</span><span style="color: #008080;"> 2</span> 
<span style="color: #008080;"> 3</span> <span style="color: #000000;">{
</span><span style="color: #008080;"> 4</span> 
<span style="color: #008080;"> 5</span>     <span style="color: #0000ff;">switch</span><span style="color: #000000;">(root.NodeType)
</span><span style="color: #008080;"> 6</span> 
<span style="color: #008080;"> 7</span> <span style="color: #000000;">    {
</span><span style="color: #008080;"> 8</span> 
<span style="color: #008080;"> 9</span>     <span style="color: #0000ff;">case</span><span style="color: #000000;"> EnumTreeNodeType.Expression:
</span><span style="color: #008080;">10</span> 
<span style="color: #008080;">11</span>           <span style="color: #0000ff;">return</span> Cacul(SemanticAnalyze(root.Children[<span style="color: #800080;">0</span>]),SemanticAnalyze(root.Children[<span style="color: #800080;">1</span><span style="color: #000000;">]));
</span><span style="color: #008080;">12</span> 
<span style="color: #008080;">13</span>           <span style="color: #0000ff;">break</span><span style="color: #000000;">;
</span><span style="color: #008080;">14</span> 
<span style="color: #008080;">15</span>     <span style="color: #0000ff;">case</span><span style="color: #000000;"> EnumTreeNodeType.Multiply:
</span><span style="color: #008080;">16</span> 
<span style="color: #008080;">17</span>           <span style="color: #0000ff;">return</span> Cacul(SemanticAnalyze(root.Children[<span style="color: #800080;">0</span>]),SemanticAnalyze(root.Children[<span style="color: #800080;">1</span><span style="color: #000000;">]));
</span><span style="color: #008080;">18</span> 
<span style="color: #008080;">19</span>           <span style="color: #0000ff;">break</span><span style="color: #000000;">;
</span><span style="color: #008080;">20</span> 
<span style="color: #008080;">21</span>     <span style="color: #0000ff;">case</span><span style="color: #000000;"> EnumTreeNodeType.PlusOpt:
</span><span style="color: #008080;">22</span> 
<span style="color: #008080;">23</span>           <span style="color: #0000ff;">var</span> child = SemanticAnalyze(root.Children[<span style="color: #800080;">0</span><span style="color: #000000;">]);
</span><span style="color: #008080;">24</span> 
<span style="color: #008080;">25</span>           <span style="color: #0000ff;">var</span> child2 = SemanticAnalyze(root.Children[<span style="color: #800080;">1</span><span style="color: #000000;">]);
</span><span style="color: #008080;">26</span> 
<span style="color: #008080;">27</span>           root.parent.Children[<span style="color: #800080;">1</span>] = child; root.parent.Children[<span style="color: #800080;">2</span>] =<span style="color: #000000;"> child2;
</span><span style="color: #008080;">28</span> 
<span style="color: #008080;">29</span>           <span style="color: #0000ff;">break</span><span style="color: #000000;">;
</span><span style="color: #008080;">30</span> 
<span style="color: #008080;">31</span>     <span style="color: #0000ff;">case</span><span style="color: #000000;"> EnumTreeNodeType.Unit:
</span><span style="color: #008080;">32</span> 
<span style="color: #008080;">33</span>           root.parent.Children[<span style="color: #800080;">0</span>] = root.Children[<span style="color: #800080;">0</span><span style="color: #000000;">];
</span><span style="color: #008080;">34</span> 
<span style="color: #008080;">35</span>           <span style="color: #0000ff;">break</span><span style="color: #000000;">;
</span><span style="color: #008080;">36</span> 
<span style="color: #008080;">37</span>     <span style="color: #008000;">//</span><span style="color: #008000;">&hellip;</span>
<span style="color: #008080;">38</span> 
<span style="color: #008080;">39</span>     <span style="color: #0000ff;">case</span> EnumTreeNodeType.Plus:<span style="color: #008000;">//</span><span style="color: #008000;"> &ldquo;+&rdquo;</span>
<span style="color: #008080;">40</span> 
<span style="color: #008080;">41</span>           <span style="color: #0000ff;">return</span> Calcu(SemanticAnalyze(root.parent.Children[<span style="color: #800080;">0</span>]), SemanticAnalyze(root.parent.Children[<span style="color: #800080;">2</span><span style="color: #000000;">]));
</span><span style="color: #008080;">42</span> 
<span style="color: #008080;">43</span>           <span style="color: #0000ff;">break</span><span style="color: #000000;">;
</span><span style="color: #008080;">44</span> 
<span style="color: #008080;">45</span>     <span style="color: #008000;">//</span><span style="color: #008000;">&hellip;</span>
<span style="color: #008080;">46</span> 
<span style="color: #008080;">47</span> }</pre>
</div>
</div>
<p>语义分析完成，我们这个编译器前端也就大功告成了。</p>
<p>所以这个编译器要实现的东西大体感觉就是这样的。虽然单单对Expression进行编译分析是没多大意思的，但是这个例子在足够简单的同时，又足够典型，等我们把这个例子实现了，再复杂的编译器也都能做出来了。编译器制作步骤比较多，工作量也大，如果一上来就抱着完整的C语言文法来做，等于把自己埋在深不见底的BUG海洋中活活淹死。</p>
<p>以后实现了编译器的语法分析后，就可以自动生成示例中的语法树了，其实这也算是一种语义分析。</p>
<p>后面系列文章将给出具体的设计和实现过程，以及完整的工程代码。敬请关注！</p>
<p>关于本系列有什么好的建议，也请提出来，O(&cap;_&cap;)O谢谢！</p>
<p>PS：下面给出【(19 + 18) * (19 - 18)】的语法树，供大家学习参考，也方便后续文章讲解。</p>
<p><span style="font-family: consolas;">&lt;Expression&gt; <br />&nbsp; ├─&lt;Multiply&gt; <br />&nbsp; │&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp; │&nbsp; ├─( <br />&nbsp; │&nbsp; │&nbsp; ├─&lt;Expression&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; ├─&lt;Multiply&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; │&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; │&nbsp; │&nbsp; └─number(19) <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; │&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─null <br />&nbsp; │&nbsp; │&nbsp; │&nbsp; └─&lt;PlusOpt&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; ├─+&nbsp; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Multiply&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; └─number(18) <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─null <br />&nbsp; │&nbsp; │&nbsp; └─) <br />&nbsp; │&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; ├─* <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Unit&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─( <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─&lt;Expression&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; ├─&lt;Multiply&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; │&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; │&nbsp; │&nbsp; └─number(19) <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; │&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─null <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; └─&lt;PlusOpt&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; ├─- <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;Multiply&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ├─&lt;Unit&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp; └─number(18) <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─&lt;MultiplyOpt&gt; <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─null <br />&nbsp; │&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; └─) <br />&nbsp; └─&lt;PlusOpt&gt;&nbsp; <br />&nbsp;&nbsp;&nbsp;&nbsp; └─null</span></p>
<p><span style="font-family: consolas;">&nbsp;</span></p>
<p>关于编译原理基础概念可参考<a href="http://www.cnblogs.com/bitzhuwei/archive/2012/10/22/SmileWei_Compiler.html">http://www.cnblogs.com/bitzhuwei/archive/2012/10/22/SmileWei_Compiler.html</a>&nbsp;</p>
<p>关于下列代码的基础数据结构参见<a href="http://www.cnblogs.com/bitzhuwei/archive/2012/03/09/compiler_basic_data_structure.html">http://www.cnblogs.com/bitzhuwei/archive/2012/03/09/compiler_basic_data_structure.html</a></p>
<h2>一、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 消除直接左递归</h2>
<p>设P -&gt; P&alpha;1 | P&alpha;2 | ... | P&alpha;n | &beta;1 | &beta;2 | ... |&beta;m</p>
<p>其中每个&alpha;不为&epsilon;（&epsilon;就是空，什么都没有的意思，类似null），每个&beta;不以P开头。</p>
<p>则非终结符P可改写为</p>
<p>P -&gt; &beta;1P&rsquo; | &beta;2P&rsquo; | ... | &beta;mP&rsquo;</p>
<p>P&rsquo; -&gt; &alpha;1P&rsquo; | &alpha;2P&rsquo; | ... | &alpha;nP&rsquo;</p>
<p>解释：原来的P展开就是&beta;x&alpha;i..&alpha;i&alpha;j..&alpha;j...&alpha;t..&alpha;t的形式，即某个&beta;开头，各种阿尔法跟随的一个串。所以与改写形式所表达的东西是一样的。</p>
<p>&nbsp;</p>
<h2>二、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 消除间接左递归</h2>
<p>给定文法G，若G不含回路（P经过若干步推导又得到P）且不含以&epsilon;为右部的产生式。</p>
<p>则其消除左递归的算法如下：</p>
<ol>
<li>对G的非终结符按任意顺序排列，如A1, A2, A3, ... , An</li>
<li>for (i = 1; i &lt;= n; i++)<br />&nbsp;&nbsp;&nbsp; for (j = 1; j &lt;= i - 1; j++)<br />&nbsp;&nbsp;&nbsp; {<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 把形如Ai -&gt; Aj&gamma;的产生式改写成Ai -&gt; &delta;1&gamma; | &delta;2&gamma; | ... | &delta;k&gamma;的形式，其中Aj -&gt; &delta;1 | &delta;2 | ... | &delta;k是关于Aj的全部规则<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 消除Ai规则中的直接左递归<br />&nbsp;&nbsp;&nbsp; }</li>
<li>简化由上一步得到的文法，即去掉多余的规则</li>

</ol>
<p>&nbsp;</p>
<h2>三、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; FIRST集</h2>
<p>若文法G为二型文法且不含左递归，则G的非终结符的每个候选式&alpha;的终结首符集FIRST（&alpha;）为FIRST（&alpha;） = { a | &alpha;经过0或多步推导为a...的形式，其中a&isin;V<sub>T</sub> }</p>
<p>解读：FIRST集的含义是：候选式经过推导，最后就是一个终结符的串，推导过程不同，会有多个不同的串（可能是无限个），这些串里的第一个字符组成的集合就是这个候选式的FIRST集。有了这个FIRST集，就可以知道这个候选式是否能匹配接下来要解析的单词流了。</p>
<p>&nbsp;</p>
<h2>四、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; FOLLOW集</h2>
<p>设上下文无关文法（二型文法）G，开始符号为S，对于G中的任意非终结符A，其FOLLOW（A） = { a | S 经过0或多步推导会出现 ...Aa...的形式，其中a&isin;V<sub>T</sub>或#号 }</p>
<p>解读：FOLLOW集的含义是：G的一切句型中，能够紧跟着非终结符A之后的一切终结符或井号#。#是当出现 ...A 这样的情况，即A为最后一个字符。</p>
<p>&nbsp;</p>
<h2>五、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 构造FOLLOW集的算法</h2>
<ol>
<li>令#&isin;FOLLOW（S）</li>
<li>若文法G中有形如A &ndash;&gt; &alpha;B&beta;的规则，且&beta;&ne;&epsilon;，则将FIRST（&beta;）中的一切非终结符加入FOLLOW（B）</li>
<li>若文法G中有形如A -&gt; &alpha;B或A -&gt; &alpha;B&beta;的规则，且&epsilon;&isin;FIRST（&beta;），则将FOLLOW（A）中的全部元素加入FOLLOW（B）</li>
<li>反复使用前两条规则，直到所有的FOLLOW集都没有改变。</li>

</ol>
<p>&nbsp;</p>
<h2>六、&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 构造LL（1）分析表的算法</h2>
<p>输入：文法G</p>
<p>输出：G的LL（1）分析表M（Ax, ay），其中A为非终结符，a为终结符</p>
<p>算法：</p>
<ol>
<li>求出G的FIRST集和FOLLOW集</li>
<li>for (G的每个产生式 A -&gt; &gamma;1 | &gamma;2 | ... | &gamma;m)<br />{<br />&nbsp;&nbsp;&nbsp; if (a &isin; FIRST（&gamma;i）) 置 M（A, a） 为 &ldquo;A -&gt; &gamma;i&rdquo;<br />&nbsp;&nbsp;&nbsp; if (&epsilon; &isin; FIRST（&gamma;i）)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for (每个 a &isin; FOLLOW（A）)<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 置 M（A, a）为 &ldquo;A -&gt; &gamma;i&rdquo;（实际上此处的&gamma;i都是&epsilon;）<br />}<br />置所有无定义的 M（A, a）为出错。</li>

</ol>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>一个编译器的实现3&mdash;&mdash;用编译原理自动化制作文本解析器</p>
<p>PS：<a title="一个编译器的实现3&mdash;&mdash;用编译原理自动化制作文本解析器2013-11-20_19-52-00" href="http://files.cnblogs.com/bitzhuwei/%E4%B8%80%E4%B8%AA%E7%BC%96%E8%AF%91%E5%99%A8%E7%9A%84%E5%AE%9E%E7%8E%B03%E2%80%94%E2%80%94%E7%94%A8%E7%BC%96%E8%AF%91%E5%8E%9F%E7%90%86%E8%87%AA%E5%8A%A8%E5%8C%96%E5%88%B6%E4%BD%9C%E6%96%87%E6%9C%AC%E8%A7%A3%E6%9E%90%E5%99%A82013-11-20_19-52-00.pdf" target="_blank">本文PDF版在这里</a>。</p>
<p>&nbsp;</p>
<p>关于编译器的概念、工作流程、算法和设计方案，可参考这里（<a href="http://www.cnblogs.com/bitzhuwei/archive/2013/06/05/CompilerDesignAndImp4Context-freeGrammar.html">http://www.cnblogs.com/bitzhuwei/archive/2013/06/05/CompilerDesignAndImp4Context-freeGrammar.html</a>）。阅读本文须理解&ldquo;上下文无关文法（Context-free Grammar）&rdquo;是什么。</p>
<p>本文以加减乘除表达式和一个3D坦克游戏模型为例，说明如何自动生成解析器以及如何使用自动生成的代码。</p>
<p>文末附源代码。</p>
<h3>加减乘除表达式</h3>
<p>运行编译器代码生成器（bitzhuwei.CGCompiler.Winform.exe），默认配置文件中已经有加减乘除表达式（Expression）的文法了。</p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194255-0c2d079f4fa6417389472f512586eecf.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image002[8]" src="http://images.cnitblog.com/blog/383191/201311/20194256-b99eb5dee5c446b3b983dd4dd2355262.jpg" alt="clip_image002[8]" width="557" height="365" border="0" /></a></p>
<p>设置好编译器名字、命名空间和代码存放的位置，点击&ldquo;开始！&rdquo;。</p>
<p>若文法没有错误，会在指定位置生成Expression解析器的代码。</p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194257-fdc4c8d1e9544174a62b33e248c372b9.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image004[8]" src="http://images.cnitblog.com/blog/383191/201311/20194258-a4d23d599a034146931e29c9b292e610.jpg" alt="clip_image004[8]" width="557" height="364" border="0" /></a></p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194259-d7ff6f60ffbf48f892d634bf982ced55.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image006[8]" src="http://images.cnitblog.com/blog/383191/201311/20194300-166d722947ff46e8ba005d9a7cca5ea7.jpg" alt="clip_image006[8]" width="558" height="237" border="0" /></a></p>
<p>一共生成了10个文件（其中bitzhuwei.CompilerBase.dll和使用说明.txt是直接复制的）。</p>
<p>三个Enum*.cs文件分别是文法的字符类型、单词类型和语法树结点类型。</p>
<p>LexicalAnalyzer*.cs文件是词法分析器。</p>
<p>LL1SyntaxParser*.cs文件是语法分析器。</p>
<p>SyntaxTreeNodeValue*.cs文件是语法树结点类型，稍候会用到。</p>
<p>使用生存的代码的方法很简单：创建一个类库项目，把生成的10个文件全部加进去，引用bitzhuwei.CompilerBase.dll文件。</p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194301-13d427fb836f424e9cfff92db92659dd.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image007[8]" src="http://images.cnitblog.com/blog/383191/201311/20194302-a3a053d627a646bbbe605c2e4476876e.jpg" alt="clip_image007[8]" width="282" height="477" border="0" /></a></p>
<p>为了测试，再创建一个Console项目，用下面的代码测试。</p>
<div class="cnblogs_code"><img id="Code_Closed_Image_338150" onclick="this.style.display='none'; document.getElementById('Code_Closed_Text_338150').style.display='none'; document.getElementById('Code_Open_Image_338150').style.display='inline'; document.getElementById('Code_Open_Text_338150').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ContractedBlock.gif" alt="" width="11" height="16" align="top" /><img id="Code_Open_Image_338150" style="display: none;" onclick="this.style.display='none'; document.getElementById('Code_Open_Text_338150').style.display='none'; getElementById('Code_Closed_Image_338150').style.display='inline'; getElementById('Code_Closed_Text_338150').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ExpandedBlockStart.gif" alt="" width="11" height="16" align="top" />
<pre><span id="Code_Closed_Text_338150" class="cnblogs_code_Collapse">测试Expression的代码</span><span id="Code_Open_Text_338150" style="display: none;">        <span style="color: #0000ff;">static</span> <span style="color: #0000ff;">void</span> Main(string[] args)
        {
            var sourceCodes = new string[]
            {
                "<span style="color: #8b0000;">37</span>",
                "<span style="color: #8b0000;">19 * 19 - 18 * 18</span>",
                "<span style="color: #8b0000;">(19 + 18) * (19 - 18)</span>",
            };
            foreach (var sourceCode in sourceCodes)
            {
                var lex = new bitzhuwei.ExpressionCompiler.LexicalAnalyzerExpression();
                lex.SetSourceCode(sourceCode);
                var tokens = lex.Analyze();
                Console.WriteLine(tokens);
                var parser = new bitzhuwei.ExpressionCompiler.LL1SyntaxParserExpression();
                parser.SetTokenListSource(tokens);
                var tree = parser.Parse();
                Console.WriteLine(tree);
                var value = tree.GetValue();
                Console.WriteLine(value);
            }
        }
</span></pre>
</div>
<p>&nbsp;</p>
<p>输入的语法树如下图所示。</p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194304-e0c51944cdaf40d08a9d7c1175a60f3a.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image009[8]" src="http://images.cnitblog.com/blog/383191/201311/20194306-99c7f6f3f97e40378bbd5fbd657a95b8.jpg" alt="clip_image009[8]" width="558" height="542" border="0" /></a></p>
<p>我们使用解析器，目的是为了得到数据结构后再获取有价值的结果。Expression的价值在于获取表达式的值，通过遍历语法树获取这个值是很容易的。（这个代码只能自己写，这属于语义分析阶段了，目前还无法自动生成。）</p>
<div class="cnblogs_code"><img id="Code_Closed_Image_570623" onclick="this.style.display='none'; document.getElementById('Code_Closed_Text_570623').style.display='none'; document.getElementById('Code_Open_Image_570623').style.display='inline'; document.getElementById('Code_Open_Text_570623').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ContractedBlock.gif" alt="" width="11" height="16" align="top" /><img id="Code_Open_Image_570623" style="display: none;" onclick="this.style.display='none'; document.getElementById('Code_Open_Text_570623').style.display='none'; getElementById('Code_Closed_Image_570623').style.display='inline'; getElementById('Code_Closed_Text_570623').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ExpandedBlockStart.gif" alt="" width="11" height="16" align="top" />
<pre><span id="Code_Closed_Text_570623" class="cnblogs_code_Collapse">SyntaxTreeExpressionGetValue.cs</span><span id="Code_Open_Text_570623" style="display: none;"><span style="color: #808080;">/// &lt;summary&gt;</span>
    <span style="color: #808080;">/// 提供SyntaxTree&amp;lt;EnumTokenTypeCG, EnumVTypeCG, SyntaxTreeNodeValueCG&amp;gt;的扩展方法</span>
    <span style="color: #808080;">/// &lt;/summary&gt;</span>
    <span style="color: #0000ff;">public</span> <span style="color: #0000ff;">static</span> partial <span style="color: #0000ff;">class</span> SyntaxTreeExpression
    {
        <span style="color: #808080;">/// &lt;summary&gt;</span>
        <span style="color: #808080;">/// 获取源代码的规范格式</span>
        <span style="color: #808080;">/// &lt;para&gt;语法分析的副产品&lt;/para&gt;</span>
        <span style="color: #808080;">/// &lt;/summary&gt;</span>
        <span style="color: #808080;">/// &lt;param name="tree"&gt;语法树&lt;/param&gt;</span>
        <span style="color: #808080;">/// &lt;returns&gt;&lt;/returns&gt;</span>
        <span style="color: #0000ff;">public</span> <span style="color: #0000ff;">static</span> <span style="color: #0000ff;">double</span> GetValue(<span style="color: #0000ff;">this</span> SyntaxTree&lt;EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression&gt; tree)
        {
            <span style="color: #0000ff;">if</span> (tree == <span style="color: #0000ff;">null</span>) <span style="color: #0000ff;">return</span> <span style="color: #0000ff;">double</span>.NaN;

            var tmpTree = tree.Clone() <span style="color: #0000ff;">as</span> SyntaxTree&lt;EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression&gt;;
            _GetValue(tmpTree);

            <span style="color: #0000ff;">return</span> <span style="color: #0000ff;">double</span>.Parse(tmpTree.Tag.ToString());
        }

        <span style="color: #0000ff;">private</span> <span style="color: #0000ff;">static</span> <span style="color: #0000ff;">void</span> _GetValue(SyntaxTree&lt;EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression&gt; tree)
        {
            <span style="color: #0000ff;">switch</span> (tree.NodeValue.NodeType)
            {
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.Unknown:
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.case_Expression:<span style="color: #008000;">//&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</span>
                    _GetValue(tree.Children[0]);
                    _GetValue(tree.Children[1]);
                    tree.Tag = <span style="color: #0000ff;">double</span>.Parse(tree.Children[0].Tag.ToString()) + <span style="color: #0000ff;">double</span>.Parse(tree.Children[1].Tag.ToString());
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.case_PlusOpt:<span style="color: #008000;">//&lt;PlusOpt&gt; ::= "+" &lt;Multiply&gt; | "-" &lt;Multiply&gt; | null;</span>
                    <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_plus_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = tree.Children[1].Tag;
                    }
                    <span style="color: #0000ff;">else</span> <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_minus_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = -<span style="color: #0000ff;">double</span>.Parse(tree.Children[1].Tag.ToString());
                    }
                    <span style="color: #0000ff;">else</span> <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_rightParentheses_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_PlusOpt___tail_startEndLeave())
                    {
                        tree.Tag = 0;
                    }
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.case_Multiply:<span style="color: #008000;">//&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</span>
                    _GetValue(tree.Children[0]);
                    _GetValue(tree.Children[1]);
                    tree.Tag = <span style="color: #0000ff;">double</span>.Parse(tree.Children[0].Tag.ToString()) * <span style="color: #0000ff;">double</span>.Parse(tree.Children[1].Tag.ToString());
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.case_MultiplyOpt:<span style="color: #008000;">//&lt;MultiplyOpt&gt; ::= "*" &lt;Unit&gt; | "/" &lt;Unit&gt; | null;</span>
                    <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_multiply_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = <span style="color: #0000ff;">double</span>.Parse(tree.Children[1].Tag.ToString());
                    }
                    <span style="color: #0000ff;">else</span> <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_divide_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = 1 / (<span style="color: #0000ff;">double</span>)tree.Children[1].Tag;
                    }
                    <span style="color: #0000ff;">else</span> <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_plus_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_minus_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_rightParentheses_Leave()
                        || tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_MultiplyOpt___tail_startEndLeave())
                    {
                        tree.Tag = 1;
                    }
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">case</span> EnumVTypeExpression.case_Unit:<span style="color: #008000;">//&lt;Unit&gt; ::= number | "(" &lt;Expression&gt; ")";</span>
                    <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_Unit___numberLeave())
                    {
                        tree.Tag = <span style="color: #0000ff;">double</span>.Parse(tree.Children[0].NodeValue.NodeName);
                    }
                    <span style="color: #0000ff;">else</span> <span style="color: #0000ff;">if</span> (tree.CandidateFunc == LL1SyntaxParserExpression.GetFuncParsecase_Unit___tail_leftParentheses_Leave())
                    {
                        _GetValue(tree.Children[1]);
                        tree.Tag = tree.Children[1].Tag;
                    }
                    <span style="color: #0000ff;">break</span>;
                <span style="color: #0000ff;">default</span>:
                    <span style="color: #0000ff;">break</span>;
            }
        }


    }
</span></pre>
</div>
<p>&nbsp;</p>
<h3>ArmadaTank模型</h3>
<p>坦克舰队（ArmadaTank）是我很喜欢的一款游戏，现在我正在试图用C#重写这个游戏。喜欢的同学可以自行搜索&ldquo;坦克舰队&rdquo;。</p>
<p>ArmadaTank的3D模型是用纯文本的*.dtm文件标识的。完全可以用自动生成的解析器来加载之。</p>
<p>步骤就不再说了，和Expression的步骤一样，这里只贴一下DTM文件的文法。</p>
<div class="cnblogs_code"><img id="Code_Closed_Image_175123" onclick="this.style.display='none'; document.getElementById('Code_Closed_Text_175123').style.display='none'; document.getElementById('Code_Open_Image_175123').style.display='inline'; document.getElementById('Code_Open_Text_175123').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ContractedBlock.gif" alt="" width="11" height="16" align="top" /><img id="Code_Open_Image_175123" style="display: none;" onclick="this.style.display='none'; document.getElementById('Code_Open_Text_175123').style.display='none'; getElementById('Code_Closed_Image_175123').style.display='inline'; getElementById('Code_Closed_Text_175123').style.display='inline';" src="http://www.cnblogs.com/Images/OutliningIndicators/ExpandedBlockStart.gif" alt="" width="11" height="16" align="top" />
<pre><span id="Code_Closed_Text_175123" class="cnblogs_code_Collapse">DTM的文法</span><span id="Code_Open_Text_175123" style="display: none;">&lt;ArmadaTanksModel&gt; ::= "<span style="color: #8b0000;">File</span>" &lt;FileContent&gt; "<span style="color: #8b0000;">endfile</span>";

&lt;FileContent&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;BlockList&gt; "<span style="color: #8b0000;">}</span>";

&lt;BlockList&gt; ::= &lt;Block&gt; &lt;BlockList&gt; | <span style="color: #0000ff;">null</span>;

&lt;Block&gt; ::= "<span style="color: #8b0000;">FileDesc</span>" &lt;FileDesc&gt; "<span style="color: #8b0000;">endfiledesc</span>" | "<span style="color: #8b0000;">Faces</span>" &lt;Faces&gt; | "<span style="color: #8b0000;">MapChannel</span>" &lt;SignedNumber&gt; &lt;MapChannel&gt; | "<span style="color: #8b0000;">Frame</span>" &lt;SignedNumber&gt; &lt;Frame&gt; "<span style="color: #8b0000;">endframe</span>";

&lt;FileDesc&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;FileDescItemList&gt; "<span style="color: #8b0000;">}</span>";

&lt;FileDescItemList&gt; ::= &lt;FileDescItem&gt; &lt;FileDescItemList&gt; | <span style="color: #0000ff;">null</span>;

&lt;FileDescItem&gt; ::= "<span style="color: #8b0000;">Frames</span>" &lt;SignedNumber&gt; | "<span style="color: #8b0000;">Vertices</span>" &lt;SignedNumber&gt; | "<span style="color: #8b0000;">Faces</span>" &lt;SignedNumber&gt; | "<span style="color: #8b0000;">Map</span>" &lt;SignedNumber&gt; "<span style="color: #8b0000;">TVertices</span>" &lt;SignedNumber&gt;;

&lt;Faces&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;FaceList&gt; "<span style="color: #8b0000;">}</span>";

&lt;FaceList&gt; ::= &lt;Face&gt; &lt;FaceList&gt; | <span style="color: #0000ff;">null</span>;

&lt;Face&gt; ::= "<span style="color: #8b0000;">Face</span>" &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt; "<span style="color: #8b0000;">MatID</span>" &lt;SignedNumber&gt;;

&lt;MapChannel&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;TextureList&gt; "<span style="color: #8b0000;">}</span>";

&lt;TextureList&gt; ::= &lt;Texture&gt; &lt;TextureList&gt; | <span style="color: #0000ff;">null</span>;

&lt;Texture&gt; ::= "<span style="color: #8b0000;">TextureVertices</span>" &lt;TextureVertices&gt; | "<span style="color: #8b0000;">TextureFaces</span>" &lt;TextureFaces&gt;;

&lt;TextureVertices&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;TVertexList&gt; "<span style="color: #8b0000;">}</span>";

&lt;TVertexList&gt; ::= &lt;TVertex&gt; &lt;TVertexList&gt; | <span style="color: #0000ff;">null</span>;

&lt;TVertex&gt; ::= "<span style="color: #8b0000;">TVertex</span>" &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt;;

&lt;TextureFaces&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;TFaceList&gt; "<span style="color: #8b0000;">}</span>";

&lt;TFaceList&gt; ::= &lt;TFace&gt; &lt;TFaceList&gt; | <span style="color: #0000ff;">null</span>;

&lt;TFace&gt; ::= "<span style="color: #8b0000;">TFace</span>" &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt;;

&lt;Frame&gt; ::= "<span style="color: #8b0000;">{</span>" &lt;FrameContentItemList&gt; "<span style="color: #8b0000;">}</span>";

&lt;FrameContentItemList&gt; ::= &lt;FrameContentItem&gt; &lt;FrameContentItemList&gt; | <span style="color: #0000ff;">null</span>;

&lt;FrameContentItem&gt; ::= "<span style="color: #8b0000;">Vertices</span>" "<span style="color: #8b0000;">{</span>" &lt;Vertices&gt; "<span style="color: #8b0000;">}</span>";

&lt;Vertices&gt; ::= &lt;Vertex&gt; &lt;Vertices&gt; | <span style="color: #0000ff;">null</span>;

&lt;Vertex&gt; ::= "<span style="color: #8b0000;">Vertex</span>" &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt; &lt;SignedNumber&gt;;

&lt;SignedNumber&gt; ::= "<span style="color: #8b0000;">+</span>" number | "<span style="color: #8b0000;">-</span>" number | number;</span></pre>
</div>
<p>&nbsp;</p>
<p>用OpenGL来显示3D模型（语义分析及其之后的阶段），如下图所示。</p>
<p><a href="http://images.cnitblog.com/blog/383191/201311/20194307-ee1cbca59cd6456ebf8b880301ec7b3c.jpg"><img style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border: 0px;" title="clip_image011[8]" src="http://images.cnitblog.com/blog/383191/201311/20194308-cbf77c9cf7d546dd970df5efa9f971f8.jpg" alt="clip_image011[8]" width="558" height="449" border="0" /></a></p>
<p>源代码在此。</p>
<p><a href="http://files.cnblogs.com/bitzhuwei/bitzhuwei.CGCompiler2013-11-20_19-27-00.rar">http://files.cnblogs.com/bitzhuwei/bitzhuwei.CGCompiler2013-11-20_19-27-00.rar</a></p>
