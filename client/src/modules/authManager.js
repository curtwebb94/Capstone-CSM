import firebase from "firebase/app";
import "firebase/auth";

const _apiUrl = "/api/userprofile";

const _doesUserExist = (firebaseUserId) => {
    return getToken().then((token) =>
        fetch(`${_apiUrl}/DoesUserExist/${firebaseUserId}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        }).then(resp => resp.ok));
};

// Function to get the user's ID based on the current user's Firebase ID
export const getUserIdByFirebaseId = async (firebaseId) => {
    try {
      // Initialize Firebase Firestore
      const firestore = firebase.firestore();
  
      // Query the "users" collection for the user with the matching "firebaseUserId"
      const querySnapshot = await firestore.collection('users').where('firebaseUserId', '==', firebaseId).get();
  
      // If there's a matching user, return their user ID
      if (!querySnapshot.empty) {
        const user = querySnapshot.docs[0].data();
        return user.id;
      } else {
        // If no matching user is found, return null or an error message as desired
        return null;
      }
    } catch (error) {
      console.error('Error getting user ID:', error);
      // Handle the error appropriately, e.g., return null or throw an error
      return null;
    }
  };

const _saveUser = (userProfile) => {
    return getToken().then((token) =>
        fetch(_apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userProfile)
        }).then(resp => resp.json()));
};



export const getToken = () => {
    const user = firebase.auth().currentUser;
    if (user) {
      return user.getIdToken();
    } else {
      // Return a default or empty value when the user is not logged in
      return Promise.resolve(null); // You can change this to return any default value you prefer
    }
  };

export const login = (email, pw) => {
    return firebase.auth().signInWithEmailAndPassword(email, pw)
        .then((signInResponse) => _doesUserExist(signInResponse.user.uid))
        .then((doesUserExist) => {
            if (!doesUserExist) {

                // If we couldn't find the user in our app's database, we should logout of firebase
                logout();

                throw new Error("Something's wrong. The user exists in firebase, but not in the application database.");
            }
        }).catch(err => {
            console.error(err);
            throw err;
        });
};


export const logout = () => {
    firebase.auth().signOut()
};


export const register = (userProfile, password) => {
    return firebase.auth().createUserWithEmailAndPassword(userProfile.email, password)
        .then((createResponse) => _saveUser({
            ...userProfile,
            firebaseUserId: createResponse.user.uid
        }));
};


export const onLoginStatusChange = (onLoginStatusChangeHandler) => {
    firebase.auth().onAuthStateChanged((user) => {
        onLoginStatusChangeHandler(!!user);
    });
};
