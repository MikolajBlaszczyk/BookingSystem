import React, { Component } from 'react';
import Login from './Components/Login'

export default class App extends Component {
    static displayName = App.name;

   
    render() {
        return (
            <div>
                <Routes>
                    <Route path="/" element={<Login />} />
                </Routes>
                
            </div>
        );
    }

}
