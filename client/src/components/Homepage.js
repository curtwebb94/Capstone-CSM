import React, { useState } from 'react';
import './Homepage.css';
import "firebase/auth";

export default function Homepage() {
  const [showSearchBar, setShowSearchBar] = useState(false);

  return (
    <div className="Homepage">
      <section>
        <div className="MainHeader">
          Organizing and Access Reusable Code
        </div>
        <div className="SecHeader">
          a user-friendly interface to store and manage code snippets effectively
        </div>
        <button onClick={() => setShowSearchBar(true)} className="search-button">
          Search for Code Now
        </button>
      </section>

      <section>
        {showSearchBar && (
          <div className="search-bar">
            <input type="text" placeholder="Enter search query..." />
          </div>
        )}
      </section>
    </div>
  );
}
