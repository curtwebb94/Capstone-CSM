import { getToken } from "./authManager";

const baseUrl = "/api/UserProfile";
const fbUrl = "/api/UserProfile/firebase"

export const getAllUsers = () => {
  return getToken().then(token => {
    return fetch(baseUrl, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      }
    })
      .then(res => res.json())
  })
};

export const getUserByFirebaseId = (uid) => {
  return getToken().then(token => {
    return fetch(`${fbUrl}/${uid}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`
      }
    })
      .then(res => res.json())
  })
}

// Define the updateUser function
export const updateUser = (userObj) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${userObj.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        id: userObj.id,
        username: userObj.username,
        password: userObj.password,
      }),
    })
      .then((res) => res.json())
      .catch((error) => {
        console.error("Error updating user:", error);
        throw error;
      });
  });
};