import React, { Component } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import TextBox from "./TextBox"
import "../styles/Login.css"

export default function Login(props) {
    const [loginParams, setLoginParams] = React.useState({
        Login: "",
        Password: ""
    })
    let navigate = useNavigate();

    function handleChange(event) {
        const { name, value } = event.target;
        setLoginParams(prev => {
            return {
                ...prev,
                [name]: value
            }
        })
    }
    function handleSubmit(e) {
        e.preventDefault();
        let token;
        localStorage.setItem("Login", loginParams.Login)
        fetch("/api/Login",
            {
                headers: {
                    "Content-type": "application/json"
                },
                mode: 'cors',
                method: "POST",
                body: JSON.stringify({ "username": loginParams.Login, "password": loginParams.Password })
            })
            .then(res => res.text())
            .then(data => {
                if (data != "") {
                    localStorage.setItem("Jwt", data);
                    token = data;
                    return data;
                }
                else {

                }
            })
            .then(data => {
                if (data != "User couldn't be found") {
                    navigate("/Logged", { replace: true });
                }
                else {
                    throw new Error('something went wrong');
                }
            })
            .then(() => {
                fetch(`/api/Everyone/Users/${loginParams.Login}`,
                    {
                        headers: {
                            'Authorization': `Bearer ${token}`,
                            "Content-type": "application/json"
                        },
                        mode: 'cors',
                        method: "GET",
                    }).then(res => res.json())
                    .then(data => {
                        localStorage.setItem("Role", data.Role)
                    })
            })
            .catch(err => err);


    }


    const firstFlexBox = { type: "text", pholder: "Your login", value: loginParams.Login, handleChangeProp: handleChange, name: "Login" }
    const secondFlexBox = { type: "password", pholder: "Your password", value: loginParams.Password, handleChangeProp: handleChange, name: "Password" }

    return (
        <div className='main-div'>
            <form method="Post" onSubmit={handleSubmit}>
                <TextBox obj={firstFlexBox} />
                <TextBox obj={secondFlexBox} />
                <button className='submit-button' >Submit</button>
            </form>
        </div>
    )
}



