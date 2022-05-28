import React from "react"
import '../styles/Reservations.css'

export default function Reservations(){
    const user = localStorage.getItem("Login");

    React.useEffect(function(){
        fetch()
    },[1])

    return (
        <div className="main-res">
            <div className="navbar-res">
                <label>Book</label>
                {user === "Admin" && <div><label >Edit</label> </div>}
                <div>
                    <label>{user}</label>
                </div>
            </div>
            <div className="content-res">

            </div>
        </div>
    )
}