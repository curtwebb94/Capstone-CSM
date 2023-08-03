import React, { useEffect, useState } from "react";
import { Card } from "reactstrap";
import { SaveFavSnippet } from "../../modules/favSnippetManager";
import { getUserByFirebaseId } from "../../modules/userManager";
import firebase from "firebase/app";
import "./snippet.css";

const Snippet = ({ snippet, setFavoriteSnippets, handleSnippetDelete }) => {
  const [isContentOpen, setIsContentOpen] = useState(false);
  const [isSavePopupOpen, setIsSavePopupOpen] = useState(false);
  const [isDeletePopupOpen, setIsDeletePopupOpen] = useState(false);
  const [isCopyPopupOpen, setIsCopyPopupOpen] = useState(false);
  const [currentUserUid, setCurrentUserUid] = useState(null);
  const [userDetails, setUserDetails] = useState(null);

  useEffect(() => {
    const fetchUserDetails = async () => {
      const user = firebase.auth().currentUser;
      if (user) {
        setCurrentUserUid(user.uid);
        const userDetails = await getUserByFirebaseId(user.uid);
        setUserDetails(userDetails);
      }
    };

    fetchUserDetails();
  }, []);

  const toggleContentOverlay = () => {
    setIsContentOpen((prevState) => !prevState);
  };

  const handleSaveClick = () => {
    if (userDetails) {
      const userId = userDetails.id;
      const favoriteSnippetData = {
        userId: userId,
        snippetId: snippet.id,
        createTime: new Date().toISOString(),
      };

      SaveFavSnippet(favoriteSnippetData, "save")
        .then((response) => {
          console.log("Snippet saved as favorite:", response);
          setIsSavePopupOpen(true); // Show the save popup

          // Hide the save popup after a few seconds (e.g., 3 seconds)
          setTimeout(() => {
            setIsSavePopupOpen(false);
          }, 3000);
        })
        .catch((error) => {
          console.error("Error saving snippet as favorite:", error);
        });
    } else {
      setIsSavePopupOpen(true); // Show the save popup for non-logged-in users
    }
  };

  const handleDeleteClick = () => {
    handleSnippetDelete(snippet.id);
    setIsDeletePopupOpen(true); // Show the delete popup

    // Hide the delete popup after a few seconds (e.g., 3 seconds)
    setTimeout(() => {
      setIsDeletePopupOpen(false);
    }, 3000);
  };

  const handleCopyClick = () => {
    // ... (existing copy button code)
    setIsCopyPopupOpen(true); // Show the copy popup

    // Hide the copy popup after a few seconds (e.g., 3 seconds)
    setTimeout(() => {
      setIsCopyPopupOpen(false);
    }, 3000);
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
        </div>
      </div>
      {snippet.isFavorited ? (
        <button className="delete-button" onClick={handleDeleteClick}>
          Delete
        </button>
      ) : (
        <button className="save-button" onClick={handleSaveClick}>
          Save
        </button>
      )}
      {isContentOpen && (
        <div className="content-overlay">
          <button className="close-button" onClick={toggleContentOverlay}>
            X
          </button>
          <div className="content-text-wrapper">
            <p className="content-text">{snippet.content}</p>
            <button className="copy-button" onClick={handleCopyClick}>
              Copy Code
            </button>
          </div>
        </div>
      )}
      {isSavePopupOpen && userDetails !== null && (
        <div className="popup">Snippet successfully saved</div>
      )}
      {isSavePopupOpen && userDetails === null && (
        <div className="popup">
          <p>Must sign-in to save snippets</p>
          <button
            className="popup-link"
            onClick={() => {
              // Redirect the user to the login page
              window.location.href = "/login";
            }}
          >
            Go to Login
          </button>
        </div>
      )}
      {isDeletePopupOpen && <div className="popup">Snippet successfully deleted</div>}
      {isCopyPopupOpen && <div className="popup">Snippet successfully copied</div>}
    </Card>
  );
};

export default Snippet;
