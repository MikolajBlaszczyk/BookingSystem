import React from "react"
import '../styles/Search.css'
import LocationBox from "./LocationBox"
import { useNavigate } from "react-router-dom"
import BookCard from "./BookCard"

export default function Search(props) {
    let user = localStorage.getItem("Role")
    const [params, setParams] = React.useState({ locations: [], locationsDiv: [], displayBookCard: false, displayContent: true, ID: "" })
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
                setParams(prev => { return { ...prev, locations: data, locationsDiv: data } })
            })
            .catch(err => err);

    }, [1])

    const array = [...params.locations];
    const distinct = [...new Set(array)]
    const selection = (<select onChange={handleSelectSearch} name="locations">
        {distinct.map((item) => {
            return (
                <option key={item.ID}>{item.NumberOfDesks}</option>
            )
        })}
    </select>
    )

    function handleManageClick() {
        navigate('/Reservations', { replace: true })
    }
    function handleSelectSearch(e) {
        let value = e.target.value;
        const newLocationDiv = params.locations.filter(x => x.NumberOfDesks >= value);
        setParams(prev => {
            return {
                ...prev,
                locationsDiv: newLocationDiv
            }
        })
    }
    function handleCardCLick(id) {
        setParams(prev => { return { ...prev, displayBookCard: (!prev.displayBookCard), displayContent: (!prev.displayContent), ID: id } })

    }
    function handleEdit() {
        navigate('/Admin', { replace: true })
    }
    function handleAddressSearch(e) {
        let value = e.target.value;
        const newLocationDiv = params.locations.filter(x => x.Address.toLowerCase().includes(value.toLowerCase()));

        setParams(prev => {
            return {
                ...prev,
                locationsDiv: newLocationDiv
            }
        })
    }

    return (
        <div className="main">
            <div className="navbar">
                <div>
                    <label onClick={handleManageClick}>Manage</label>
                </div>
                {user === "Admin" && <div><label onClick={handleEdit}>Edit</label></div>}
                <div>
                    <label>{user}</label>
                </div>
            </div>

            {params.displayContent && <div className="content">
                <div className="navbar-filters">
                    <input type="text" className="search-input" onChange={handleAddressSearch} placeholder="Address" />
                    <div className="nav-locations">
                        <label>Minimum desks:</label>
                        {selection}
                    </div>
                </div>
                <div className="location-div">
                    {<div className="locations-cards">
                        {params.locationsDiv.length == 0 ? <h1>There are no location with this Address</h1> : params.locationsDiv.map(location => {
                            return <LocationBox key={location.ID} obj={{ ...location, handleClick: handleCardCLick, buttonValue: "check" }} />
                        })}
                    </div>}
                </div>
            </div>}
            {params.displayBookCard == true && <BookCard obj={{ locations: [...params.locations], ID: params.ID, handleClick: handleCardCLick }} />}
        </div>
    )
}



