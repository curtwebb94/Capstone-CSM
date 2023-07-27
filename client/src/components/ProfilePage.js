import React, { useEffect, useState } from 'react';
import './ProfilePage.css'; // Add your custom CSS for the profile page here
import firebase from 'firebase/app';
import 'firebase/auth';
import { Link } from 'react-router-dom';

export default function ProfilePage({ isLoggedIn, handleLogout }) {
    const [user, setUser] = useState(null);

    useEffect(() => {
        // Subscribe to Firebase authentication state changes
        const unsubscribe = firebase.auth().onAuthStateChanged((user) => {
            if (user) {
                setUser({
                    name: user.displayName,
                    email: user.email,
                    password: user.password,
                });
            } else {
                // User is not logged in, set the user state to null
                setUser(null);
            }
        });

        // Clean up the subscription when the component unmounts
        return () => unsubscribe();
    }, []);



    if (!user) {
        // Render the message and button to navigate to the login page if the user is not logged in
        return (
            <div className="profile-page">
                <div className="profile-image">
                    <img src="https://media.istockphoto.com/vectors/what-emoji-shock-emotion-wtf-emoticon-cartoon-style-vector-id626785978?k=6&m=626785978&s=170667a&w=0&h=Hy0TWXsX07RnqTSS3uh8Fv1CL49cRsob1RYA_MrSfSY=" alt="Profile" />
                </div>
                <h2>Oh No!</h2>
                <p>Please log in to view the profile page.</p>
                <Link to="/login" className="login-button">Login</Link>
            </div>
        );
    }

    return (
        <div>


            {/* Profile Page Content */}
            <div className="profile-page">
                <div className="profile-info">
                    <h1>Name: {user.name}</h1>
                    <p>Email: {user.email}</p>
                    <p>Password: {user.password}</p>
                    {/* Add other user data as needed */}
                </div>
            </div>

        </div>
    );
}
