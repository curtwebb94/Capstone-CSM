import React, { useEffect, useState } from "react";
import { Card } from "reactstrap";
import { SaveFavSnippet } from "../../modules/favSnippetManager";
import { getUserByFirebaseId } from "../../modules/userManager";
import firebase from 'firebase/app';
import "./snippet.css";

export default function Snippet({ snippet }) {
  const [isStarred, setIsStarred] = useState(false);
  const [isContentOpen, setIsContentOpen] = useState(false); // State to track whether content overlay is open
  const [currentUserUid, setCurrentUserUid] = useState(null);
  const [userDetails, setUserDetails] = useState(null);

  useEffect(() => {
    const fetchUserDetails = async () => {
      // Get the current user's Firebase UID
      const user = firebase.auth().currentUser;
      if (user) {
        setCurrentUserUid(user.uid);

        // Use getUserByFirebaseId to get more user details if needed
        const userDetails = await getUserByFirebaseId(user.uid);
        setUserDetails(userDetails);
      }
    };

    fetchUserDetails();
  }, []);

  const toggleContentOverlay = () => {
    setIsContentOpen((prevState) => !prevState); // Toggle the visibility of content overlay
  };

  const handleSaveClick = () => {
    // Check if user details are available
    if (userDetails) {
      // Get the actual user ID from userDetails.id
      const userId = userDetails.id;

      // Call the SaveFavSnippet function to save the snippet as a favorite
      const favoriteSnippetData = {
        userId: userId,
        snippetId: snippet.id,
        createTime: new Date().toISOString(),
      };

      SaveFavSnippet(favoriteSnippetData)
        .then((response) => {
          console.log('Snippet saved as favorite:', response);

          // Update the isStarred state to indicate that the snippet is now a favorite
          setIsStarred(true);
        })
        .catch((error) => {
          console.error('Error saving snippet as favorite:', error);
          // Handle any errors that occurred during the save operation
        });
    }
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
      {!isStarred && ( // Show the "Save" button only if the snippet is not yet a favorite
        <button className="save-button" onClick={handleSaveClick}>
          Save
        </button>
      )}
    </Card>
  );
}
