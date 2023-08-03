import React from 'react';
import './NavBar.css'; // Import the CSS file
import { Link } from 'react-router-dom';
import firebase from 'firebase';


export const logout = () => {
  firebase.auth().signOut();
};

export default function Navbar({ isLoggedIn }) {
  return (
    <nav className="navbar">
      <div className="navbar-title">
        <Link to="/">Code Snippet Manager</Link>
      </div>
      <ul className="navbar-menu">
        <li className="nav-item">
          <Link to="/" className="nav-link">Home</Link>
        </li>
        <li className="nav-item">
          <Link to="/about" className="nav-link">About</Link>
        </li>
        <li className="nav-item">
          <Link to="/search-code" className="nav-link">Search Snippets</Link>
        </li>
        {isLoggedIn && ( // Show the "Snippet Manager" link only if logged in
          <li className="nav-item">
            <Link to="/snippet-manager" className="nav-link">Snippet Manager</Link>
          </li>
        )}
        {isLoggedIn ? (
          <li className="nav-item">
            <button className="logout-button" onClick={logout}>
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
    </nav>
  );
}


