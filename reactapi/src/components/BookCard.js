import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../styles/BookCard.css'
export default function BookCard(props) {
    const navigate = useNavigate();
    const [params, setParams] = React.useState(
        {
            ID: "",
            StartDate: "",
            EndDate: ""
        });

    async function fetchData() {
        const token = localStorage.getItem("Jwt");
        fetch(`/api/Employee/desk/book/${params.ID}/${params.StartDate}/${params.EndDate}`,
            {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    "Content-type": "application/json",
                },
                mode: "cors",
                method: "PUT"
            })
            .then(response => {
                return response.json()
            })
            .then(() => {
                navigate('/Logged', { replace: true })
            })
            .catch(err => err);
    }

    React.useEffect(() => {
        fetchData();
    }, [])

    const bookDesk = (e) => {
        e.preventDefault();
        fetchData();
    }

    function handleChange(e) {
        const { value, placeholder } = e.target
        console.log(params)
        setParams(prev => {
            return {
                ...prev,
                [placeholder]: value
            }
        })
    }

    const location = [...props.obj.locations.filter(x => x.ID === props.obj.ID)]
    const desks = [...location[0].Desks]

    const displayDesks = (
        <div className="desk-list">
            {desks.map(desk => {
                return (
                    <div className="desk-card">
                        <div>
                            <h3>{`Position: ${desk.Position}`}</h3>
                            <h3>{`Monitors: ${desk.Monitors}`}</h3>
                        </div>
                        <input type="radio" className="radio" placeholder="ID" value={desk.ID} name="booked" onChange={handleChange} />
                    </div>
                )
            })}
        </div>
    )

    return (
        <div className="book-card">
            <div className="navbar-book-card">
                <button className="close-card" onClick={props.obj.handleClick}>X</button>
            </div>
            <form className='content-book-form' onSubmit={bookDesk} >
                <div className="content-book-card">
                    <div className="resevation-table">
                        {displayDesks}
                    </div>
                    <div className="controls">
                        <div className="controls-date">
                            <label htmlFor="StartDate">From</label>
                            <input type="date" placeholder="StartDate" name="StartDate" onChange={handleChange} value={params.StartDate} />
                            <label htmlFor="EndDate">To</label>
                            <input type="date" placeholder="EndDate" name="EndDate" onChange={handleChange} value={params.EndDate} />
                        </div>
                        <button >Book</button>
                    </div>
                </div>
            </form>
        </div>
    )
}