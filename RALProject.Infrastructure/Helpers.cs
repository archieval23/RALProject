using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Infrastructure
{
    public class Helpers
    {
        public static Func<TTarget, bool> ConvertPredicate<TSource, TTarget>(Expression<Func<TSource, bool>> root)
        {
            var visitor = new ParameterTypeVisitor<TSource, TTarget>();
            var expression = (Expression<Func<TTarget, bool>>)visitor.Visit(root);
            return expression.Compile();
        }
    }

    public class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
    {
        private ReadOnlyCollection<ParameterExpression> _parameters;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters != null)
                return _parameters.FirstOrDefault(p => p.Name == node.Name);
            else
                return node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _parameters = VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda");
            return Expression.Lambda(Visit(node.Body), _parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TSource))
            {
                return Expression.Property(Visit(node.Expression), node.Member.Name);
            }
            return base.VisitMember(node);
        }
    }
}
