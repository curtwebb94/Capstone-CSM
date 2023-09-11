import React from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import Login from './Login'
import Register from './Register'
import About from './About'
import Homepage from './Homepage'
import SnippetList from './snippets/snippetList'
import SnippetLocker from './snippets/snipppetLocker'
import SnippetForm from './snippets/snippetForm'

export default function ApplicationViews({ isLoggedIn }) {

    return (
        <main>
            <Routes>
                <Route path="/"
                        element={<Homepage />} />
                    
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/about" element={<About />} />
                <Route path="/search-code" element={<SnippetList />} />
                <Route path="/snippet-manager" element={<SnippetLocker />} />
                <Route path="/snippet-form" element={<SnippetForm />} />
                <Route path="*" element={<p>Whoops, nothing is here...</p>} />
            </Routes>
        </main>
    )
}