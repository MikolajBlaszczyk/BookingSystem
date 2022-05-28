import '../styles/LocationBox.css'

export default function locationBox(props) {

    return (<>
        <div className="content-locationbox">
            <div>
                <h1>{`Address: ${props.obj.Address}`}</h1>
                <h2>{`City: ${props.obj.City}`}</h2>
                <h3>{`Desks in location: ${props.obj.NumberOfDesks}`}</h3>
                <h3>{props.obj.AllReserved == true ? "All desks are booked!" : "Available desks"}</h3>
            </div>
            <button onClick={() => props.obj.handleClick(props.obj.ID)}>{props.obj.buttonValue}</button>
        </div>

    </>
    )
}