using System.Collections.Generic;
using CSM.Models;

namespace CSM.Repositories
{
    public interface ICodeSnippetTagRepository
    {
        List<CodeSnippetTag> GetAllCodeSnippetTags();
        CodeSnippetTag GetCodeSnippetTagById(int id);
        void AddCodeSnippetTag(CodeSnippetTag codeSnippetTag);
        void UpdateCodeSnippetTag(CodeSnippetTag codeSnippetTag);
        void DeleteCodeSnippetTag(int id);
    }
}
