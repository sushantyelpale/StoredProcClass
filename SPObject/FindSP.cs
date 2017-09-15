using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.Editor;
using ICSharpCode.NRefactory.PatternMatching;

namespace SPObj
{
    class FindSP
    {
        AllPatterns Pat = new AllPatterns();

        public void CheckforAnySP(Solution solution, Dictionary<string, List<string>> SpNameClassNameDict)
        {
            foreach (var file in solution.AllFiles)
            {
                if (file.IndexOfExprStmt.Count == 0 && file.IndexOfVarDeclStmt.Count == 0)
                    continue;
                file.SyntaxTree.Freeze();
                
                var document = new StringBuilderDocument(file.OriginalText);
                var formattingOptions = FormattingOptionsFactory.CreateAllman();
                var options = new TextEditorOptions();
                using (var script = new DocumentScript(document, formattingOptions, options))
                {
                    foreach (ExpressionStatement expr in file.IndexOfExprStmt)
                    {
                        var copy = (ExpressionStatement)expr.Clone();

                        string varName = expr.GetText().Split("\"\"".ToCharArray())[1];
                        string output = Regex.Replace(varName, @"\w+\.\b", "");
                        string ExprClass = (expr.GetParent<TypeDeclaration>()).Name;
                        
                        if (!SpNameClassNameDict.ContainsKey(output))
                        {
                            SpNameClassNameDict.Add(output, new List<string>());
                            SpNameClassNameDict[output].Add(ExprClass);
                        }
                        else if (!SpNameClassNameDict[output].Contains(ExprClass))
                            SpNameClassNameDict[output].Add(ExprClass);
                    }

                    foreach (VariableDeclarationStatement expr in file.IndexOfVarDeclStmt)
                    {
                        var copy = (VariableDeclarationStatement)expr.Clone();
                        string varName = expr.GetText().Split("\"\"".ToCharArray())[1];
                        string output = Regex.Replace(varName, @"\w+\.\b", "");
                        string ExprClass = (expr.GetParent<TypeDeclaration>()).Name;
                        
                        if (!SpNameClassNameDict.ContainsKey(output))
                        {
                            SpNameClassNameDict.Add(output, new List<string>());
                            SpNameClassNameDict[output].Add(ExprClass);
                        }
                        else if (!SpNameClassNameDict[output].Contains(ExprClass))
                            SpNameClassNameDict[output].Add(ExprClass);
                    }
                }
                File.WriteAllText(Path.ChangeExtension(file.FileName, ".output.cs"), document.Text);
            }
        }
    }
}
