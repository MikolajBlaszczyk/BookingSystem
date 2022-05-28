import '../styles/BookCard.css'
export default function BookCard(props) {

    console.log(props.obj.locations);
    const location = [...props.obj.locations.filter(x => x.ID === props.obj.ID)]
    console.log(location)
    const desks = [...location[0].Desks]
    console.log(desks)
    const displayDesks = (
        <div className="desk-list">
            {desks.map(desk => {
                return (
                    <div className="desk-card">
                        <div>
                            <h3>{`Position: ${desk.Position}`}</h3>
                            <h3>{`Monitors: ${desk.Monitors}`}</h3>
                            <h3>{desk.IsReserved ? "Unavailable" : "Available"}</h3>
                        </div>
                        <input type="radio" className="radio" value="book" name="booked" />
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
            <form className='content-book-form' onSubmit >
                <div className="content-book-card">
                    <div className="resevation-table">
                        {displayDesks}
                    </div>
                    <div className="controls">
                        <div className="controls-date">
                            <label htmlFor="StartDate">From</label>
                            <input type="date" name="StartDate" />
                            <label htmlFor="EndDate">To</label>
                            <input type="date" name="EndDate" />
                        </div>
                        <button>Book</button>
                    </div>
                </div>
            </form>
        </div>
    )
}