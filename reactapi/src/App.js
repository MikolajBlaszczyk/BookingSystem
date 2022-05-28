/// <reference path="components/login.jsx" />
import React, { Component } from 'react';
import { BrowserRouter as Router, withRouter, Route, Routes } from 'react-router-dom';
import Search from './components/Search';
import Login from './components/Login';
import Admin from './components/Admin'
import Reservations from './components/Reservations'

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }


    state = {
        logged: false,
        Login: ""
    }

    LoggedIn = (e) => {
        console.log('this is'+e)
        this.setState({
            logged: true,
            Login: (e)
        })
    }

    render() {
        return (
            <Router>
                <Routes>
                    <Route path="/" element={<Login logged={this.LoggedIn} />} />
                    <Route path="/Logged" element={<Search />} />
                    <Route path="/Admin" element={<Admin />} />
                    <Route path="/Reservations" element={<Reservations  />}/>
                </Routes>
            </Router>
        );
    }
}

