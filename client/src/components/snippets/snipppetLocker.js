import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getUserByFirebaseId, updateUser } from "../../modules/userManager";
import { getToken } from "../../modules/authManager";
import { getFavSnippetsByUserId, DeleteFavSnippet } from "../../modules/favSnippetManager";
import { getSnippetDetails } from "../../modules/snippetManager";
import firebase from "firebase/app";
import Snippet from "./snippet";
import "./snippetLocker.css";

const SnippetLocker = () => {
  const [favoriteSnippets, setFavoriteSnippets] = useState([]);
  const [currentUserUid, setCurrentUserUid] = useState(null);
  const [email, setEmail] = useState(null);
  const [editMode, setEditMode] = useState(false);
  const [newUsername, setNewUsername] = useState("");
  const [userDetails, setUserDetails] = useState(null); // Define userDetails state

  useEffect(() => {
    const currentUser = firebase.auth().currentUser;
    if (currentUser) {
      const userId = currentUser.uid;
      console.log("User ID:", userId);
    } else {
      console.log("No user is currently signed in.");
    }

    // Fetch the current user's Firebase UID and get the favorite snippets based on the UID
    const fetchUserData = async () => {
      try {
        const user = firebase.auth().currentUser;
        if (user) {
          setCurrentUserUid(user.uid);

          // Use getUserByFirebaseId to get more user details if needed
          const userDetails = await getUserByFirebaseId(user.uid);
          console.log("User Details:", userDetails);
          console.log("User Id:", userDetails.id);
          setEmail(userDetails.username);


          // Set user details state
          setUserDetails(userDetails);

          // Get the user's favorite snippets using the UID
          const favoriteSnippets = await getFavSnippetsByUserId(userDetails.id);
          console.log(favoriteSnippets);

          // Fetch the details of each favorite snippet and update the state with the details
          const snippetDetails = await Promise.all(
            favoriteSnippets.map((snippet) => getSnippetDetails(snippet.snippetId))
          );
          console.log("Snippet Details:", snippetDetails);

          // Set the 'isFavorited' property for each snippet based on the favoriteSnippets list
          const snippetsWithFavoritedStatus = snippetDetails.map((snippet) => ({
            ...snippet,
            isFavorited: favoriteSnippets.some((favSnippet) => favSnippet.snippetId === snippet.id),
          }));

          // Update the state with the fetched favorite snippets and their details
          setFavoriteSnippets(snippetsWithFavoritedStatus);
        }
      } catch (error) {
        console.error("Error fetching favorite snippets:", error);
      }
    };

    fetchUserData();
  }, []);

  // Function to handle updating the username
  const handleUsernameUpdate = () => {
    const user = firebase.auth().currentUser;
    if (user) {
      // use the 'updateUser' function to update the username of the user with the provided data
      updateUser({ id: userDetails.id, username: newUsername })
        .then((updatedUser) => {
          setEmail(updatedUser.username);
          // Exit the edit mode by setting 'editMode' state to 'false'
          setEditMode(false);
        })
        .catch((error) => {
          console.error("Error updating username:", error);
        });
    }
  };

  const handleSnippetDelete = (snippetId) => {
    const user = firebase.auth().currentUser;
    getUserByFirebaseId(user.uid).then((userDetails) => {
      getFavSnippetsByUserId(userDetails.id).then((favoriteSnippets) => {
        const favoriteSnippetToDelete = favoriteSnippets.find((fs) => fs.snippetId === snippetId);

        if (favoriteSnippetToDelete) {
          DeleteFavSnippet(favoriteSnippetToDelete.id)
            .then(() => {
              setFavoriteSnippets((prevSnippets) =>
                prevSnippets.filter((snippet) => snippet.id !== snippetId)
              );
            })
            .catch((error) => {
              console.error("Error deleting snippet from favorites:", error);
            });
        }
      });
    });
  };

  return (
    <div className="snippet-locker-container">
      <div className="profile-section">
        <h1>User Profile</h1>
        {editMode ? (
          <div>
            <input
              type="text"
              value={newUsername}
              onChange={(e) => setNewUsername(e.target.value)}
            />
            <button onClick={handleUsernameUpdate}>Save</button>
            <button onClick={() => setEditMode(false)}>Cancel</button>
          </div>
        ) : (
          <div>
            <p>Username: {email}</p>
            <button onClick={() => setEditMode(true)}>Edit Username</button>
          </div>
        )}
      </div>

      <div className="favorite-snippets-section">
        <h1>Favorite Snippets</h1>
        <Link to="/snippet-form">
          <button className="create-snippet-button">Create Snippet</button>
        </Link>
        {favoriteSnippets.length > 0 ? (
          favoriteSnippets.map((snippet) => (
            <Snippet
              key={snippet.id}
              snippet={snippet}
              setFavoriteSnippets={setFavoriteSnippets}
              handleSnippetDelete={handleSnippetDelete}
            />
          ))
        ) : (
          <p>No favorite snippets found.</p>
        )}
      </div>
    </div>
  );
};

export default SnippetLocker;
