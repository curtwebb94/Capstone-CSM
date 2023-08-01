using CSM.Models;
using System.Collections.Generic;

namespace CSM.Repositories
{
    public interface IFavoriteSnippetRepository
    {
        List<FavoriteSnippet> GetAllFavoriteSnippets();
        FavoriteSnippet GetFavoriteSnippetById(int id);
        void AddFavoriteSnippet(FavoriteSnippet favoriteSnippet);
        void DeleteFavoriteSnippet(int id);
        void UpdateFavoriteSnippet(FavoriteSnippet favoriteSnippet);
        List<FavoriteSnippet> GetFavoriteSnippetsByUserId(int userId);
        List<FavoriteSnippet> GetFavoriteSnippetsByFirebaseId(string firebaseId);

    }
}
