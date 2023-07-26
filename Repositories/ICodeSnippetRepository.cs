using System.Collections.Generic;
using CSM.Models;

namespace CSM.Repositories
{
    public interface ICodeSnippetRepository
    {
        List<CodeSnippet> GetAllCodeSnippets();
        CodeSnippet GetCodeSnippetById(int id);
        void AddCodeSnippet(CodeSnippet codeSnippet);
        void DeleteCodeSnippet(int id);
        void EditCodeSnippet(CodeSnippet codeSnippet);
    }
}
