import React, { useEffect, useState } from "react";
import { getUserByFirebaseId } from "../../modules/userManager";
import { getFavSnippetsByFirebaseId, getFavSnippetsByUserId } from "../../modules/favSnippetManager";
import { getSnippetDetails } from "../../modules/snippetManager"; // Import the function from snippetManager.js
import firebase from "firebase/app";
import Snippet from "./snippet";
import "./snippetLocker.css"; // Import the CSS file for styling

const SnippetLocker = () => {
  const [favoriteSnippets, setFavoriteSnippets] = useState([]);
  const [currentUserUid, setCurrentUserUid] = useState(null);
  const [email, setEmail] = useState(null);

  useEffect(() => {
    const currentUser = firebase.auth().currentUser;
    if (currentUser) {
      const userId = currentUser.uid;
      console.log("User ID:", userId);
    } else {
      console.log("No user is currently signed in.");
    }

    // Fetch the current user's Firebase UID and get the favorite snippets based on the UID
    const fetchFavoriteSnippets = async () => {
      try {
        // Get the current user's Firebase UID
        const user = firebase.auth().currentUser;
        setEmail(user.email);
        if (user) {
          setCurrentUserUid(user.uid);

          // Use getUserByFirebaseId to get more user details if needed
          const userDetails = await getUserByFirebaseId(user.uid);
          console.log("User Details:", userDetails);
          console.log("User Id:", userDetails.id);

          // Get the user's favorite snippets using the UID
          const favoriteSnippets = await getFavSnippetsByUserId(userDetails.id);
          console.log(favoriteSnippets);

          // Fetch the details of each favorite snippet and update the state with the details
          const snippetDetails = await Promise.all(
            favoriteSnippets.map((snippet) => getSnippetDetails(snippet.snippetId))
          );
          console.log("Snippet Details:", snippetDetails);

          // Update the state with the fetched favorite snippets and their details
          setFavoriteSnippets(snippetDetails);
        }
      } catch (error) {
        console.error("Error fetching favorite snippets:", error);
      }
    };

    fetchFavoriteSnippets();
  }, []);

  return (
    <div className="snippet-locker-container">
      <div className="profile-section">
        <h1>User Profile</h1>
        <p>Username: {email}</p>
      </div>

      <div className="favorite-snippets-section">
        <h1>Favorite Snippets</h1>
        {favoriteSnippets.length > 0 ? (
          favoriteSnippets.map((snippet) => (
            <Snippet key={snippet.id} snippet={snippet} />
          ))
        ) : (
          <p>No favorite snippets found.</p>
        )}
      </div>
    </div>
  );
};

export default SnippetLocker;
