import firebase from "firebase/app";
import "firebase/auth";

const _apiUrl = "/api/userprofile";

const _doesUserExist = async (firebaseUserId) => {
    try {
        const token = await getToken();
        const response = await fetch(`${_apiUrl}/DoesUserExist/${firebaseUserId}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
        return response.ok;
    } catch (error) {
        console.error('Error checking user existence:', error);
        return false;
    }
};

export const getUserIdByFirebaseId = async (firebaseId) => {
    try {
        const firestore = firebase.firestore();
        const querySnapshot = await firestore.collection('users').where('firebaseUserId', '==', firebaseId).get();
        if (!querySnapshot.empty) {
            const user = querySnapshot.docs[0].data();
            return user.id;
        } else {
            return null;
        }
    } catch (error) {
        console.error('Error getting user ID:', error);
        return null;
    }
};

const _saveUser = async (userProfile) => {
    try {
        const token = await getToken();
        const response = await fetch(_apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(userProfile)
        });
        return response.json();
    } catch (error) {
        console.error('Error saving user:', error);
        return null;
    }
};

export const getToken = () => {
    const user = firebase.auth().currentUser;
    if (user) {
        return user.getIdToken();
    } else {
        return Promise.resolve(null);
    }
};

export const login = (email, pw) => {
    return firebase.auth().signInWithEmailAndPassword(email, pw)
        .then((signInResponse) => _doesUserExist(signInResponse.user.uid))
        .then((doesUserExist) => {
            if (!doesUserExist) {
                logout();
                throw new Error("User exists in firebase but not in the application database.");
            }
        }).catch(err => {
            console.error(err);
            throw err;
        });
};

export const logout = () => {
    firebase.auth().signOut();
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
