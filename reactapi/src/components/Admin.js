import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Admin.css"
import LocationBox from "./LocationBox"
import DeskBox from "./DeskBox"

export default function Admin(props) {
    const user = localStorage.getItem("Login");
    const navigate = useNavigate();
    let count = 0;
    const [params, setParams] = React.useState({
        locations: [],
        switchValue: "switch to desks",
        desks: [],
        switchMethod: "add",
        Address: "",
        City: "",
        Position: "",
        Monitor: 0,
        LocationID: 0
    })
    function handleLabelClick() {
        navigate('/Logged', { replace: true })
    }

    function handleMangeClick() {
        navigate('/Reservations', { replace: true })
    }

    function DeleteLocation() {
        console.log("delete locaiton");
    }

    function handleSwitch() {
        setParams(prev => { return { ...prev, switchValue: ("switch to" + (prev.switchValue.includes('desks') ? " locations" : " desks")) } });
    }

    function handleChange(e) {
        const { value, placeholder } = e.target;
        setParams(prev => {
            return {
                ...prev,
                [placeholder]: value
            }
        })
    }

    async function DeleteLocation(id) {
        setParams(prev => { return { ...prev, locations: (params.locations.filter(x => x.ID != id)) } })
        const token = localStorage.getItem("Jwt");
        fetch(`/api/Admin/desk/remove/${id}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    "Content-type": "application/json",
                },
                mode: "cors",
                method: "DELETE",
            })
            .then(res => { console.log(res.statusText) })
    }

    useEffect(function () {
        const token = localStorage.getItem("Jwt");
        fetch("/api/Everyone/Reservations/Admin",
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-type': "application/json"
                },
                mode: 'cors',
                method: "GET"
            })
            .then(res => res.json())
            .then(data => setParams(prev => { console.log(data); return { ...prev, desks: data } }))
            .then(err => err);
        fetch("/api/Everyone/data", {
            headers: {
                'Authorization': `Bearer ${token}`,
                "Content-type": "application/json",
            },
            mode: "cors",
            method: "GET",
        }).then(res => res.json())
            .then(data => setParams(prev => { return { ...prev, locations: data } }))
            .catch(err => err);
    }, [0])

    async function RemoveDesk(id) {
        console.log(id);
        setParams(prev => { return { ...prev, desks: (params.desks.filter(x => x.ID != id)) } })
        const token = localStorage.getItem("Jwt");
        fetch(`/api/Admin/desk/remove/${id}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    "Content-type": "application/json",
                },
                mode: "cors",
                method: "DELETE",
            })
            .then(res => { console.log(res.statusText) })
    }
    async function handleSubmit(e) {
        e.preventDefault();
        const { name } = e.target;
        console.log(name);
        const token = localStorage.getItem("Jwt");
        if (name == "desk") {
            fetch(`/api/Admin/desk/add/${params.Position}/${params.Monitor}/${params.LocationID}`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        "Content-type": "application/json",
                    },
                    mode: "cors",
                    method: "PUT",
                })
        }
        else if (name == "location") {
            fetch(`/api/Admin/location/add/${params.Address}/${params.City}`,
                {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        "Content-type": "application/json",
                    },
                    mode: "cors",
                    method: "PUT",
                })
        }
    }

    return (

        <div className="main-admin">
            <div className="navbar-admin">
                <div>
                    <label onClick={handleMangeClick}>Manage</label>
                </div>
                <div>
                    <label onClick={handleLabelClick}>Book</label>
                </div>
                <div>
                    <label>{user}</label>
                </div>
            </div>
            <div className="content-admin">
                <div className="location-div">
                    {params.switchValue === 'switch to desks' ?
                        params.locations.map(location => {
                            return <LocationBox key={location.ID} obj={{ ...location, buttonValue: "remove", handleClick: DeleteLocation }} />
                        })
                        :
                        params.desks.map(desk => {
                            return <DeskBox key={count++} obj={{ ...desk, locations: params.locations, Admin: true, handleClick: RemoveDesk }} />
                        })
                    }
                </div>
                <div className="navbar-filters-admin">
                    <div className="switch">
                        <button onClick={handleSwitch}>{params.switchValue}</button>
                    </div>
                    <div>

                        {params.switchValue == 'switch to desks' ?
                            <form className="form-admin" name="location" onSubmit={handleSubmit}>
                                <input type="text" placeholder="Address" value={params.Address} onChange={handleChange} />
                                <input type="text" placeholder="City" value={params.City} onChange={handleChange} />
                                <button>add</button>
                            </form>
                            :
                            <form className="form-admin" name="desk" onSubmit={handleSubmit}>
                                <input type="text" placeholder="Position" value={params.Position} onChange={handleChange} />
                                <input type="number" placeholder="Monitor" value={params.Monitor} onChange={handleChange} />
                                <input type="number" placeholder="LocationID" value={params.LocationID} onChange={handleChange} />
                                <button >add</button>
                            </form>
                        }

                    </div>
                </div>
            </div>
        </div>
    )
}