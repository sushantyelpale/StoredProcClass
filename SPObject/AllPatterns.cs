using System;
using System.Text;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.PatternMatching;

namespace SPObj
{
    class AllPatterns
    {
        // Pattern of variable Decl Stmt having sqlCommand having 2 arguments
        //Replaced by SqlCommand cmd = new SqlCommand("InsertTicket", con);  /argument of any type
        public VariableDeclarationStatement sqlCmdstmt1()
        {
            var expr = new VariableDeclarationStatement
            {
                Type = new SimpleType("SqlCommand"),
                Variables = { 
                    new VariableInitializer(
                        Pattern.AnyString, 
                        new ObjectCreateExpression {
                            Type = new SimpleType("SqlCommand"),
                            Arguments = { new AnyNode("PrimitiveExpression")}
                        })
                }
            };
            return expr;
        }

        // Pattern of variable Decl Stmt having sqlCommand having 2 arguments
        //Replaced by SqlCommand cmd = new SqlCommand("InsertTicket", con);  /argument of any type
        public VariableDeclarationStatement sqlCmdstmt2()
        {
            var expr = new VariableDeclarationStatement
            {
                Type = new SimpleType("SqlCommand"),
                Variables = { 
                    new VariableInitializer(
                        Pattern.AnyString, 
                        new ObjectCreateExpression {
                            Type = new SimpleType("SqlCommand"),
                            Arguments = { 
                                new AnyNode("PrimitiveExpression"),
                                new IdentifierExpression(Pattern.AnyString)
                               }
                        })
                }
            };
            return expr;
        }



        // Pattern of variable Decl Stmt having sqlCommand having 2 arguments
        //Replaced by SqlCommand cmd = new SqlCommand("InsertTicket", con);  /argument of any type
        public ExpressionStatement SqlCmdExprStmt1()
        {
            var expr = new ExpressionStatement
            {
                Expression = new AssignmentExpression{
                    Left = new IdentifierExpression(Pattern.AnyString),
                    Right = new ObjectCreateExpression {
                            Type = new SimpleType("SqlCommand"),
                            Arguments = { new AnyNode("PrimitiveExpression")}
                        }
                }
            };
            return expr;
        }

        


        // Pattern of variable Decl Stmt having sqlCommand having 2 arguments
        //Replaced by SqlCommand cmd = new SqlCommand("InsertTicket", con);  /argument of any type
        public ExpressionStatement SqlCmdExprStmt2()
        {
            var expr = new ExpressionStatement
            {
                Expression = new AssignmentExpression{
                    Left = new IdentifierExpression(Pattern.AnyString),
                    Right = new ObjectCreateExpression {
                            Type = new SimpleType("SqlCommand"),
                            Arguments = { 
                               new AnyNode("PrimitiveExpression"),
                                new IdentifierExpression(Pattern.AnyString) }
                        }
                    }
            };
            return expr;
        }

    }
}
