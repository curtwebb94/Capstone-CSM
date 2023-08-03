# Code Snippet Manager


Code Snippet Manager is a web application that allows users to store, manage, and organize their code snippets. It provides an easy-to-use interface for creating, editing, and deleting code snippets, along with features to categorize and search for snippets based on programming languages.

## Features

- User authentication: Users can create an account or log in using their existing credentials. Firebase Authentication is used for secure and seamless user authentication.

- Code snippet creation: Users can create new code snippets with a title, content, description, programming language, and creation date.

- Code snippet deletion: Users have the ability to delete unwanted code snippets from their collection.

- Categorization by programming language: Snippets can be categorized based on the programming language used, making it easy for users to find specific snippets.

- Search functionality: Users can search for code snippets using keywords or programming language filters.

## Technologies Used

- **Frontend**: The frontend of this application is built using React, a popular JavaScript library for building user interfaces. HTML, CSS, and JavaScript are used for creating the user interface and handling user interactions.

- **Backend**: The backend is implemented using Node.js, a server-side JavaScript runtime. Express.js, a minimalist web application framework, is used to handle routing and API endpoints.

- **Database**: The data for code snippets and user details is stored in a NoSQL database using Firebase Firestore.

- **Authentication**: Firebase Authentication is used for user authentication, providing secure login and registration features.

- **Styling**: The application is styled using CSS, and custom styles are applied to enhance the user interface.

## Setup and Installation

1. Clone the repository: `git clone https://github.com/your-username/code-snippet-manager.git`
2. Install dependencies: `cd code-snippet-manager` and `npm install`
3. Set up Firebase project: Create a new Firebase project and set up Firebase Authentication and Firestore database.
4. Configure Firebase credentials: Create a `.env` file in the root directory and add your Firebase credentials as environment variables.
   ```
   REACT_APP_FIREBASE_API_KEY=your_api_key
   REACT_APP_FIREBASE_AUTH_DOMAIN=your_auth_domain
   REACT_APP_FIREBASE_PROJECT_ID=your_project_id
   REACT_APP_FIREBASE_DATABASE_URL=your_database_url
   REACT_APP_FIREBASE_STORAGE_BUCKET=your_storage_bucket
   REACT_APP_FIREBASE_MESSAGING_SENDER_ID=your_messaging_sender_id
   REACT_APP_FIREBASE_APP_ID=your_app_id
   ```
5. Start the development server: `npm start`

## Usage

1. Sign up or log in with your existing account.
2. Create a new code snippet by clicking on the "Create Snippet" button and fill in the required details.
3. View, edit, or delete your existing snippets from the dashboard.
4. Use the search functionality to find specific snippets by keywords or programming language filters.

## Contributing

Contributions to Code Snippet Manager are welcome! If you find any bugs or have suggestions for new features, feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT). Feel free to use, modify, and distribute the code as per the terms of the license.

## Acknowledgments

- Thanks to the open-source community for providing useful libraries and tools that made this project possible.
- Special thanks to [Firebase](https://firebase.google.com/) for providing authentication and database services.
