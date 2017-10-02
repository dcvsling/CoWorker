namespace CoWorker.Abstractions.Expressions
{
    using System;
    using System.Linq.Expressions;

    public interface IExpressionVisitor
    {
        Func<Expression, Expression> Visit { get; set; }
        Func<CatchBlock,CatchBlock> VisitCatchBlock{ get; set; }
        Func<ElementInit,ElementInit> VisitElementInit{ get; set; }
        Func<LabelTarget,LabelTarget> VisitLabelTarget{ get; set; }
        Func<MemberAssignment,MemberAssignment> VisitMemberAssignment{ get; set; }
        Func<MemberBinding,MemberBinding> VisitMemberBinding{ get; set; }
        Func<MemberListBinding,MemberListBinding> VisitMemberListBinding{ get; set; }
        Func<MemberMemberBinding,MemberMemberBinding> VisitMemberMemberBinding{ get; set; }
        Func<SwitchCase,SwitchCase> VisitSwitchCase{ get; set; }
        Func<BinaryExpression,Expression> VisitBinary{ get; set; }
        Func<BlockExpression,Expression> VisitBlock{ get; set; }
        Func<ConditionalExpression,Expression> VisitConditional{ get; set; }
        Func<ConstantExpression,Expression> VisitConstant{ get; set; }
        Func<DebugInfoExpression,Expression> VisitDebugInfo{ get; set; }
        Func<DefaultExpression,Expression> VisitDefault{ get; set; }
        Func<Expression,Expression> VisitExtension{ get; set; }
        Func<GotoExpression,Expression> VisitGoto{ get; set; }
        Func<IndexExpression,Expression> VisitIndex{ get; set; }
        Func<InvocationExpression,Expression> VisitInvocation{ get; set; }
        Func<LabelExpression,Expression> VisitLabel{ get; set; }
        Func<ListInitExpression,Expression> VisitListInit{ get; set; }
        Func<LoopExpression,Expression> VisitLoop{ get; set; }
        Func<MemberExpression,Expression> VisitMember{ get; set; }
        Func<MemberInitExpression,Expression> VisitMemberInit{ get; set; }
        Func<MethodCallExpression,Expression> VisitMethodCall{ get; set; }
        Func<NewExpression,Expression> VisitNew{ get; set; }
        Func<NewArrayExpression,Expression> VisitNewArray{ get; set; }
        Func<ParameterExpression,Expression> VisitParameter{ get; set; }
        Func<RuntimeVariablesExpression,Expression> VisitRuntimeVariables{ get; set; }
        Func<SwitchExpression,Expression> VisitSwitch{ get; set; }
        Func<TryExpression,Expression> VisitTry{ get; set; }
        Func<TypeBinaryExpression,Expression> VisitTypeBinary{ get; set; }
        Func<UnaryExpression,Expression> VisitUnary{ get; set; }
        Func<LambdaExpression,Expression> VisitLambdaNonGeneric { get; set; }
    }
}
