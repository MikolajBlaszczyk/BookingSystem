import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Admin.css"
import LocationBox from "./LocationBox"

export default function Admin(props) {
    const user = localStorage.getItem("Login");
    const navigate = useNavigate();
    const [params, setParams] = React.useState({ locations: [], switchValue: "switch to desks" })

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

    useEffect(function () {
        const token = localStorage.getItem("Jwt");
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


    const switchValue = 'locations'

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
                    {params.locations.map(location => {
                        return <LocationBox key={location.ID} obj={{ ...location, buttonValue: "remove", handleClick: DeleteLocation }} />
                    })}
                </div>
                <div className="navbar-filters-admin">
                    <div className="switch">
                        <button onClick={handleSwitch}>{params.switchValue}</button>
                    </div>
                    <div>

                        {params.switchValue == 'switch to desks' ?
                            <form className="form-admin">
                                <input type="text" placeholder="position" />
                                <input type="number" placeholder="monitors" />
                                <input type="number" placeholder="location ID" />
                                <button>Add</button>
                            </form>
                            :
                            <form className="form-admin">
                                <input type="text" placeholder="Address" />
                                <input type="text" placeholder="City" />
                                <button>Add</button>
                            </form>
                        }

                    </div>
                </div>
            </div>
        </div>
    )
}