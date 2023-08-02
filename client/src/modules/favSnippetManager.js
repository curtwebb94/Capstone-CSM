import { getToken } from "./authManager";

const apiUrl = "/api/FavoriteSnippet";

// SHOW LIST BY USER ID

export const getFavSnippetsByUserId = (id) => {
  return getToken().then((token) => {
    return fetch(`${apiUrl}/user/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get products.",
        );
      }
    });
  });
};

export const getFavSnippetsByFirebaseId = (uid) => {
  return getToken().then((token) => {
    return fetch(`${apiUrl}/user/${uid}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get products.",
        );
      }
    });
  });
};

// SAVE SNIPPETS

// favSnippetManager.js

// favSnippetManager.js

// Pass the `action` parameter to determine the action (save or delete)
export const SaveFavSnippet = (favoriteSnippetData, action) => {
  return getToken().then((token) => {
    let url;
    let method;

    if (action === "save") {
      url = `${apiUrl}`;
      method = "POST";
    } else if (action === "delete") {
      url = `${apiUrl}/${favoriteSnippetData.id}`;
      method = "DELETE";
    } else {
      throw new Error("Invalid action. Action must be 'save' or 'delete'.");
    }

    return fetch(url, {
      method: method,
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(favoriteSnippetData),
    })
      .then((resp) => {
        if (!resp.ok) {
          throw new Error("Failed to save/delete snippet as favorite.");
        }
        // Check if response contains JSON data before parsing
        return resp.text().then((text) => (text ? JSON.parse(text) : null));
      })
      .catch((error) => {
        console.error("Error saving/deleting snippet as favorite:", error);
        throw error;
      });
  });
};


export const DeleteFavSnippet = (id) => {
  return getToken().then((token) => {
      return fetch(apiUrl + `/${id}`, {
          method: "DELETE",
          headers: {
              Authorization: `Bearer ${token}`,
              "Content-Type": "application/json",
          },
      })
  });
};
