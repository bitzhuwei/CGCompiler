namespace CSharpGL.GLSL430Compiler
{
    /// <summary>
    /// 文法GLSL430的语法树结点枚举类型
    /// </summary>
    public enum EnumVTypeGLSL430
    {
        /// <summary>
        /// 未知的语法结点符号
        /// </summary>
        Unknown,
        /// <summary>
        /// &lt;translation_unit_nullable&gt; ::= &lt;external_declaration&gt; &lt;translation_unit_nullable&gt; | null;
        /// </summary>
        case_translation_unit_nullable,
        /// <summary>
        /// &lt;external_declaration&gt; ::= &quot;precision&quot; &lt;precision_qualifier&gt; &lt;type_specifier&gt; &quot;;&quot; | &lt;type_specifier&gt; &lt;external_declaration_1&gt; | &lt;type_qualifier&gt; &lt;external_declaration_2&gt;;
        /// </summary>
        case_external_declaration,
        /// <summary>
        /// &lt;external_declaration_2&gt; ::= &quot;void&quot; &lt;array_specifier_nullable&gt; &lt;external_declaration_2_1&gt; | &lt;struct_specifier&gt; &lt;array_specifier_nullable&gt; &lt;external_declaration_2_2&gt; | identifier &lt;external_declaration_2_3&gt; | &quot;;&quot;;
        /// </summary>
        case_external_declaration_2,
        /// <summary>
        /// &lt;external_declaration_2_3&gt; ::= identifier &lt;external_declaration_2_3_1&gt;;
        /// </summary>
        case_external_declaration_2_3,
        /// <summary>
        /// &lt;external_declaration_2_3_1&gt; ::= &quot;[&quot; &lt;constant_expression_or_null&gt; &quot;]&quot; &lt;array_specifier_nullable&gt; &lt;external_declaration_2_3_1_1&gt; | identifier &lt;external_declaration_2_3_1_2&gt; | &lt;init_declarator_list_2&gt; &lt;semicolon_or_null&gt; | &quot;{&quot; &lt;struct_declaration_list&gt; &quot;}&quot; &lt;variable_name_or_null&gt; &quot;;&quot;;
        /// </summary>
        case_external_declaration_2_3_1,
        /// <summary>
        /// &lt;external_declaration_2_3_1_2&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &lt;compound_statement_no_new_scope_or_semicolon&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_3_1_2,
        /// <summary>
        /// &lt;external_declaration_2_3_1_1&gt; ::= identifier &lt;external_declaration_2_3_1_1_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_3_1_1,
        /// <summary>
        /// &lt;external_declaration_2_3_1_1_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &lt;compound_statement_no_new_scope_or_semicolon&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_3_1_1_1,
        /// <summary>
        /// &lt;external_declaration_2_2&gt; ::= identifier &lt;external_declaration_2_2_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_2,
        /// <summary>
        /// &lt;external_declaration_2_2_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &lt;compound_statement_no_new_scope_or_semicolon&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_2_1,
        /// <summary>
        /// &lt;external_declaration_2_1&gt; ::= identifier &lt;external_declaration_2_1_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_1,
        /// <summary>
        /// &lt;external_declaration_2_1_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &lt;compound_statement_no_new_scope_or_semicolon&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_2_1_1,
        /// <summary>
        /// &lt;external_declaration_1&gt; ::= &lt;init_declarator_list_2&gt; | identifier &lt;external_declaration_1_1&gt;;
        /// </summary>
        case_external_declaration_1,
        /// <summary>
        /// &lt;external_declaration_1_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &lt;compound_statement_no_new_scope_or_semicolon&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_external_declaration_1_1,
        /// <summary>
        /// &lt;compound_statement_no_new_scope_or_semicolon&gt; ::= &lt;compound_statement_no_new_scope&gt; | &quot;;&quot;;
        /// </summary>
        case_compound_statement_no_new_scope_or_semicolon,
        /// <summary>
        /// &lt;declaration_or_expression_statement&gt; ::= identifier &lt;declaration_or_expression_statement_1&gt;;
        /// </summary>
        case_declaration_or_expression_statement,
        /// <summary>
        /// &lt;declaration_or_expression_statement_1&gt; ::= identifier &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_or_expression_statement_1,
        /// <summary>
        /// &lt;statement&gt; ::= &lt;compound_statement&gt; | &lt;simple_statement&gt;;
        /// </summary>
        case_statement,
        /// <summary>
        /// &lt;simple_statement&gt; ::= &lt;declaration_or_expression_statement&gt; | &lt;selection_statement&gt; | &lt;switch_statement&gt; | &lt;case_label&gt; | &lt;iteration_statement&gt; | &lt;jump_statement&gt;;
        /// </summary>
        case_simple_statement,
        /// <summary>
        /// &lt;selection_statement&gt; ::= &quot;if&quot; &quot;(&quot; &lt;expression&gt; &quot;)&quot; &lt;selection_rest_statement&gt;;
        /// </summary>
        case_selection_statement,
        /// <summary>
        /// &lt;selection_rest_statement&gt; ::= &lt;statement&gt; &lt;selection_rest_statement_2&gt;;
        /// </summary>
        case_selection_rest_statement,
        /// <summary>
        /// &lt;selection_rest_statement_2&gt; ::= &quot;else&quot; &lt;statement&gt; | null;
        /// </summary>
        case_selection_rest_statement_2,
        /// <summary>
        /// &lt;switch_statement&gt; ::= &quot;switch&quot; &quot;(&quot; &lt;expression&gt; &quot;)&quot; &quot;{&quot; &lt;switch_statement_list&gt; &quot;}&quot;;
        /// </summary>
        case_switch_statement,
        /// <summary>
        /// &lt;switch_statement_list&gt; ::= &lt;statement_list_nullable&gt;;
        /// </summary>
        case_switch_statement_list,
        /// <summary>
        /// &lt;case_label&gt; ::= &quot;case&quot; &lt;expression&gt; &quot;:&quot; | &quot;default&quot; &quot;:&quot;;
        /// </summary>
        case_case_label,
        /// <summary>
        /// &lt;iteration_statement&gt; ::= &quot;while&quot; &quot;(&quot; &lt;condition&gt; &quot;)&quot; &lt;statement_no_new_scope&gt; | &quot;do&quot; &lt;statement&gt; &quot;while&quot; &quot;(&quot; &lt;expression&gt; &quot;)&quot; &quot;;&quot; | &quot;for&quot; &quot;(&quot; &lt;for_init_statement&gt; &lt;for_rest_statement&gt; &quot;)&quot; &lt;statement_no_new_scope&gt;;
        /// </summary>
        case_iteration_statement,
        /// <summary>
        /// &lt;for_init_statement&gt; ::= &lt;declaration_or_expression_statement&gt;;
        /// </summary>
        case_for_init_statement,
        /// <summary>
        /// &lt;expression_statement&gt; ::= &quot;;&quot; | &lt;expression&gt; &quot;;&quot;;
        /// </summary>
        case_expression_statement,
        /// <summary>
        /// &lt;declaration_statement&gt; ::= &lt;declaration&gt;;
        /// </summary>
        case_declaration_statement,
        /// <summary>
        /// &lt;declaration&gt; ::= &quot;precision&quot; &lt;precision_qualifier&gt; &lt;type_specifier&gt; &quot;;&quot; | &lt;type_specifier&gt; &lt;declaration_2&gt; | &lt;type_qualifier&gt; &lt;declaration_3&gt;;
        /// </summary>
        case_declaration,
        /// <summary>
        /// &lt;declaration_3&gt; ::= &quot;void&quot; &lt;array_specifier_nullable&gt; &lt;declaration_3_1&gt; | &lt;struct_specifier&gt; &lt;array_specifier_nullable&gt; &lt;declaration_3_2&gt; | &quot;;&quot; | identifier &lt;declaration_3_3&gt;;
        /// </summary>
        case_declaration_3,
        /// <summary>
        /// &lt;declaration_3_3&gt; ::= &lt;init_declarator_list_2&gt; &lt;semicolon_or_null&gt; | &quot;[&quot; &lt;constant_expression_or_null&gt; &quot;]&quot; &lt;array_specifier_nullable&gt; &lt;declaration_3_3_1&gt; | identifier &lt;declaration_3_3_2&gt; | &quot;{&quot; &lt;struct_declaration_list&gt; &quot;}&quot; &lt;variable_name_or_null&gt; &quot;;&quot;;
        /// </summary>
        case_declaration_3_3,
        /// <summary>
        /// &lt;semicolon_or_null&gt; ::= &quot;;&quot; | null;
        /// </summary>
        case_semicolon_or_null,
        /// <summary>
        /// &lt;declaration_3_3_2&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &quot;;&quot; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_3_2,
        /// <summary>
        /// &lt;declaration_3_3_1&gt; ::= identifier &lt;declaration_3_3_1_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_3_1,
        /// <summary>
        /// &lt;declaration_3_3_1_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &quot;;&quot; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_3_1_1,
        /// <summary>
        /// &lt;declaration_3_2&gt; ::= identifier &lt;declaration_3_2_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_2,
        /// <summary>
        /// &lt;declaration_3_2_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &quot;;&quot; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_2_1,
        /// <summary>
        /// &lt;declaration_3_1&gt; ::= identifier &lt;declaration_3_1_1&gt; | &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_1,
        /// <summary>
        /// &lt;declaration_3_1_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &quot;;&quot; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_3_1_1,
        /// <summary>
        /// &lt;declaration_2&gt; ::= &lt;init_declarator_list_2&gt; | identifier &lt;declaration_2_1&gt;;
        /// </summary>
        case_declaration_2,
        /// <summary>
        /// &lt;declaration_2_1&gt; ::= &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; &quot;;&quot; | &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_declaration_2_1,
        /// <summary>
        /// &lt;function_prototype&gt; ::= &lt;type_specifier&gt; identifier &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot; | &lt;type_qualifier&gt; &lt;type_specifier&gt; identifier &quot;(&quot; &lt;function_parameters_or_null&gt; &quot;)&quot;;
        /// </summary>
        case_function_prototype,
        /// <summary>
        /// &lt;function_declarator&gt; ::= &lt;function_header&gt; &lt;function_parameters_or_null&gt;;
        /// </summary>
        case_function_declarator,
        /// <summary>
        /// &lt;function_parameters_or_null&gt; ::= &lt;parameter_declaration&gt; &lt;function_header_with_parameters_2&gt; | null;
        /// </summary>
        case_function_parameters_or_null,
        /// <summary>
        /// &lt;function_header_with_parameters&gt; ::= &lt;function_header&gt; &lt;parameter_declaration&gt; &lt;function_header_with_parameters_2&gt;;
        /// </summary>
        case_function_header_with_parameters,
        /// <summary>
        /// &lt;function_header_with_parameters_2&gt; ::= &quot;,&quot; &lt;parameter_declaration&gt; &lt;function_header_with_parameters_2&gt; | null;
        /// </summary>
        case_function_header_with_parameters_2,
        /// <summary>
        /// &lt;function_header&gt; ::= &lt;fully_specified_type&gt; identifier &quot;(&quot;;
        /// </summary>
        case_function_header,
        /// <summary>
        /// &lt;parameter_declaration&gt; ::= &lt;type_specifier&gt; &lt;variable_name_or_null&gt; | &lt;type_qualifier&gt; &lt;type_specifier&gt; &lt;variable_name_or_null&gt;;
        /// </summary>
        case_parameter_declaration,
        /// <summary>
        /// &lt;parameter_declarator&gt; ::= &lt;type_specifier&gt; identifier &lt;array_specifier_nullable&gt;;
        /// </summary>
        case_parameter_declarator,
        /// <summary>
        /// &lt;parameter_type_specifier&gt; ::= &lt;type_specifier&gt;;
        /// </summary>
        case_parameter_type_specifier,
        /// <summary>
        /// &lt;init_declarator_list&gt; ::= &lt;single_declaration&gt; &lt;init_declarator_list_2&gt;;
        /// </summary>
        case_init_declarator_list,
        /// <summary>
        /// &lt;init_declarator_list_2&gt; ::= &quot;,&quot; identifier &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; &lt;init_declarator_list_2&gt; | null;
        /// </summary>
        case_init_declarator_list_2,
        /// <summary>
        /// &lt;equal_initializer_nullable&gt; ::= &quot;=&quot; &lt;initializer&gt; | null;
        /// </summary>
        case_equal_initializer_nullable,
        /// <summary>
        /// &lt;initializer&gt; ::= &lt;assignment_expression&gt; | &quot;{&quot; &lt;initializer_list&gt; &lt;comma_or_null&gt; &quot;}&quot;;
        /// </summary>
        case_initializer,
        /// <summary>
        /// &lt;initializer_list&gt; ::= &lt;initializer&gt; &lt;initializer_list_2&gt;;
        /// </summary>
        case_initializer_list,
        /// <summary>
        /// &lt;initializer_list_2&gt; ::= &quot;,&quot; &lt;initializer&gt; &lt;initializer_list_2&gt; | null;
        /// </summary>
        case_initializer_list_2,
        /// <summary>
        /// &lt;comma_or_null&gt; ::= &quot;,&quot; | null;
        /// </summary>
        case_comma_or_null,
        /// <summary>
        /// &lt;single_declaration&gt; ::= &lt;fully_specified_type&gt; &lt;variable_initializer_nullable&gt;;
        /// </summary>
        case_single_declaration,
        /// <summary>
        /// &lt;variable_initializer_nullable&gt; ::= identifier &lt;array_specifier_nullable&gt; &lt;equal_initializer_nullable&gt; | null;
        /// </summary>
        case_variable_initializer_nullable,
        /// <summary>
        /// &lt;identifier_list_or_null&gt; ::= &lt;identifier_list&gt; | null;
        /// </summary>
        case_identifier_list_or_null,
        /// <summary>
        /// &lt;identifier_list&gt; ::= identifier &lt;identifier_list_2&gt;;
        /// </summary>
        case_identifier_list,
        /// <summary>
        /// &lt;identifier_list_2&gt; ::= &quot;,&quot; identifier &lt;identifier_list_2&gt; | null;
        /// </summary>
        case_identifier_list_2,
        /// <summary>
        /// &lt;variable_name_or_null&gt; ::= identifier &lt;array_specifier_nullable&gt; | null;
        /// </summary>
        case_variable_name_or_null,
        /// <summary>
        /// &lt;for_rest_statement&gt; ::= &lt;conditionopt&gt; &quot;;&quot; &lt;expression_or_null&gt;;
        /// </summary>
        case_for_rest_statement,
        /// <summary>
        /// &lt;conditionopt&gt; ::= &lt;condition&gt;;
        /// </summary>
        case_conditionopt,
        /// <summary>
        /// &lt;condition&gt; ::= &lt;expression&gt;;
        /// </summary>
        case_condition,
        /// <summary>
        /// &lt;statement_no_new_scope&gt; ::= &lt;compound_statement_no_new_scope&gt; | &lt;simple_statement&gt;;
        /// </summary>
        case_statement_no_new_scope,
        /// <summary>
        /// &lt;compound_statement_no_new_scope&gt; ::= &quot;{&quot; &lt;statement_list_nullable&gt; &quot;}&quot;;
        /// </summary>
        case_compound_statement_no_new_scope,
        /// <summary>
        /// &lt;jump_statement&gt; ::= &quot;continue&quot; &quot;;&quot; | &quot;break&quot; &quot;;&quot; | &quot;return&quot; &lt;expression_or_null&gt; &quot;;&quot; | &quot;discard&quot; &quot;;&quot;;
        /// </summary>
        case_jump_statement,
        /// <summary>
        /// &lt;expression_or_null&gt; ::= &lt;expression&gt; | null;
        /// </summary>
        case_expression_or_null,
        /// <summary>
        /// &lt;compound_statement&gt; ::= &quot;{&quot; &lt;statement_list_nullable&gt; &quot;}&quot;;
        /// </summary>
        case_compound_statement,
        /// <summary>
        /// &lt;statement_list&gt; ::= &lt;statement&gt; &lt;statement_list_nullable&gt;;
        /// </summary>
        case_statement_list,
        /// <summary>
        /// &lt;statement_list_nullable&gt; ::= &lt;statement&gt; &lt;statement_list_nullable&gt; | null;
        /// </summary>
        case_statement_list_nullable,
        /// <summary>
        /// &lt;expression&gt; ::= &lt;assignment_expression&gt; &lt;expression_2&gt;;
        /// </summary>
        case_expression,
        /// <summary>
        /// &lt;expression_2&gt; ::= &quot;,&quot; &lt;assignment_expression&gt; &lt;expression_2&gt; | null;
        /// </summary>
        case_expression_2,
        /// <summary>
        /// &lt;assignment_expression&gt; ::= &lt;unary_expression&gt; &lt;assignment_expression_2&gt;;
        /// </summary>
        case_assignment_expression,
        /// <summary>
        /// &lt;unary_expression&gt; ::= &lt;postfix_expression&gt; | &quot;++&quot; &lt;unary_expression&gt; | &quot;--&quot; &lt;unary_expression&gt; | &lt;unary_operator&gt; &lt;unary_expression&gt;;
        /// </summary>
        case_unary_expression,
        /// <summary>
        /// &lt;postfix_expression&gt; ::= identifier &lt;postfix_expression_3&gt; | &lt;constant_primary_expression&gt; &lt;postfix_expression_2&gt; | &quot;(&quot; &lt;expression&gt; &quot;)&quot; &lt;postfix_expression_2&gt;;
        /// </summary>
        case_postfix_expression,
        /// <summary>
        /// &lt;postfix_expression_3&gt; ::= &quot;[&quot; &lt;integer_expression&gt; &quot;]&quot; &lt;postfix_expression_3_1&gt; | &quot;.&quot; &lt;FIELD_SELECTION&gt; &lt;postfix_expression_2&gt; | &quot;++&quot; &lt;postfix_expression_2&gt; | &quot;--&quot; &lt;postfix_expression_2&gt; | null | &quot;(&quot; &lt;expression_or_void_or_null&gt; &quot;)&quot; &lt;postfix_expression_2&gt;;
        /// </summary>
        case_postfix_expression_3,
        /// <summary>
        /// &lt;postfix_expression_3_1&gt; ::= &lt;postfix_expression_2&gt; | &quot;(&quot; &lt;expression_or_void_or_null&gt; &quot;)&quot; &lt;postfix_expression_2&gt;;
        /// </summary>
        case_postfix_expression_3_1,
        /// <summary>
        /// &lt;postfix_expression_2&gt; ::= &quot;[&quot; &lt;integer_expression&gt; &quot;]&quot; &lt;postfix_expression_2&gt; | &quot;.&quot; &lt;FIELD_SELECTION&gt; &lt;postfix_expression_2&gt; | &quot;++&quot; &lt;postfix_expression_2&gt; | &quot;--&quot; &lt;postfix_expression_2&gt; | null;
        /// </summary>
        case_postfix_expression_2,
        /// <summary>
        /// &lt;FIELD_SELECTION&gt; ::= identifier;
        /// </summary>
        case_FIELD_SELECTION,
        /// <summary>
        /// &lt;primary_expression&gt; ::= identifier | &lt;constant_primary_expression&gt; | &quot;(&quot; &lt;expression&gt; &quot;)&quot;;
        /// </summary>
        case_primary_expression,
        /// <summary>
        /// &lt;constant_primary_expression&gt; ::= number | &lt;bool_constant&gt;;
        /// </summary>
        case_constant_primary_expression,
        /// <summary>
        /// &lt;bool_constant&gt; ::= &quot;true&quot; | &quot;false&quot;;
        /// </summary>
        case_bool_constant,
        /// <summary>
        /// &lt;unary_operator&gt; ::= &quot;+&quot; | &quot;-&quot; | &quot;!&quot; | &quot;~&quot;;
        /// </summary>
        case_unary_operator,
        /// <summary>
        /// &lt;expression_or_void_or_null&gt; ::= &lt;expression&gt; | &quot;void&quot; | null;
        /// </summary>
        case_expression_or_void_or_null,
        /// <summary>
        /// &lt;index_selector_nullable&gt; ::= &quot;[&quot; &lt;integer_expression&gt; &quot;]&quot; | null;
        /// </summary>
        case_index_selector_nullable,
        /// <summary>
        /// &lt;integer_expression&gt; ::= &lt;expression&gt;;
        /// </summary>
        case_integer_expression,
        /// <summary>
        /// &lt;assignment_expression_2&gt; ::= &lt;multiplicative_expression_2&gt; &lt;additive_expression_2&gt; &lt;shift_expression_2&gt; &lt;relational_expression_2&gt; &lt;equality_expression_2&gt; &lt;and_expression_2&gt; &lt;exclusive_or_expression_2&gt; &lt;inclusive_or_expression_2&gt; &lt;logical_and_expression_2&gt; &lt;logical_xor_expression_2&gt; &lt;logical_or_expression_2&gt; &lt;conditional_expression_2&gt; | &lt;assignment_operator&gt; &lt;assignment_expression&gt;;
        /// </summary>
        case_assignment_expression_2,
        /// <summary>
        /// &lt;assignment_operator&gt; ::= &quot;=&quot; | &quot;*=&quot; | &quot;/=&quot; | &quot;%=&quot; | &quot;+=&quot; | &quot;-=&quot; | &quot;&lt;&lt;=&quot; | &quot;&gt;&gt;=&quot; | &quot;&amp;=&quot; | &quot;^=&quot; | &quot;|=&quot;;
        /// </summary>
        case_assignment_operator,
        /// <summary>
        /// &lt;conditional_expression&gt; ::= &lt;logical_or_expression&gt; &lt;conditional_expression_2&gt;;
        /// </summary>
        case_conditional_expression,
        /// <summary>
        /// &lt;conditional_expression_2&gt; ::= &quot;?&quot; &lt;expression&gt; &quot;:&quot; &lt;assignment_expression&gt; | null;
        /// </summary>
        case_conditional_expression_2,
        /// <summary>
        /// &lt;logical_or_expression&gt; ::= &lt;logical_xor_expression&gt; &lt;logical_or_expression_2&gt;;
        /// </summary>
        case_logical_or_expression,
        /// <summary>
        /// &lt;logical_or_expression_2&gt; ::= &quot;||&quot; &lt;logical_xor_expression&gt; &lt;logical_or_expression_2&gt; | null;
        /// </summary>
        case_logical_or_expression_2,
        /// <summary>
        /// &lt;logical_xor_expression&gt; ::= &lt;logical_and_expression&gt; &lt;logical_xor_expression_2&gt;;
        /// </summary>
        case_logical_xor_expression,
        /// <summary>
        /// &lt;logical_xor_expression_2&gt; ::= &quot;^^&quot; &lt;logical_and_expression&gt; &lt;logical_xor_expression_2&gt; | null;
        /// </summary>
        case_logical_xor_expression_2,
        /// <summary>
        /// &lt;logical_and_expression&gt; ::= &lt;inclusive_or_expression&gt; &lt;logical_and_expression_2&gt;;
        /// </summary>
        case_logical_and_expression,
        /// <summary>
        /// &lt;logical_and_expression_2&gt; ::= &quot;&amp;&amp;&quot; &lt;inclusive_or_expression&gt; &lt;logical_and_expression_2&gt; | null;
        /// </summary>
        case_logical_and_expression_2,
        /// <summary>
        /// &lt;inclusive_or_expression&gt; ::= &lt;exclusive_or_expression&gt; &lt;inclusive_or_expression_2&gt;;
        /// </summary>
        case_inclusive_or_expression,
        /// <summary>
        /// &lt;inclusive_or_expression_2&gt; ::= &quot;|&quot; &lt;exclusive_or_expression&gt; &lt;inclusive_or_expression_2&gt; | null;
        /// </summary>
        case_inclusive_or_expression_2,
        /// <summary>
        /// &lt;exclusive_or_expression&gt; ::= &lt;and_expression&gt; &lt;exclusive_or_expression_2&gt;;
        /// </summary>
        case_exclusive_or_expression,
        /// <summary>
        /// &lt;exclusive_or_expression_2&gt; ::= &quot;^&quot; &lt;and_expression&gt; &lt;exclusive_or_expression_2&gt; | null;
        /// </summary>
        case_exclusive_or_expression_2,
        /// <summary>
        /// &lt;and_expression&gt; ::= &lt;equality_expression&gt; &lt;and_expression_2&gt;;
        /// </summary>
        case_and_expression,
        /// <summary>
        /// &lt;and_expression_2&gt; ::= &quot;&amp;&quot; &lt;equality_expression&gt; &lt;and_expression_2&gt; | null;
        /// </summary>
        case_and_expression_2,
        /// <summary>
        /// &lt;equality_expression&gt; ::= &lt;relational_expression&gt; &lt;equality_expression_2&gt;;
        /// </summary>
        case_equality_expression,
        /// <summary>
        /// &lt;equality_expression_2&gt; ::= &quot;==&quot; &lt;relational_expression&gt; &lt;equality_expression_2&gt; | &quot;!=&quot; &lt;relational_expression&gt; &lt;equality_expression_2&gt; | null;
        /// </summary>
        case_equality_expression_2,
        /// <summary>
        /// &lt;relational_expression&gt; ::= &lt;shift_expression&gt; &lt;relational_expression_2&gt;;
        /// </summary>
        case_relational_expression,
        /// <summary>
        /// &lt;relational_expression_2&gt; ::= &quot;&lt;&quot; &lt;shift_expression&gt; &lt;relational_expression_2&gt; | &quot;&gt;&quot; &lt;shift_expression&gt; &lt;relational_expression_2&gt; | &quot;&lt;=&quot; &lt;shift_expression&gt; &lt;relational_expression_2&gt; | &quot;&gt;=&quot; &lt;shift_expression&gt; &lt;relational_expression_2&gt; | null;
        /// </summary>
        case_relational_expression_2,
        /// <summary>
        /// &lt;shift_expression&gt; ::= &lt;additive_expression&gt; &lt;shift_expression_2&gt;;
        /// </summary>
        case_shift_expression,
        /// <summary>
        /// &lt;shift_expression_2&gt; ::= &quot;&lt;&lt;&quot; &lt;additive_expression&gt; &lt;shift_expression_2&gt; | &quot;&gt;&gt;&quot; &lt;additive_expression&gt; &lt;shift_expression_2&gt; | null;
        /// </summary>
        case_shift_expression_2,
        /// <summary>
        /// &lt;additive_expression&gt; ::= &lt;multiplicative_expression&gt; &lt;additive_expression_2&gt;;
        /// </summary>
        case_additive_expression,
        /// <summary>
        /// &lt;additive_expression_2&gt; ::= &quot;+&quot; &lt;multiplicative_expression&gt; &lt;additive_expression_2&gt; | &quot;-&quot; &lt;multiplicative_expression&gt; &lt;additive_expression_2&gt; | null;
        /// </summary>
        case_additive_expression_2,
        /// <summary>
        /// &lt;multiplicative_expression&gt; ::= &lt;unary_expression&gt; &lt;multiplicative_expression_2&gt;;
        /// </summary>
        case_multiplicative_expression,
        /// <summary>
        /// &lt;multiplicative_expression_2&gt; ::= &quot;*&quot; &lt;unary_expression&gt; &lt;multiplicative_expression_2&gt; | &quot;/&quot; &lt;unary_expression&gt; &lt;multiplicative_expression_2&gt; | &quot;%&quot; &lt;unary_expression&gt; &lt;multiplicative_expression_2&gt; | null;
        /// </summary>
        case_multiplicative_expression_2,
        /// <summary>
        /// &lt;fully_specified_type&gt; ::= &lt;type_specifier&gt; | &lt;type_qualifier&gt; &lt;type_specifier&gt;;
        /// </summary>
        case_fully_specified_type,
        /// <summary>
        /// &lt;type_specifier&gt; ::= &lt;type_specifier_nonarray&gt; &lt;array_specifier_nullable&gt;;
        /// </summary>
        case_type_specifier,
        /// <summary>
        /// &lt;array_specifier&gt; ::= &quot;[&quot; &lt;constant_expression_or_null&gt; &quot;]&quot; &lt;array_specifier_nullable&gt;;
        /// </summary>
        case_array_specifier,
        /// <summary>
        /// &lt;array_specifier_nullable&gt; ::= &quot;[&quot; &lt;constant_expression_or_null&gt; &quot;]&quot; &lt;array_specifier_nullable&gt; | null;
        /// </summary>
        case_array_specifier_nullable,
        /// <summary>
        /// &lt;constant_expression_or_null&gt; ::= &lt;conditional_expression&gt; | null;
        /// </summary>
        case_constant_expression_or_null,
        /// <summary>
        /// &lt;constant_expression&gt; ::= &lt;conditional_expression&gt;;
        /// </summary>
        case_constant_expression,
        /// <summary>
        /// &lt;type_specifier_nonarray&gt; ::= &quot;void&quot; | &lt;struct_specifier&gt; | identifier;
        /// </summary>
        case_type_specifier_nonarray,
        /// <summary>
        /// &lt;build_in_type&gt; ::= &quot;float&quot; | &quot;double&quot; | &quot;int&quot; | &quot;uint&quot; | &quot;bool&quot; | &quot;vec2&quot; | &quot;vec3&quot; | &quot;vec4&quot; | &quot;dvec2&quot; | &quot;dvec3&quot; | &quot;dvec4&quot; | &quot;bvec2&quot; | &quot;bvec3&quot; | &quot;bvec4&quot; | &quot;ivec2&quot; | &quot;ivec3&quot; | &quot;ivec4&quot; | &quot;uvec2&quot; | &quot;uvec3&quot; | &quot;uvec4&quot; | &quot;mat2&quot; | &quot;mat3&quot; | &quot;mat4&quot; | &quot;mat2x2&quot; | &quot;mat2x3&quot; | &quot;mat2x4&quot; | &quot;mat3x2&quot; | &quot;mat3x3&quot; | &quot;mat3x4&quot; | &quot;mat4x2&quot; | &quot;mat4x3&quot; | &quot;mat4x4&quot; | &quot;dmat2&quot; | &quot;dmat3&quot; | &quot;dmat4&quot; | &quot;dmat2x2&quot; | &quot;dmat2x3&quot; | &quot;dmat2x4&quot; | &quot;dmat3x2&quot; | &quot;dmat3x3&quot; | &quot;dmat3x4&quot; | &quot;dmat4x2&quot; | &quot;dmat4x3&quot; | &quot;dmat4x4&quot; | &quot;atomic_uint&quot; | &quot;sampler1D&quot; | &quot;sampler2D&quot; | &quot;sampler3D&quot; | &quot;samplerCube&quot; | &quot;sampler1DShadow&quot; | &quot;sampler2DShadow&quot; | &quot;samplerCubeShadow&quot; | &quot;sampler1DArray&quot; | &quot;sampler2DArray&quot; | &quot;sampler1DArrayShadow&quot; | &quot;sampler2DArrayShadow&quot; | &quot;samplerCubeArray&quot; | &quot;samplerCubeArrayShadow&quot; | &quot;isampler1D&quot; | &quot;isampler2D&quot; | &quot;isampler3D&quot; | &quot;isamplerCube&quot; | &quot;isampler1DArray&quot; | &quot;isampler2DArray&quot; | &quot;isamplerCubeArray&quot; | &quot;usampler1D&quot; | &quot;usampler2D&quot; | &quot;usampler3D&quot; | &quot;usamplerCube&quot; | &quot;usampler1DArray&quot; | &quot;usampler2DArray&quot; | &quot;usamplerCubeArray&quot; | &quot;sampler2DRect&quot; | &quot;sampler2DRectShadow&quot; | &quot;isampler2DRect&quot; | &quot;usampler2DRect&quot; | &quot;samplerBuffer&quot; | &quot;isamplerBuffer&quot; | &quot;usamplerBuffer&quot; | &quot;sampler2DMS&quot; | &quot;isampler2DMS&quot; | &quot;usampler2DMS&quot; | &quot;sampler2DMSArray&quot; | &quot;isampler2DMSArray&quot; | &quot;usampler2DMSArray&quot; | &quot;image1D&quot; | &quot;iimage1D&quot; | &quot;uimage1D&quot; | &quot;image2D&quot; | &quot;iimage2D&quot; | &quot;uimage2D&quot; | &quot;image3D&quot; | &quot;iimage3D&quot; | &quot;uimage3D&quot; | &quot;image2DRect&quot; | &quot;iimage2DRect&quot; | &quot;uimage2DRect&quot; | &quot;imageCube&quot; | &quot;iimageCube&quot; | &quot;uimageCube&quot; | &quot;imageBuffer&quot; | &quot;iimageBuffer&quot; | &quot;uimageBuffer&quot; | &quot;image1DArray&quot; | &quot;iimage1DArray&quot; | &quot;uimage1DArray&quot; | &quot;image2DArray&quot; | &quot;iimage2DArray&quot; | &quot;uimage2DArray&quot; | &quot;imageCubeArray&quot; | &quot;iimageCubeArray&quot; | &quot;uimageCubeArray&quot; | &quot;image2DMS&quot; | &quot;iimage2DMS&quot; | &quot;uimage2DMS&quot; | &quot;image2DMSArray&quot; | &quot;iimage2DMSArray&quot; | &quot;uimage2DMSArray&quot;;
        /// </summary>
        case_build_in_type,
        /// <summary>
        /// &lt;struct_specifier&gt; ::= &quot;struct&quot; &lt;identifier_or_null&gt; &quot;{&quot; &lt;struct_declaration_list&gt; &quot;}&quot;;
        /// </summary>
        case_struct_specifier,
        /// <summary>
        /// &lt;identifier_or_null&gt; ::= identifier | null;
        /// </summary>
        case_identifier_or_null,
        /// <summary>
        /// &lt;struct_declaration_list&gt; ::= &lt;struct_declaration&gt; &lt;struct_declaration_list_nullable&gt;;
        /// </summary>
        case_struct_declaration_list,
        /// <summary>
        /// &lt;struct_declaration_list_nullable&gt; ::= &lt;struct_declaration&gt; &lt;struct_declaration_list_nullable&gt; | null;
        /// </summary>
        case_struct_declaration_list_nullable,
        /// <summary>
        /// &lt;struct_declaration&gt; ::= &lt;type_specifier&gt; &lt;struct_declarator_list&gt; &quot;;&quot; | &lt;type_qualifier&gt; &lt;type_specifier&gt; &lt;struct_declarator_list&gt; &quot;;&quot;;
        /// </summary>
        case_struct_declaration,
        /// <summary>
        /// &lt;struct_declarator_list&gt; ::= &lt;struct_declarator&gt; &lt;struct_declarator_list_2&gt;;
        /// </summary>
        case_struct_declarator_list,
        /// <summary>
        /// &lt;struct_declarator_list_2&gt; ::= &quot;,&quot; &lt;struct_declarator&gt; &lt;struct_declarator_list_2&gt; | null;
        /// </summary>
        case_struct_declarator_list_2,
        /// <summary>
        /// &lt;struct_declarator&gt; ::= identifier &lt;array_specifier_nullable&gt;;
        /// </summary>
        case_struct_declarator,
        /// <summary>
        /// &lt;type_qualifier&gt; ::= &lt;single_type_qualifier&gt; &lt;type_qualifier_nullable&gt;;
        /// </summary>
        case_type_qualifier,
        /// <summary>
        /// &lt;type_qualifier_nullable&gt; ::= &lt;single_type_qualifier&gt; &lt;type_qualifier_nullable&gt; | null;
        /// </summary>
        case_type_qualifier_nullable,
        /// <summary>
        /// &lt;single_type_qualifier&gt; ::= &lt;storage_qualifier&gt; | &lt;layout_qualifier&gt; | &lt;precision_qualifier&gt; | &lt;interpolation_qualifier&gt; | &lt;invariant_qualifier&gt; | &lt;precise_qualifier&gt;;
        /// </summary>
        case_single_type_qualifier,
        /// <summary>
        /// &lt;storage_qualifier&gt; ::= &quot;const&quot; | &quot;inout&quot; | &quot;in&quot; | &quot;out&quot; | &quot;centroid&quot; | &quot;patch&quot; | &quot;sample&quot; | &quot;uniform&quot; | &quot;buffer&quot; | &quot;shared&quot; | &quot;coherent&quot; | &quot;volatile&quot; | &quot;restrict&quot; | &quot;readonly&quot; | &quot;writeonly&quot; | &quot;subroutine&quot; &lt;subroutine_2&gt;;
        /// </summary>
        case_storage_qualifier,
        /// <summary>
        /// &lt;subroutine_2&gt; ::= &quot;(&quot; &lt;type_name_list&gt; &quot;)&quot; | null;
        /// </summary>
        case_subroutine_2,
        /// <summary>
        /// &lt;type_name_list&gt; ::= identifier &lt;type_name_list_2&gt;;
        /// </summary>
        case_type_name_list,
        /// <summary>
        /// &lt;type_name_list_2&gt; ::= &quot;,&quot; identifier &lt;type_name_list_2&gt; | null;
        /// </summary>
        case_type_name_list_2,
        /// <summary>
        /// &lt;layout_qualifier&gt; ::= &quot;layout&quot; &quot;(&quot; &lt;layout_qualifier_id_list&gt; &quot;)&quot;;
        /// </summary>
        case_layout_qualifier,
        /// <summary>
        /// &lt;layout_qualifier_id_list&gt; ::= &lt;layout_qualifier_id&gt; &lt;layout_qualifier_id_list_2&gt;;
        /// </summary>
        case_layout_qualifier_id_list,
        /// <summary>
        /// &lt;layout_qualifier_id_list_2&gt; ::= &quot;,&quot; &lt;layout_qualifier_id&gt; &lt;layout_qualifier_id_list_2&gt; | null;
        /// </summary>
        case_layout_qualifier_id_list_2,
        /// <summary>
        /// &lt;layout_qualifier_id&gt; ::= identifier &lt;layout_qualifier_id_2&gt;;
        /// </summary>
        case_layout_qualifier_id,
        /// <summary>
        /// &lt;layout_qualifier_id_2&gt; ::= &quot;=&quot; number | null;
        /// </summary>
        case_layout_qualifier_id_2,
        /// <summary>
        /// &lt;precision_qualifier&gt; ::= &quot;high_precision&quot; | &quot;medium_precision&quot; | &quot;low_precision&quot;;
        /// </summary>
        case_precision_qualifier,
        /// <summary>
        /// &lt;interpolation_qualifier&gt; ::= &quot;smooth&quot; | &quot;flat&quot; | &quot;noperspective&quot;;
        /// </summary>
        case_interpolation_qualifier,
        /// <summary>
        /// &lt;invariant_qualifier&gt; ::= &quot;invariant&quot;;
        /// </summary>
        case_invariant_qualifier,
        /// <summary>
        /// &lt;precise_qualifier&gt; ::= &quot;precise&quot;;
        /// </summary>
        case_precise_qualifier,
        /// <summary>
        /// null
        /// </summary>
        epsilonLeave,
        /// <summary>
        /// &quot;precision&quot;
        /// </summary>
        tail_precisionLeave,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        tail_semicolon_Leave,
        /// <summary>
        /// &quot;void&quot;
        /// </summary>
        tail_voidLeave,
        /// <summary>
        /// identifier
        /// </summary>
        identifierLeave,
        /// <summary>
        /// &quot;[&quot;
        /// </summary>
        tail_leftBracket_Leave,
        /// <summary>
        /// &quot;]&quot;
        /// </summary>
        tail_rightBracket_Leave,
        /// <summary>
        /// &quot;{&quot;
        /// </summary>
        tail_leftBrace_Leave,
        /// <summary>
        /// &quot;}&quot;
        /// </summary>
        tail_rightBrace_Leave,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        tail_leftParentheses_Leave,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        tail_rightParentheses_Leave,
        /// <summary>
        /// &quot;if&quot;
        /// </summary>
        tail_ifLeave,
        /// <summary>
        /// &quot;else&quot;
        /// </summary>
        tail_elseLeave,
        /// <summary>
        /// &quot;switch&quot;
        /// </summary>
        tail_switchLeave,
        /// <summary>
        /// &quot;case&quot;
        /// </summary>
        tail_caseLeave,
        /// <summary>
        /// &quot;:&quot;
        /// </summary>
        tail_colon_Leave,
        /// <summary>
        /// &quot;default&quot;
        /// </summary>
        tail_defaultLeave,
        /// <summary>
        /// &quot;while&quot;
        /// </summary>
        tail_whileLeave,
        /// <summary>
        /// &quot;do&quot;
        /// </summary>
        tail_doLeave,
        /// <summary>
        /// &quot;for&quot;
        /// </summary>
        tail_forLeave,
        /// <summary>
        /// &quot;,&quot;
        /// </summary>
        tail_comma_Leave,
        /// <summary>
        /// &quot;=&quot;
        /// </summary>
        tail_equality_Leave,
        /// <summary>
        /// &quot;continue&quot;
        /// </summary>
        tail_continueLeave,
        /// <summary>
        /// &quot;break&quot;
        /// </summary>
        tail_breakLeave,
        /// <summary>
        /// &quot;return&quot;
        /// </summary>
        tail_returnLeave,
        /// <summary>
        /// &quot;discard&quot;
        /// </summary>
        tail_discardLeave,
        /// <summary>
        /// &quot;++&quot;
        /// </summary>
        tail_plus_Plus_Leave,
        /// <summary>
        /// &quot;--&quot;
        /// </summary>
        tail_minus_Minus_Leave,
        /// <summary>
        /// &quot;.&quot;
        /// </summary>
        tail_dot_Leave,
        /// <summary>
        /// number
        /// </summary>
        numberLeave,
        /// <summary>
        /// &quot;true&quot;
        /// </summary>
        tail_trueLeave,
        /// <summary>
        /// &quot;false&quot;
        /// </summary>
        tail_falseLeave,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        tail_plus_Leave,
        /// <summary>
        /// &quot;-&quot;
        /// </summary>
        tail_minus_Leave,
        /// <summary>
        /// &quot;!&quot;
        /// </summary>
        tail_not_Leave,
        /// <summary>
        /// &quot;~&quot;
        /// </summary>
        tail_reverse_Leave,
        /// <summary>
        /// &quot;*=&quot;
        /// </summary>
        tail_multiply_Equality_Leave,
        /// <summary>
        /// &quot;/=&quot;
        /// </summary>
        tail_divide_Equality_Leave,
        /// <summary>
        /// &quot;%=&quot;
        /// </summary>
        tail_percent_Equality_Leave,
        /// <summary>
        /// &quot;+=&quot;
        /// </summary>
        tail_plus_Equality_Leave,
        /// <summary>
        /// &quot;-=&quot;
        /// </summary>
        tail_minus_Equality_Leave,
        /// <summary>
        /// &quot;&lt;&lt;=&quot;
        /// </summary>
        tail_lessThan_LessThan_Equality_Leave,
        /// <summary>
        /// &quot;&gt;&gt;=&quot;
        /// </summary>
        tail_greaterThan_GreaterThan_Equality_Leave,
        /// <summary>
        /// &quot;&amp;=&quot;
        /// </summary>
        tail_and_Equality_Leave,
        /// <summary>
        /// &quot;^=&quot;
        /// </summary>
        tail_xor_Equality_Leave,
        /// <summary>
        /// &quot;|=&quot;
        /// </summary>
        tail_or_Equality_Leave,
        /// <summary>
        /// &quot;?&quot;
        /// </summary>
        tail_question_Leave,
        /// <summary>
        /// &quot;||&quot;
        /// </summary>
        tail_or_Or_Leave,
        /// <summary>
        /// &quot;^^&quot;
        /// </summary>
        tail_xor_Xor_Leave,
        /// <summary>
        /// &quot;&amp;&amp;&quot;
        /// </summary>
        tail_and_And_Leave,
        /// <summary>
        /// &quot;|&quot;
        /// </summary>
        tail_or_Leave,
        /// <summary>
        /// &quot;^&quot;
        /// </summary>
        tail_xor_Leave,
        /// <summary>
        /// &quot;&amp;&quot;
        /// </summary>
        tail_and_Leave,
        /// <summary>
        /// &quot;==&quot;
        /// </summary>
        tail_equality_Equality_Leave,
        /// <summary>
        /// &quot;!=&quot;
        /// </summary>
        tail_not_Equality_Leave,
        /// <summary>
        /// &quot;&lt;&quot;
        /// </summary>
        tail_lessThan_Leave,
        /// <summary>
        /// &quot;&gt;&quot;
        /// </summary>
        tail_greaterThan_Leave,
        /// <summary>
        /// &quot;&lt;=&quot;
        /// </summary>
        tail_lessThan_Equality_Leave,
        /// <summary>
        /// &quot;&gt;=&quot;
        /// </summary>
        tail_greaterThan_Equality_Leave,
        /// <summary>
        /// &quot;&lt;&lt;&quot;
        /// </summary>
        tail_lessThan_LessThan_Leave,
        /// <summary>
        /// &quot;&gt;&gt;&quot;
        /// </summary>
        tail_greaterThan_GreaterThan_Leave,
        /// <summary>
        /// &quot;*&quot;
        /// </summary>
        tail_multiply_Leave,
        /// <summary>
        /// &quot;/&quot;
        /// </summary>
        tail_divide_Leave,
        /// <summary>
        /// &quot;%&quot;
        /// </summary>
        tail_percent_Leave,
        /// <summary>
        /// &quot;float&quot;
        /// </summary>
        tail_floatLeave,
        /// <summary>
        /// &quot;double&quot;
        /// </summary>
        tail_doubleLeave,
        /// <summary>
        /// &quot;int&quot;
        /// </summary>
        tail_intLeave,
        /// <summary>
        /// &quot;uint&quot;
        /// </summary>
        tail_uintLeave,
        /// <summary>
        /// &quot;bool&quot;
        /// </summary>
        tail_boolLeave,
        /// <summary>
        /// &quot;vec2&quot;
        /// </summary>
        tail_vec2Leave,
        /// <summary>
        /// &quot;vec3&quot;
        /// </summary>
        tail_vec3Leave,
        /// <summary>
        /// &quot;vec4&quot;
        /// </summary>
        tail_vec4Leave,
        /// <summary>
        /// &quot;dvec2&quot;
        /// </summary>
        tail_dvec2Leave,
        /// <summary>
        /// &quot;dvec3&quot;
        /// </summary>
        tail_dvec3Leave,
        /// <summary>
        /// &quot;dvec4&quot;
        /// </summary>
        tail_dvec4Leave,
        /// <summary>
        /// &quot;bvec2&quot;
        /// </summary>
        tail_bvec2Leave,
        /// <summary>
        /// &quot;bvec3&quot;
        /// </summary>
        tail_bvec3Leave,
        /// <summary>
        /// &quot;bvec4&quot;
        /// </summary>
        tail_bvec4Leave,
        /// <summary>
        /// &quot;ivec2&quot;
        /// </summary>
        tail_ivec2Leave,
        /// <summary>
        /// &quot;ivec3&quot;
        /// </summary>
        tail_ivec3Leave,
        /// <summary>
        /// &quot;ivec4&quot;
        /// </summary>
        tail_ivec4Leave,
        /// <summary>
        /// &quot;uvec2&quot;
        /// </summary>
        tail_uvec2Leave,
        /// <summary>
        /// &quot;uvec3&quot;
        /// </summary>
        tail_uvec3Leave,
        /// <summary>
        /// &quot;uvec4&quot;
        /// </summary>
        tail_uvec4Leave,
        /// <summary>
        /// &quot;mat2&quot;
        /// </summary>
        tail_mat2Leave,
        /// <summary>
        /// &quot;mat3&quot;
        /// </summary>
        tail_mat3Leave,
        /// <summary>
        /// &quot;mat4&quot;
        /// </summary>
        tail_mat4Leave,
        /// <summary>
        /// &quot;mat2x2&quot;
        /// </summary>
        tail_mat2x2Leave,
        /// <summary>
        /// &quot;mat2x3&quot;
        /// </summary>
        tail_mat2x3Leave,
        /// <summary>
        /// &quot;mat2x4&quot;
        /// </summary>
        tail_mat2x4Leave,
        /// <summary>
        /// &quot;mat3x2&quot;
        /// </summary>
        tail_mat3x2Leave,
        /// <summary>
        /// &quot;mat3x3&quot;
        /// </summary>
        tail_mat3x3Leave,
        /// <summary>
        /// &quot;mat3x4&quot;
        /// </summary>
        tail_mat3x4Leave,
        /// <summary>
        /// &quot;mat4x2&quot;
        /// </summary>
        tail_mat4x2Leave,
        /// <summary>
        /// &quot;mat4x3&quot;
        /// </summary>
        tail_mat4x3Leave,
        /// <summary>
        /// &quot;mat4x4&quot;
        /// </summary>
        tail_mat4x4Leave,
        /// <summary>
        /// &quot;dmat2&quot;
        /// </summary>
        tail_dmat2Leave,
        /// <summary>
        /// &quot;dmat3&quot;
        /// </summary>
        tail_dmat3Leave,
        /// <summary>
        /// &quot;dmat4&quot;
        /// </summary>
        tail_dmat4Leave,
        /// <summary>
        /// &quot;dmat2x2&quot;
        /// </summary>
        tail_dmat2x2Leave,
        /// <summary>
        /// &quot;dmat2x3&quot;
        /// </summary>
        tail_dmat2x3Leave,
        /// <summary>
        /// &quot;dmat2x4&quot;
        /// </summary>
        tail_dmat2x4Leave,
        /// <summary>
        /// &quot;dmat3x2&quot;
        /// </summary>
        tail_dmat3x2Leave,
        /// <summary>
        /// &quot;dmat3x3&quot;
        /// </summary>
        tail_dmat3x3Leave,
        /// <summary>
        /// &quot;dmat3x4&quot;
        /// </summary>
        tail_dmat3x4Leave,
        /// <summary>
        /// &quot;dmat4x2&quot;
        /// </summary>
        tail_dmat4x2Leave,
        /// <summary>
        /// &quot;dmat4x3&quot;
        /// </summary>
        tail_dmat4x3Leave,
        /// <summary>
        /// &quot;dmat4x4&quot;
        /// </summary>
        tail_dmat4x4Leave,
        /// <summary>
        /// &quot;atomic_uint&quot;
        /// </summary>
        tail_atomic_uintLeave,
        /// <summary>
        /// &quot;sampler1D&quot;
        /// </summary>
        tail_sampler1DLeave,
        /// <summary>
        /// &quot;sampler2D&quot;
        /// </summary>
        tail_sampler2DLeave,
        /// <summary>
        /// &quot;sampler3D&quot;
        /// </summary>
        tail_sampler3DLeave,
        /// <summary>
        /// &quot;samplerCube&quot;
        /// </summary>
        tail_samplerCubeLeave,
        /// <summary>
        /// &quot;sampler1DShadow&quot;
        /// </summary>
        tail_sampler1DShadowLeave,
        /// <summary>
        /// &quot;sampler2DShadow&quot;
        /// </summary>
        tail_sampler2DShadowLeave,
        /// <summary>
        /// &quot;samplerCubeShadow&quot;
        /// </summary>
        tail_samplerCubeShadowLeave,
        /// <summary>
        /// &quot;sampler1DArray&quot;
        /// </summary>
        tail_sampler1DArrayLeave,
        /// <summary>
        /// &quot;sampler2DArray&quot;
        /// </summary>
        tail_sampler2DArrayLeave,
        /// <summary>
        /// &quot;sampler1DArrayShadow&quot;
        /// </summary>
        tail_sampler1DArrayShadowLeave,
        /// <summary>
        /// &quot;sampler2DArrayShadow&quot;
        /// </summary>
        tail_sampler2DArrayShadowLeave,
        /// <summary>
        /// &quot;samplerCubeArray&quot;
        /// </summary>
        tail_samplerCubeArrayLeave,
        /// <summary>
        /// &quot;samplerCubeArrayShadow&quot;
        /// </summary>
        tail_samplerCubeArrayShadowLeave,
        /// <summary>
        /// &quot;isampler1D&quot;
        /// </summary>
        tail_isampler1DLeave,
        /// <summary>
        /// &quot;isampler2D&quot;
        /// </summary>
        tail_isampler2DLeave,
        /// <summary>
        /// &quot;isampler3D&quot;
        /// </summary>
        tail_isampler3DLeave,
        /// <summary>
        /// &quot;isamplerCube&quot;
        /// </summary>
        tail_isamplerCubeLeave,
        /// <summary>
        /// &quot;isampler1DArray&quot;
        /// </summary>
        tail_isampler1DArrayLeave,
        /// <summary>
        /// &quot;isampler2DArray&quot;
        /// </summary>
        tail_isampler2DArrayLeave,
        /// <summary>
        /// &quot;isamplerCubeArray&quot;
        /// </summary>
        tail_isamplerCubeArrayLeave,
        /// <summary>
        /// &quot;usampler1D&quot;
        /// </summary>
        tail_usampler1DLeave,
        /// <summary>
        /// &quot;usampler2D&quot;
        /// </summary>
        tail_usampler2DLeave,
        /// <summary>
        /// &quot;usampler3D&quot;
        /// </summary>
        tail_usampler3DLeave,
        /// <summary>
        /// &quot;usamplerCube&quot;
        /// </summary>
        tail_usamplerCubeLeave,
        /// <summary>
        /// &quot;usampler1DArray&quot;
        /// </summary>
        tail_usampler1DArrayLeave,
        /// <summary>
        /// &quot;usampler2DArray&quot;
        /// </summary>
        tail_usampler2DArrayLeave,
        /// <summary>
        /// &quot;usamplerCubeArray&quot;
        /// </summary>
        tail_usamplerCubeArrayLeave,
        /// <summary>
        /// &quot;sampler2DRect&quot;
        /// </summary>
        tail_sampler2DRectLeave,
        /// <summary>
        /// &quot;sampler2DRectShadow&quot;
        /// </summary>
        tail_sampler2DRectShadowLeave,
        /// <summary>
        /// &quot;isampler2DRect&quot;
        /// </summary>
        tail_isampler2DRectLeave,
        /// <summary>
        /// &quot;usampler2DRect&quot;
        /// </summary>
        tail_usampler2DRectLeave,
        /// <summary>
        /// &quot;samplerBuffer&quot;
        /// </summary>
        tail_samplerBufferLeave,
        /// <summary>
        /// &quot;isamplerBuffer&quot;
        /// </summary>
        tail_isamplerBufferLeave,
        /// <summary>
        /// &quot;usamplerBuffer&quot;
        /// </summary>
        tail_usamplerBufferLeave,
        /// <summary>
        /// &quot;sampler2DMS&quot;
        /// </summary>
        tail_sampler2DMSLeave,
        /// <summary>
        /// &quot;isampler2DMS&quot;
        /// </summary>
        tail_isampler2DMSLeave,
        /// <summary>
        /// &quot;usampler2DMS&quot;
        /// </summary>
        tail_usampler2DMSLeave,
        /// <summary>
        /// &quot;sampler2DMSArray&quot;
        /// </summary>
        tail_sampler2DMSArrayLeave,
        /// <summary>
        /// &quot;isampler2DMSArray&quot;
        /// </summary>
        tail_isampler2DMSArrayLeave,
        /// <summary>
        /// &quot;usampler2DMSArray&quot;
        /// </summary>
        tail_usampler2DMSArrayLeave,
        /// <summary>
        /// &quot;image1D&quot;
        /// </summary>
        tail_image1DLeave,
        /// <summary>
        /// &quot;iimage1D&quot;
        /// </summary>
        tail_iimage1DLeave,
        /// <summary>
        /// &quot;uimage1D&quot;
        /// </summary>
        tail_uimage1DLeave,
        /// <summary>
        /// &quot;image2D&quot;
        /// </summary>
        tail_image2DLeave,
        /// <summary>
        /// &quot;iimage2D&quot;
        /// </summary>
        tail_iimage2DLeave,
        /// <summary>
        /// &quot;uimage2D&quot;
        /// </summary>
        tail_uimage2DLeave,
        /// <summary>
        /// &quot;image3D&quot;
        /// </summary>
        tail_image3DLeave,
        /// <summary>
        /// &quot;iimage3D&quot;
        /// </summary>
        tail_iimage3DLeave,
        /// <summary>
        /// &quot;uimage3D&quot;
        /// </summary>
        tail_uimage3DLeave,
        /// <summary>
        /// &quot;image2DRect&quot;
        /// </summary>
        tail_image2DRectLeave,
        /// <summary>
        /// &quot;iimage2DRect&quot;
        /// </summary>
        tail_iimage2DRectLeave,
        /// <summary>
        /// &quot;uimage2DRect&quot;
        /// </summary>
        tail_uimage2DRectLeave,
        /// <summary>
        /// &quot;imageCube&quot;
        /// </summary>
        tail_imageCubeLeave,
        /// <summary>
        /// &quot;iimageCube&quot;
        /// </summary>
        tail_iimageCubeLeave,
        /// <summary>
        /// &quot;uimageCube&quot;
        /// </summary>
        tail_uimageCubeLeave,
        /// <summary>
        /// &quot;imageBuffer&quot;
        /// </summary>
        tail_imageBufferLeave,
        /// <summary>
        /// &quot;iimageBuffer&quot;
        /// </summary>
        tail_iimageBufferLeave,
        /// <summary>
        /// &quot;uimageBuffer&quot;
        /// </summary>
        tail_uimageBufferLeave,
        /// <summary>
        /// &quot;image1DArray&quot;
        /// </summary>
        tail_image1DArrayLeave,
        /// <summary>
        /// &quot;iimage1DArray&quot;
        /// </summary>
        tail_iimage1DArrayLeave,
        /// <summary>
        /// &quot;uimage1DArray&quot;
        /// </summary>
        tail_uimage1DArrayLeave,
        /// <summary>
        /// &quot;image2DArray&quot;
        /// </summary>
        tail_image2DArrayLeave,
        /// <summary>
        /// &quot;iimage2DArray&quot;
        /// </summary>
        tail_iimage2DArrayLeave,
        /// <summary>
        /// &quot;uimage2DArray&quot;
        /// </summary>
        tail_uimage2DArrayLeave,
        /// <summary>
        /// &quot;imageCubeArray&quot;
        /// </summary>
        tail_imageCubeArrayLeave,
        /// <summary>
        /// &quot;iimageCubeArray&quot;
        /// </summary>
        tail_iimageCubeArrayLeave,
        /// <summary>
        /// &quot;uimageCubeArray&quot;
        /// </summary>
        tail_uimageCubeArrayLeave,
        /// <summary>
        /// &quot;image2DMS&quot;
        /// </summary>
        tail_image2DMSLeave,
        /// <summary>
        /// &quot;iimage2DMS&quot;
        /// </summary>
        tail_iimage2DMSLeave,
        /// <summary>
        /// &quot;uimage2DMS&quot;
        /// </summary>
        tail_uimage2DMSLeave,
        /// <summary>
        /// &quot;image2DMSArray&quot;
        /// </summary>
        tail_image2DMSArrayLeave,
        /// <summary>
        /// &quot;iimage2DMSArray&quot;
        /// </summary>
        tail_iimage2DMSArrayLeave,
        /// <summary>
        /// &quot;uimage2DMSArray&quot;
        /// </summary>
        tail_uimage2DMSArrayLeave,
        /// <summary>
        /// &quot;struct&quot;
        /// </summary>
        tail_structLeave,
        /// <summary>
        /// &quot;const&quot;
        /// </summary>
        tail_constLeave,
        /// <summary>
        /// &quot;inout&quot;
        /// </summary>
        tail_inoutLeave,
        /// <summary>
        /// &quot;in&quot;
        /// </summary>
        tail_inLeave,
        /// <summary>
        /// &quot;out&quot;
        /// </summary>
        tail_outLeave,
        /// <summary>
        /// &quot;centroid&quot;
        /// </summary>
        tail_centroidLeave,
        /// <summary>
        /// &quot;patch&quot;
        /// </summary>
        tail_patchLeave,
        /// <summary>
        /// &quot;sample&quot;
        /// </summary>
        tail_sampleLeave,
        /// <summary>
        /// &quot;uniform&quot;
        /// </summary>
        tail_uniformLeave,
        /// <summary>
        /// &quot;buffer&quot;
        /// </summary>
        tail_bufferLeave,
        /// <summary>
        /// &quot;shared&quot;
        /// </summary>
        tail_sharedLeave,
        /// <summary>
        /// &quot;coherent&quot;
        /// </summary>
        tail_coherentLeave,
        /// <summary>
        /// &quot;volatile&quot;
        /// </summary>
        tail_volatileLeave,
        /// <summary>
        /// &quot;restrict&quot;
        /// </summary>
        tail_restrictLeave,
        /// <summary>
        /// &quot;readonly&quot;
        /// </summary>
        tail_readonlyLeave,
        /// <summary>
        /// &quot;writeonly&quot;
        /// </summary>
        tail_writeonlyLeave,
        /// <summary>
        /// &quot;subroutine&quot;
        /// </summary>
        tail_subroutineLeave,
        /// <summary>
        /// &quot;layout&quot;
        /// </summary>
        tail_layoutLeave,
        /// <summary>
        /// &quot;high_precision&quot;
        /// </summary>
        tail_high_precisionLeave,
        /// <summary>
        /// &quot;medium_precision&quot;
        /// </summary>
        tail_medium_precisionLeave,
        /// <summary>
        /// &quot;low_precision&quot;
        /// </summary>
        tail_low_precisionLeave,
        /// <summary>
        /// &quot;smooth&quot;
        /// </summary>
        tail_smoothLeave,
        /// <summary>
        /// &quot;flat&quot;
        /// </summary>
        tail_flatLeave,
        /// <summary>
        /// &quot;noperspective&quot;
        /// </summary>
        tail_noperspectiveLeave,
        /// <summary>
        /// &quot;invariant&quot;
        /// </summary>
        tail_invariantLeave,
        /// <summary>
        /// &quot;precise&quot;
        /// </summary>
        tail_preciseLeave,
        /// <summary>
        /// #
        /// </summary>
        tail_startEndLeave,
    }

}

