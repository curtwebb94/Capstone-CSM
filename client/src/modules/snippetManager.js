import { getToken } from "./authManager";

const apiUrl = "/api/CodeSnippet";

export const getSnippets = () => {
  return getToken().then((token) => {
    return fetch(apiUrl, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get snippets.",
        );
      }
    });
  });
};

export const addSnippet = (snippet) => {
  return getToken().then((token) => {
      return fetch(`${apiUrl}`, {
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
              throw new Error("An unknown error occurred while adding the user to the group.");
          }
      });
  });
};

export const getTags = () => {
  return getToken().then((token) => {
    return fetch(apiUrl, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get tags.",
        );
      }
    });
  });
};

export const getSnippetDetails = (id) => {
  return getToken().then((token) => {
    return fetch(`${apiUrl}/${id}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to get snippet details.",
        );
      }
    });
  })
};



