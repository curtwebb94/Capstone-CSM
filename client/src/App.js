import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { Spinner } from 'reactstrap';
import ApplicationViews from './components/ApplicationViews';
import { onLoginStatusChange } from './modules/authManager';
import Navbar from './components/NavBar';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null);

  useEffect(() => {
    onLoginStatusChange(setIsLoggedIn);
  }, []);

  if (isLoggedIn === null) {
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <Router>
      {/* Pass the isLoggedIn prop to the Navbar component */}
      <Navbar isLoggedIn={isLoggedIn} handleLogout={() => {}} />
      <ApplicationViews isLoggedIn={isLoggedIn} />
    </Router>
  );
}

export default App;
