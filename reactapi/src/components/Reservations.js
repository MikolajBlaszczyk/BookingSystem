import React from "react"
import { useNavigate } from "react-router-dom";
import '../styles/Reservations.css'
import DeskBox from "./DeskBox"

export default function Reservations() {
    const user = localStorage.getItem("Login");
    const [params, setParams] = React.useState({ desks: [], locations: [] });
    const navigate = useNavigate();

    React.useEffect(function () {
        const token = localStorage.getItem("Jwt");

        fetch("/api/Everyone/data", {
            headers: {
                'Authorization': `Bearer ${token}`,
                "Content-type": "application/json",
            },
            mode: "cors",
            method: "GET",
        }).then(res => res.json())
            .then(data => {
                setParams(prev => { return { ...prev, locations: [...data] } })
            })
            .catch(err => err);

        fetch("/api/Everyone/Reservations", {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-type': "application/json"
            },
            mode: "cors",
            method: "GET"
        })
            .then(res => res.json())
            .then(data => {
                setParams(prev => { return { ...prev, desks: [...data] } })
            })
            .catch(err => err);
    }, [0])

    function ReservationsChange(id) {
        setParams(prev => {
            return {
                ...prev,
                desks: prev.desks.filter(x => x.ID != id)
            }
        })
    }

    function handleBook() {
        navigate('/Logged', { replace: true });
    }
    function handleEdit() {
        navigate('/Admin', { replace: true })
    }

    return (
        <div className="main-res">
            <div className="navbar-res">
                <label onClick={handleBook}>Book</label>
                {user === "Admin" && <div><label onClick={handleEdit}>Edit</label> </div>}
                <div>
                    <label>{user}</label>
                </div>
            </div>
            <div className="content-res">
                {params.desks.map(desk => {
                    return (
                        <DeskBox key={desk.ID} obj={{ ...desk, locations: params.locations, Admin: false, resChange: ReservationsChange }} />
                    )
                })}
            </div>
        </div>
    )
}