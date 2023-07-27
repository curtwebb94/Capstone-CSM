import React from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import Login from './Login'
import Register from './Register'
import About from './About'
import ProfilePage from './ProfilePage'
import Homepage from './Homepage'

export default function ApplicationViews({ isLoggedIn }) {

    return (
        <main>
            <Routes>
                <Route path="/"
                        element={<Homepage />} />
                    
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/about" element={<About />} />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="*" element={<p>Whoops, nothing here...</p>} />
            </Routes>
        </main>
    )
}