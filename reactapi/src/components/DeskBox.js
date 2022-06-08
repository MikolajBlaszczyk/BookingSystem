import '../styles/LocationBox.css'
import '../styles/DeskBox.css'
import React from 'react';
export default function DeskBox(props) {
    //IsReserved = true, Position = desk.Position, Monitors = desk.Monitors, LocationID = desk.LocationID
    const [params, setParams] = React.useState(
        {
            oldID: "",
            newID: "",
            startDate: "",
            endDate: "",
            reservationID: ""
        })
    const arr = props.obj.locations;

    function handleChange(e) {
        const { value, placeholder } = e.target;
        setParams(prev => {
            return {
                ...prev,
                [placeholder]: value
            }
        })
    }
    async function DeleteReservation() {
        const token = localStorage.getItem("Jwt");
        fetch(`/api/Employee/Delete/${props.obj.resID}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    "Content-type": "application/json",
                },
                mode: "cors",
                method: "DELETE",
            })
            .then(res => { console.log(res.statusText) })
        props.obj.resChange(props.obj.ID);
    }
    async function Submit(e) {
        e.preventDefault();

        const token = localStorage.getItem("Jwt");
        fetch(`/api/Employee/desk/book/change/${props.obj.ID}/${params.newID}/${params.startDate}/${params.endDate}/${props.obj.resID}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    "Content-type": "application/json",
                },
                mode: "cors",
                method: "PUT",
            })
            .then(res => { console.log(res.statusText) })
    }

    return (
        <div className="content-locationbox">
            <div className="desk">
                <h1>{`Position: ${props.obj.Position}`}</h1>
                <h2>{`Monitors: ${props.obj.Monitors}`}</h2>
                <h3>{`Locations ${arr.filter(item => item.ID == props.obj.LocationID)[0].Address}`}</h3>
                <h3>{`From ${props.obj.Start}`}</h3>
                <h3>{`To ${props.obj.End} `}</h3>
            </div>
            {props.obj.Admin == false &&
                <div className='special-div'>
                    <form className='special-form' onSubmit={Submit}>
                        <div className='controls-desk'>
                            <label htmlFor='start'>Start</label>
                            <input type="date" value={params.startDate} name="start" placeholder='startDate' onChange={handleChange} />
                            <label htmlFor='end'>End</label>
                            <input type="date" value={params.End} name="end" placeholder='endDate' onChange={handleChange} />
                            <label htmlFor='id'>New desk ID</label>
                            <input type="number" value={params.newID} name="id" placeholder="newID" onChange={handleChange} />
                            <button className='swap'>Swap</button>
                        </div>
                    </form>
                    <button onClick={DeleteReservation}>cancel</button>
                </div>
            }
            {props.obj.Admin == true &&
                <div>
                    <button onClick={() => props.obj.handleClick(props.obj.ID)}>remove</button>
                </div>
            }
        </div>
    )
}