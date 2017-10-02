namespace CoWorker.Abstractions.Expressions
{
    using System;
    using System.Linq.Expressions;

    public class ExpressionVisitorTemplate : ExpressionVisitor,IExpressionVisitor
    {
        public IExpressionVisitor Clone => new ExpressionVisitorTemplate(this);

        private IExpressionVisitor visitor;
        public ExpressionVisitorTemplate(Action<ExpressionVisitorTemplate> config = null)
        {
            config?.Invoke(this);
            this.visitor = this as IExpressionVisitor;
        }

        private ExpressionVisitorTemplate(IExpressionVisitor visitor)
        {
            this.visitor = visitor ?? this as IExpressionVisitor;
        }

        #region Middle Property
        Func<Expression, Expression> IExpressionVisitor.Visit { get; set; }
        Func<CatchBlock, CatchBlock> IExpressionVisitor.VisitCatchBlock { get; set; }
        Func<ElementInit, ElementInit> IExpressionVisitor.VisitElementInit { get; set; }
        Func<LabelTarget, LabelTarget> IExpressionVisitor.VisitLabelTarget { get; set; }
        Func<MemberAssignment, MemberAssignment> IExpressionVisitor.VisitMemberAssignment { get; set; }
        Func<MemberBinding, MemberBinding> IExpressionVisitor.VisitMemberBinding { get; set; }
        Func<MemberListBinding, MemberListBinding> IExpressionVisitor.VisitMemberListBinding { get; set; }
        Func<MemberMemberBinding, MemberMemberBinding> IExpressionVisitor.VisitMemberMemberBinding { get; set; }
        Func<SwitchCase, SwitchCase> IExpressionVisitor.VisitSwitchCase { get; set; }
        Func<BinaryExpression, Expression> IExpressionVisitor.VisitBinary { get; set; }
        Func<BlockExpression, Expression> IExpressionVisitor.VisitBlock { get; set; }
        Func<ConditionalExpression, Expression> IExpressionVisitor.VisitConditional { get; set; }
        Func<ConstantExpression, Expression> IExpressionVisitor.VisitConstant { get; set; }
        Func<DebugInfoExpression, Expression> IExpressionVisitor.VisitDebugInfo { get; set; }
        Func<DefaultExpression, Expression> IExpressionVisitor.VisitDefault { get; set; }
        Func<Expression, Expression> IExpressionVisitor.VisitExtension { get; set; }
        Func<GotoExpression, Expression> IExpressionVisitor.VisitGoto { get; set; }
        Func<IndexExpression, Expression> IExpressionVisitor.VisitIndex { get; set; }
        Func<InvocationExpression, Expression> IExpressionVisitor.VisitInvocation { get; set; }
        Func<LabelExpression, Expression> IExpressionVisitor.VisitLabel { get; set; }
        Func<ListInitExpression, Expression> IExpressionVisitor.VisitListInit { get; set; }
        Func<LoopExpression, Expression> IExpressionVisitor.VisitLoop { get; set; }
        Func<MemberExpression, Expression> IExpressionVisitor.VisitMember { get; set; }
        Func<MemberInitExpression, Expression> IExpressionVisitor.VisitMemberInit { get; set; }
        Func<MethodCallExpression, Expression> IExpressionVisitor.VisitMethodCall { get; set; }
        Func<NewExpression, Expression> IExpressionVisitor.VisitNew { get; set; }
        Func<NewArrayExpression, Expression> IExpressionVisitor.VisitNewArray { get; set; }
        Func<ParameterExpression, Expression> IExpressionVisitor.VisitParameter { get; set; }
        Func<RuntimeVariablesExpression, Expression> IExpressionVisitor.VisitRuntimeVariables { get; set; }
        Func<SwitchExpression, Expression> IExpressionVisitor.VisitSwitch { get; set; }
        Func<TryExpression, Expression> IExpressionVisitor.VisitTry { get; set; }
        Func<TypeBinaryExpression, Expression> IExpressionVisitor.VisitTypeBinary { get; set; }
        Func<UnaryExpression, Expression> IExpressionVisitor.VisitUnary { get; set; }
        Func<LambdaExpression, Expression> IExpressionVisitor.VisitLambdaNonGeneric { get; set; }
        #endregion

        #region Expression Visitor Implements
        public override Expression Visit(Expression node)
        {
            if (visitor.Visit != null && node != null)
                node = visitor.Visit(node);
            return base.Visit(node);
        }
        protected new CatchBlock VisitCatchBlock(CatchBlock node)
            => (visitor.VisitCatchBlock ?? base.VisitCatchBlock)(node);
        protected new ElementInit VisitElementInit(ElementInit node)
            => (visitor.VisitElementInit ?? base.VisitElementInit)(node);
        protected new LabelTarget VisitLabelTarget(LabelTarget node)
            => (visitor.VisitLabelTarget ?? base.VisitLabelTarget)(node);
        protected new MemberAssignment VisitMemberAssignment(MemberAssignment node)
            => (visitor.VisitMemberAssignment ?? base.VisitMemberAssignment)(node);
        protected new MemberBinding VisitMemberBinding(MemberBinding node)
            => (visitor.VisitMemberBinding ?? base.VisitMemberBinding)(node);
        protected new MemberListBinding VisitMemberListBinding(MemberListBinding node)
            => (visitor.VisitMemberListBinding ?? base.VisitMemberListBinding)(node);
        protected new MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
            => (visitor.VisitMemberMemberBinding ?? base.VisitMemberMemberBinding)(node);
        protected new SwitchCase VisitSwitchCase(SwitchCase node)
            => (visitor.VisitSwitchCase ?? base.VisitSwitchCase)(node);
        protected new Expression VisitBinary(BinaryExpression node)
            => (visitor.VisitBinary ?? base.VisitBinary)(node);
        protected new Expression VisitBlock(BlockExpression node)
            => (visitor.VisitBlock ?? base.VisitBlock)(node);
        protected new Expression VisitConditional(ConditionalExpression node)
            => (visitor.VisitConditional ?? base.VisitConditional)(node);
        protected new Expression VisitConstant(ConstantExpression node)
            => (visitor.VisitConstant ?? base.VisitConstant)(node);
        protected new Expression VisitDebugInfo(DebugInfoExpression node)
            => (visitor.VisitDebugInfo ?? base.VisitDebugInfo)(node);
        protected new Expression VisitDefault(DefaultExpression node)
            => (visitor.VisitDefault ?? base.VisitDefault)(node);
        protected new Expression VisitExtension(Expression node)
            => (visitor.VisitExtension ?? base.VisitExtension)(node);
        protected new Expression VisitGoto(GotoExpression node)
            => (visitor.VisitGoto ?? base.VisitGoto)(node);
        protected new Expression VisitIndex(IndexExpression node)
            => (visitor.VisitIndex ?? base.VisitIndex)(node);
        protected new Expression VisitInvocation(InvocationExpression node)
            => (visitor.VisitInvocation ?? base.VisitInvocation)(node);
        protected new Expression VisitLabel(LabelExpression node)
            => (visitor.VisitLabel ?? base.VisitLabel)(node);
        protected new Expression VisitLambda<T>(Expression<T> node)
            => visitor.VisitLambdaNonGeneric?.Invoke(node) ?? base.VisitLambda<T>(node);
        protected new Expression VisitListInit(ListInitExpression node)
            => (visitor.VisitListInit ?? base.VisitListInit)(node);
        protected new Expression VisitLoop(LoopExpression node)
            => (visitor.VisitLoop ?? base.VisitLoop)(node);
        protected new Expression VisitMember(MemberExpression node)
            => (visitor.VisitMember ?? base.VisitMember)(node);
        protected new Expression VisitMemberInit(MemberInitExpression node)
            => (visitor.VisitMemberInit ?? base.VisitMemberInit)(node);
        protected new Expression VisitMethodCall(MethodCallExpression node)
            => (visitor.VisitMethodCall ?? base.VisitMethodCall)(node);
        protected new Expression VisitNew(NewExpression node)
            => (visitor.VisitNew ?? base.VisitNew)(node);
        protected new Expression VisitNewArray(NewArrayExpression node)
            => (visitor.VisitNewArray ?? base.VisitNewArray)(node);
        protected override Expression VisitParameter(ParameterExpression node)
            => (visitor.VisitParameter ?? base.VisitParameter)(node);
        protected new Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
            => (visitor.VisitRuntimeVariables ?? base.VisitRuntimeVariables)(node);
        protected new Expression VisitSwitch(SwitchExpression node)
            => (visitor.VisitSwitch ?? base.VisitSwitch)(node);
        protected new Expression VisitTry(TryExpression node)
            => (visitor.VisitTry ?? base.VisitTry)(node);
        protected new Expression VisitTypeBinary(TypeBinaryExpression node)
            => (visitor.VisitTypeBinary ?? base.VisitTypeBinary)(node);
        protected new Expression VisitUnary(UnaryExpression node)
            => (visitor.VisitUnary ?? base.VisitUnary)(node);
        #endregion
        
    }
}
