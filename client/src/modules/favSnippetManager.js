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

export const SaveFavSnippet = (snippet) => {
  return getToken().then((token) => {
    return fetch(apiUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(snippet),
    }).then((res) => {
      if (res.ok) {
        return res.json();
      } else {
        throw new Error("An unknown error occurred while creating the result.");
      }
    });
  });
};