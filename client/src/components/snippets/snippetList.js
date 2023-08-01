import React, { useEffect, useState } from "react";
import Snippet from "./snippet";
import { getSnippets } from "../../modules/snippetManager";
import "./snippetList.css";

export default function SnippetList() {
  const [snippets, setSnippets] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");

  // Placeholder text options for the search bar
  const placeholderOptions = [
    "Search by title or content...",
    "Find snippets...",
    "Type your query here...",
    "Explore snippets...",
  ];

  // Current index to rotate through the placeholderOptions array
  const [placeholderIndex, setPlaceholderIndex] = useState(0);

  // Function to rotate through the placeholderOptions array
  useEffect(() => {
    const interval = setInterval(() => {
      setPlaceholderIndex((prevIndex) =>
        prevIndex === placeholderOptions.length - 1 ? 0 : prevIndex + 1
      );
    }, 2000); // Rotate every 2 seconds

    return () => clearInterval(interval);
  }, []);

  // Fetch the snippets from the API and set the initial list of snippets
  useEffect(() => {
    getSnippets().then((data) => setSnippets(data));
  }, []);

  // Filter the snippets based on the search term
  const filteredSnippets = snippets.filter(
    (snippet) =>
      snippet.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
      snippet.content.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div>
      <div className="search-bar">
        <input
          type="text"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          placeholder={placeholderOptions[placeholderIndex]}
          className="search-input"
        />
      </div>
      {searchTerm && (
        <div className="snippet-list">
          {filteredSnippets.length > 0 ? (
            filteredSnippets.map((snippet) => (
              <Snippet key={snippet.id} snippet={snippet} />
            ))
          ) : (
            <p>No matching snippets found.</p>
          )}
        </div>
      )}
    </div>
  );
}
