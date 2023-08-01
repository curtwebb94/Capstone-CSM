import React from 'react';
import { useNavigate } from 'react-router-dom'; // Import useNavigate hook
import './Homepage.css';
import "firebase/auth";

export default function Homepage() {
  const navigate = useNavigate(); // Initialize the useNavigate hook

  // Function to handle the "Search Code Now" button click and navigate to "search-code" page
  const handleSearchButtonClick = () => {
    navigate('/search-code');
  };

  return (
    <div className="Homepage">
      <section>
        <div className="MainHeader">
          Organizing and Access Reusable Code
        </div>
        <div className="SecHeader">
          a user-friendly interface to store and manage code snippets effectively
        </div>
        <button onClick={handleSearchButtonClick} className="search-button">
          Search for Code Now
        </button>
      </section>
    </div>
  );
}
