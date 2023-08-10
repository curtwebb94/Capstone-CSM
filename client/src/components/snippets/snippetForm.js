import React, { useState, useEffect } from "react";
import { getToken } from "../../modules/authManager";
import { addSnippet } from "../../modules/snippetManager";
import firebase from "firebase/app";
import { getUserByFirebaseId } from "../../modules/userManager";
import "./snippetForm.css";

const SnippetForm = () => {
  const [snippetData, setSnippetData] = useState({
    title: "",
    content: "",
    description: "",
    createTime: "",
    createdBy: "",
  });

  const [selectedLanguage, setSelectedLanguage] = useState("");
  const programmingLanguages = [
    "JavaScript",
    "Python",
    "Java",
    "C++",
    "Ruby",
    "PHP",
    "Swift",
    "C#",
    "Go",
    "TypeScript",
    // Add more programming languages as needed
  ];

  const currentUser = firebase.auth().currentUser;

  useEffect(() => {
    const interval = setInterval(() => {
      const currentTime = new Date().toLocaleTimeString();
      const currentDate = new Date().toLocaleDateString();
      setSnippetData((prevData) => ({
        ...prevData,
        createTime: `${currentDate} ${currentTime}`,
      }));
    }, 1000); // Update every 1 second

    return () => clearInterval(interval);
  }, []);

  const handleLanguageChange = (e) => {
    setSelectedLanguage(e.target.value);
    setSnippetData((prevData) => ({
      ...prevData,
      title: `${e.target.value}: ${prevData.title.split(":")[1] || ""}`,
    }));
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setSnippetData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (currentUser) {
      getUserByFirebaseId(currentUser.uid).then((UserId) => {
        const newSnippetData = {
          title: snippetData.title,
          content: snippetData.content,
          description: snippetData.description,
          createTime: snippetData.createTime,
          createdBy: currentUser.email,
          UserId: UserId.id,
        };

        getToken().then((token) => {
          addSnippet(newSnippetData, token)
            .then((response) => {
              console.log("Code snippet created successfully:", response);
              // Clear the form after successful submission
              setSnippetData({
                title: "",
                content: "",
                description: "",
                createTime: "",
                createdBy: "",
              });
              setSelectedLanguage(""); // Clear the selected language
            })
            .catch((error) => {
              console.error("Error creating code snippet:", error);
              // Handle any errors that occurred during the create operation
            });
        });
      });
    } else {
      console.error("No user is currently logged in.");
    }
  };

  return (
    <div className="code-snippet-form">
      <h2>Create Code Snippet</h2>
      <form onSubmit={handleSubmit}>
        <div className="language-select">
          <label htmlFor="programmingLanguage"></label>
          <select
            id="programmingLanguage"
            name="programmingLanguage"
            value={selectedLanguage}
            onChange={handleLanguageChange}
          >
            <option value="">Select a language</option>
            {programmingLanguages.map((language) => (
              <option key={language} value={language}>
                {language}
              </option>
            ))}
          </select>
        </div>

        <label htmlFor="title">Title:</label>
        <input
          type="text"
          id="title"
          name="title"
          value={snippetData.title}
          onChange={handleChange}
        />

        <label htmlFor="content">Content:</label>
        <textarea
          id="content"
          name="content"
          value={snippetData.content}
          onChange={handleChange}
        />

        <label htmlFor="description">Description:</label>
        <textarea
          id="description"
          name="description"
          value={snippetData.description}
          onChange={handleChange}
        />

        <label htmlFor="createTime">Create Time:</label>
        <input
          type="text"
          id="createTime"
          name="createTime"
          value={snippetData.createTime}
          readOnly // Make the input read-only
          onChange={handleChange}
        />

        <label htmlFor="createdBy">Created By:</label>
        <input
          type="text"
          id="createdBy"
          name="createdBy"
          placeholder="Enter your username..."
          value={snippetData.createdBy}
          onChange={handleChange}
        />

        <button type="submit" className="popup-button">
          Create Snippet
        </button>
      </form>
    </div>
  );
};

export default SnippetForm;
