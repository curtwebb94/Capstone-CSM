import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getUserByFirebaseId } from "../../modules/userManager";
import { getFavSnippetsByUserId, DeleteFavSnippet } from "../../modules/favSnippetManager";
import { getSnippetDetails } from "../../modules/snippetManager";
import firebase from "firebase/app";
import Snippet from "./snippet";
import "./snippetLocker.css";

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

    fetchFavoriteSnippets();
  }, []);

  const handleSnippetDelete = (snippetId) => {
    // Find the favorite snippet with the matching snippetId
    const user = firebase.auth().currentUser;
    getUserByFirebaseId(user.uid).then((userDetails) => {
      getFavSnippetsByUserId(userDetails.id).then((favoriteSnippets) => {
        const favoriteSnippetToDelete = favoriteSnippets.find( (fs) => fs.snippetId = snippetId)



        if (favoriteSnippetToDelete) {
          // Call the DeleteFavSnippet function to delete the snippet from favorites by its id
          DeleteFavSnippet(favoriteSnippetToDelete.id)
            .then(() => {
              // Remove the deleted snippet from the favoriteSnippets list
              setFavoriteSnippets((prevSnippets) =>
                prevSnippets.filter((snippet) => snippet.id !== snippetId)
              );
            })
            .catch((error) => {
              console.error("Error deleting snippet from favorites:", error);
              // Handle any errors that occurred during the delete operation
            });
        }
      });

    })
  };

  return (
    <div className="snippet-locker-container">
      <div className="profile-section">
        <h1>User Profile</h1>
        <p>Username: {email}</p>
        <button>Edit Username</button>
      </div>

      <div className="favorite-snippets-section">
        <h1>Favorite Snippets</h1>
        <Link to="/snippet-form">
        <button>Create Snippet</button>
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
