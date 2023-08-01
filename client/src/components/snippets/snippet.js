import React, { useState } from "react";
import { Card } from "reactstrap";
import { SaveFavSnippet } from "../../modules/favSnippetManager";
import "./snippet.css";

export default function Snippet({ snippet }) {
  const [isStarred, setIsStarred] = useState(false);
  const [isContentOpen, setIsContentOpen] = useState(false); // State to track whether content overlay is open

  const handleSaveClick = () => {
    // ... (same as your existing handleSaveClick function)
  };

  const toggleContentOverlay = () => {
    setIsContentOpen((prevState) => !prevState); // Toggle the visibility of content overlay
  };

  return (
    <Card className="m-4 snippet-card">
      <div className="snippet-content">
        <p className="text-left px-2">Title: {snippet.title}</p>
        <p className="text-left px-2">Description: {snippet.description}</p>
        <div className="button-container">
          <button className="transparent-button" onClick={toggleContentOverlay}>
            View Code
          </button>
          {isContentOpen && (
            <button className="transparent-button close-button" onClick={toggleContentOverlay}>
              Close
            </button>
          )}
        </div>
        {isContentOpen && (
          <div className="content-overlay">
            <div className="content-popup">
              <p className="content-text">{snippet.content}</p>
            </div>
          </div>
        )}
      </div>
      <button className="save-button" onClick={handleSaveClick}>
        Save
      </button>
    </Card>
  );
}
