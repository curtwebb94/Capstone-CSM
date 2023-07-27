import React from 'react';
import firebase from 'firebase/app';
import 'firebase/auth';

export const logout = () => {
  firebase.auth().signOut();
};

export default function Navbar({ isLoggedIn, handleLogout }) {
    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container px-5">
                <a className="navbar-brand" href="#!">
                    Code Snippet Manager
                </a>
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent"
                    aria-controls="navbarSupportedContent"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
                        <li className="nav-item">
                            <a className="nav-link active" aria-current="page" href="/">
                                Home
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="/about">
                                About
                            </a>
                        </li>
                        {isLoggedIn && (
                            <>
                                <li className="nav-item">
                                    <a className="nav-link" href="/profile">
                                        Profile
                                    </a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link" href="#!">
                                        Snippet Manager
                                    </a>
                                </li>
                            </>
                        )}
                        <li className="nav-item">
                            <a className="nav-link" href="#!">
                                Contact
                            </a>
                        </li>
                            {isLoggedIn ? (
                        <li className="nav-item">
                            <button className="nav-link" onClick={logout}>
                            Logout
                            </button>
                        </li>
                        ) : (
                            <li className="nav-item">
                                <a className="login-link" href="/login">
                                    Login
                                </a>
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}
