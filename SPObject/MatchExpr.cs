using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.PatternMatching;

namespace SPObj
{
    class MatchExpr
    {
        AllPatterns Pat = new AllPatterns();
        static string VARSECLSTMT = "VariableDeclarationStatement";
        static string EXPRSTMT = "ExpressionStatement";

        public void SearchInvocationToAdd(Solution solution)
        {
            foreach (var file in solution.AllFiles)
            {
                var astResolver = new CSharpAstResolver(file.Project.Compilation, file.SyntaxTree, file.UnresolvedTypeSystemForFile);
                foreach (var invocation in file.SyntaxTree.Descendants.OfType<AstNode>())
                {
                    string invName = invocation.GetType().Name;
                    if (invName == VARSECLSTMT || invName == EXPRSTMT)
                        MatchInvocation(invocation, file, astResolver);
                }
            }
        }


        public void MatchInvocation(AstNode invocation, CSharpFile file, CSharpAstResolver astResolver)
        {
            if (Pat.sqlCmdstmt1().Match(invocation).Success || Pat.sqlCmdstmt2().Match(invocation).Success)
            {
                file.IndexOfVarDeclStmt.Add((VariableDeclarationStatement)invocation);
            }
            else if (Pat.SqlCmdExprStmt1().Match(invocation).Success || Pat.SqlCmdExprStmt2().Match(invocation).Success)
            {
                file.IndexOfExprStmt.Add((ExpressionStatement)invocation);
            }
        }
    }
}
