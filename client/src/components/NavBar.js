import React from 'react';
import firebase from 'firebase/app';
import 'firebase/auth';
import './NavBar.css'; // Import the CSS file
import { Link } from 'react-router-dom';

export const logout = () => {
  firebase.auth().signOut();
};

export default function Navbar({isLoggedIn}) {
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
          <Link to="/profile" className="nav-link">Profile</Link>
        </li>
        <li className="nav-item">
          <Link to="/snippet-manager" className="nav-link">Snippet Manager</Link>
        </li>
        <li className="nav-item">
          <Link to="/contact" className="nav-link">Contact</Link>
        </li>
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
        )
        }
      </ul>
    </nav>
  );
}
